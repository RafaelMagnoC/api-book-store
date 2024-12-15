using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using api_bookStore.App.DataBase;
using api_bookStore.App.Modules.Book.Entity;
using api_bookStore.App.Modules.Sale.DTO;
using api_bookStore.App.Modules.Sale.Entity;
using api_bookStore.App.Modules.Sale.Interface;
using api_bookStore.App.Modules.Sale.ViewModel;
using api_BookStore.App.Exceptions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace api_bookStore.App.Modules.Sale.Repository
{
    /// <summary>
    /// Repositório responsável pelas operações de vendas, como adicionar vendas, reduzir o estoque de livros e verificar a disponibilidade dos livros.
    /// </summary>
    /// <remarks>
    /// Inicializa uma nova instância do repositório de vendas.
    /// </remarks>
    /// <param name="bookStoreContext">O contexto do banco de dados para acessar as entidades de venda e livro.</param>
    /// <param name="mapper">Objeto que realiza a conversão entre as entidades e os DTOs.</param>
    public class SaleRepository(BookStoreContext bookStoreContext, IMapper mapper) : ISaleRepository
    {
        private readonly BookStoreContext _bookStoreContext = bookStoreContext ?? throw new ArgumentNullException(nameof(bookStoreContext));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        /// <summary>
        /// Adiciona uma nova venda ao sistema, verificando a disponibilidade dos livros e reduzindo o estoque.
        /// </summary>
        /// <param name="saleViewModelCreate">A lista de livros e quantidades para a venda.</param>
        /// <returns>Um DTO representando a venda realizada.</returns>
        /// <exception cref="NotFound">Lançado quando um livro não é encontrado ou não há estoque suficiente.</exception>
        /// <exception cref="AvailableQuantity">Lançado quando um livro não tem saldo suficiente para a venda.</exception>
        /// <exception cref="CreateException">Lançado quando ocorre um erro ao registrar a venda.</exception>
        public async Task<SaleDTO> SaleAdd(List<SaleViewModelCreate> saleViewModelCreate)
        {
            try
            {

                List<BookEntity> books = await _bookStoreContext.Book.Where(book => saleViewModelCreate.Select(sale => sale.BookId).Contains(book.Id)).ToListAsync();

                await CheckBookStock(books, saleViewModelCreate);

                EntityEntry<SaleEntity> saleCreated = await _bookStoreContext.Sale.AddAsync(new SaleEntity());

                int saleSaved = await _bookStoreContext.SaveChangesAsync();

                List<SaleBookEntity> saleBookEntities = saleViewModelCreate
                .Select(sale =>
                    new SaleBookEntity(sale.BookId, saleCreated.Entity.Id, books.FirstOrDefault(book => book.Id == sale.BookId)!.Price, sale.Quantity))
                .ToList();

                await _bookStoreContext.SaleBook.AddRangeAsync(saleBookEntities);

                int saleBookSaved = await _bookStoreContext.SaveChangesAsync();

                await ReduceBookStock(saleViewModelCreate);

                CalculeTotalSale(saleCreated.Entity, saleBookEntities);

                return saleBookSaved > 0 ? _mapper.Map<SaleDTO>(saleCreated.Entity) : throw new CreateException("Um erro ocorreu ao registrar a venda.");
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public async void CalculeTotalSale(SaleEntity sale, List<SaleBookEntity> saleBookList)
        {
            try
            {
                sale.TotalQuantity = saleBookList.Sum(sale => sale.Quantity);
                sale.TotalValue = saleBookList.Sum(sale => sale.Subtotal);
                _bookStoreContext.Update(sale);
                int saleUpdated = await _bookStoreContext.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// Reduz o estoque dos livros após a venda, verificando a quantidade disponível.
        /// </summary>
        /// <param name="saleViewModelCreate">A lista de livros e quantidades que foram vendidas.</param>
        /// <returns>A lista de livros atualizados após a venda.</returns>
        /// <exception cref="NotFound">Lançado quando um livro não é encontrado.</exception>
        /// <exception cref="AvailableQuantity">Lançado quando o estoque de um livro é insuficiente.</exception>
        public async Task<List<BookEntity>> ReduceBookStock(List<SaleViewModelCreate> saleViewModelCreate)
        {
            try
            {

                List<BookEntity> books = await _bookStoreContext.Book
                    .Where(book => saleViewModelCreate.Select(sale => sale.BookId).Contains(book.Id))
                    .Include(book => book.Inventory)
                    .ToListAsync();

                if (books.Count != saleViewModelCreate.Count)
                {
                    throw new NotFound("Um ou mais livros não foram encontrados.");
                }

                foreach (var sale in saleViewModelCreate)
                {
                    var book = books.FirstOrDefault(b => b.Id == sale.BookId) ?? throw new NotFound($"Livro com ID {sale.BookId} não encontrado.");

                    book.Inventory.Quantity -= sale.Quantity;
                }

                _bookStoreContext.Book.UpdateRange(books);

                int bookUpdated = await _bookStoreContext.SaveChangesAsync();

                return bookUpdated > 0 ? books : throw new Exception("Erro ao reduzir o estoque.");
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// Verifica se os livros existem no banco de dados.
        /// </summary>
        /// <param name="bookIds">A lista de IDs dos livros a serem verificados.</param>
        /// <returns>A lista de livros encontrados no banco de dados.</returns>
        /// <exception cref="NotFound">Lançado quando algum dos livros não é encontrado.</exception>
        public async Task<List<BookEntity>> BookExists(List<int> bookIds)
        {
            try
            {
                List<BookEntity> books = await _bookStoreContext.Book
                .AsNoTracking()
                .Where(book => bookIds.Contains(book.Id))
                .Include(book => book.Inventory)
                .ToListAsync();

                List<int> foundBookIds = books.Select(b => b.Id).ToList();
                List<int> missingBookIds = bookIds.Except(foundBookIds).ToList();

                if (missingBookIds.Count > 0)
                {
                    throw new NotFound($"Os seguintes IDs de livros não existem no banco de dados: {string.Join(", ", missingBookIds)}");
                }

                return books;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// Verifica a quantidade de estoque disponível para os livros antes de registrar a venda.
        /// </summary>
        /// <param name="bookList">A lista de livros.</param>
        /// <param name="saleViewModelCreate">A lista de livros e quantidades da venda.</param>
        /// <returns>A lista de livros que possuem estoque suficiente.</returns>
        /// <exception cref="AvailableQuantity">Lançado quando algum livro não tem saldo suficiente para a venda.</exception>
        public async Task<List<BookEntity>> CheckBookStock(List<BookEntity> bookList, List<SaleViewModelCreate> saleViewModelCreate)
        {
            try
            {
                List<BookEntity> books = await BookExists(bookList.Select(b => b.Id).ToList());

                foreach (var sale in saleViewModelCreate)
                {
                    var book = books.FirstOrDefault(b => b.Id == sale.BookId) ?? throw new NotFound($"Livro com ID {sale.BookId} não encontrado.");

                    if (book.Inventory.Quantity <= 0)
                    {
                        throw new AvailableQuantity($"O livro {book.Title} não possui saldo!");
                    }

                    if (book.Inventory.Quantity < sale.Quantity)
                    {
                        throw new AvailableQuantity($"O livro {book.Title} não possui saldo!");
                    }
                }

                return books;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// Retorna todos as vendas registradas no sistema.
        /// </summary>
        /// <returns>A lista de DTOs de vendas.</returns>
        public async Task<List<SaleDTO>> Sales()
        {
            try
            {
                List<SaleEntity> sales = await _bookStoreContext.Sale
                .AsNoTracking()
                .Include(sale => sale.SaleBook)
                .ThenInclude(sale => sale.Book)
                .ToListAsync();

                return sales.Count > 0 ? _mapper.Map<List<SaleDTO>>(sales) : [];
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
    }
}
