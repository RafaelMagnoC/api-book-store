using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_bookStore.App.DataBase;
using api_bookStore.App.Modules.Author.Entity;
using api_bookStore.App.Modules.Author.Interface;
using api_bookStore.App.Modules.Book.DTO;
using api_bookStore.App.Modules.Book.Entity;
using api_bookStore.App.Modules.Book.Interface;
using api_bookStore.App.Modules.Book.ViewModel;
using api_bookStore.App.Modules.Category.Entity;
using api_bookStore.App.Modules.Category.Interface;
using api_bookStore.App.Modules.Inventory.DTO;
using api_bookStore.App.Modules.Inventory.Entity;
using api_bookStore.App.Modules.Inventory.Interface;
using api_bookStore.App.Modules.Inventory.ViewModel;
using api_BookStore.App.Exceptions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace api_bookStore.App.Modules.Book.Repository
{
    public class BookRepository(BookStoreContext bookStoreContext, IMapper mapper) : IBookRepository
    {

        private readonly BookStoreContext _bookStoreContext = bookStoreContext;
        private readonly IMapper _mapper = mapper;

        /// <summary>
        /// Adiciona um novo livro ao sistema.
        /// </summary>
        /// <param name="bookViewModelCreate">O modelo de livro que será adicionado.</param>
        /// <returns>O livro recém-adicionado.</returns>
        /// <exception cref="Exception">Lançado quando ocorre um erro interno de servidor.</exception>
        public async Task<BookDTO> BookAdd(BookViewModelCreate bookViewModelCreate)
        {
            try
            {
                AuthorEntity author = await _bookStoreContext.Author.FindAsync(bookViewModelCreate.AuthorId) ?? throw new NotFound($"nenhum author com o id: {bookViewModelCreate.AuthorId} encontrado.");
                CategoryEntity category = await _bookStoreContext.Category.FindAsync(bookViewModelCreate.CategoryId) ?? throw new NotFound($"nenhuma categoria com o id: {bookViewModelCreate.CategoryId} encontrada.");
                InventoryEntity inventory = await InventoryAdd(new InventoryViewModelCreate(bookViewModelCreate.Quantity));

                BookEntity book = new(bookViewModelCreate)
                {
                    Author = author,
                    AuthorId = author.Id,
                    Category = category,
                    CategoryId = category.Id,
                    InventoryId = inventory.Id,
                    Quantity = inventory
                };
                EntityEntry<BookEntity> bookCreated = await _bookStoreContext.Book.AddAsync(book);
                int bookSaved = await _bookStoreContext.SaveChangesAsync();

                return bookSaved > 0 ? _mapper.Map<BookDTO>(bookCreated.Entity) : throw new CreateException("um erro ocorreu ao cadastrar o livro");
            }
            catch (Exception exception)
            {
                throw new Exception(exception.ToString());
            }
        }

        /// <summary>
        /// Adiciona uma quantidade de produto ao sistema.
        /// </summary>
        /// <param name="inventoryViewModel">O modelo de inventário que será adicionada.</param>
        /// <returns>a quantidade do produto recém-adicionada.</returns>
        /// <exception cref="Exception">Lançado quando ocorre um erro interno de servidor.</exception>
        public async Task<InventoryEntity> InventoryAdd(InventoryViewModelCreate inventoryViewModel)
        {
            try
            {
                EntityEntry<InventoryEntity> InventoryCreated = await _bookStoreContext.Inventory.AddAsync(new InventoryEntity(inventoryViewModel));
                int InventorySaved = await _bookStoreContext.SaveChangesAsync();

                return InventorySaved > 0 ? InventoryCreated.Entity : throw new CreateException("um erro ocorreu ao cadastrar o inventário");
            }
            catch (Exception exception)
            {
                throw new Exception(exception.ToString());
            }
        }

        /// <summary>
        /// Atualiza os dados de um livro no sistema pelo seu id.
        /// </summary>
        /// <param name="bookId">O id do livro pesquisado.</param>
        /// <param name="bookViewModelUpdate">O modelo de livro contendo os dados a serem cadastrados.</param>
        /// <returns>O livro com dados atualizados.</returns>
        /// <exception cref="NotFound">Lançado quando o livro não é encontrado pelo id.</exception>
        /// <exception cref="UpdateException">Lançado quando ocorre um erro durante a atualização dos dados.</exception>
        /// <exception cref="Exception">Lançado quando ocorre um erro interno de servidor.</exception>
        public async Task<BookDTO> BookAtt(int bookId, BookViewModelUpdate bookViewModelUpdate)
        {
            try
            {
                BookEntity bookExists = await _bookStoreContext.Book.Include(book => book.Category).FirstOrDefaultAsync(u => u.Id == bookId) ?? throw new NotFound($"nenhum livro com o id: {bookId} encontrado.");

                _bookStoreContext.Entry(bookExists).CurrentValues.SetValues(bookViewModelUpdate);
                bookExists.UpdatedAt = DateTime.Now;

                int bookUpdated = await _bookStoreContext.SaveChangesAsync();

                return bookUpdated > 0 ? _mapper.Map<BookDTO>(bookExists) : throw new UpdateException("um erro ocorreu ao atualizar o livro");
            }
            catch (Exception exception)
            {
                throw new Exception(exception.ToString());
            }

        }

        /// <summary>
        /// Deleta um livro no sistema pelo seu id.
        /// </summary>
        /// <param name="bookId">O id do livro a ser deletado.</param>
        /// <returns>True se o livro for deletado com sucesso.</returns>
        /// <exception cref="NotFound">Lançado quando o livro não é encontrado pelo id.</exception>
        /// <exception cref="RemoveException">Lançado quando ocorre um erro durante a exclusão.</exception>
        /// <exception cref="Exception">Lançado quando ocorre um erro interno de servidor.</exception>
        public async Task<bool> BookDel(int bookId)
        {
            try
            {
                BookEntity bookExists = await _bookStoreContext.Book.Include(book => book.Category).FirstOrDefaultAsync(u => u.Id == bookId) ?? throw new NotFound($"nenhum livro com o id: {bookId} encontrado.");

                _bookStoreContext.Book.Remove(bookExists);

                int bookSuccessfullyRemoved = await _bookStoreContext.SaveChangesAsync();

                return bookSuccessfullyRemoved > 0 ? true : throw new RemoveException("um erro ocorreu ao remover o livro");
            }
            catch (Exception exception)
            {
                throw new Exception(exception.ToString());
            }

        }
        /// <summary>
        /// Busca um livro no sistema pelo seu id.
        /// </summary>
        /// <param name="bookId">O id do livro pesquisado.</param>
        /// <returns>O livro encontrado.</returns>
        /// <exception cref="Exception">Lançado quando ocorre um erro interno de servidor.</exception>
        public async Task<BookDTO> Book(int bookId)
        {
            try
            {
                BookEntity bookExists = await _bookStoreContext.Book.AsNoTracking().Include(book => book.Category).FirstOrDefaultAsync(u => u.Id == bookId) ?? throw new NotFound($"nenhum livro com o id: {bookId} encontrado.");

                return _mapper.Map<BookDTO>(bookExists);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.ToString());
            }

        }

        /// <summary>
        /// Busca todos livros cadastrados no sistema.
        /// </summary>
        /// <param name="categoryName">O nome da categoria para filtrar a listagem.</param>
        /// <returns>Lista vazia ou de livros cadastrados.</returns>
        /// <exception cref="Exception">Lançado quando ocorre um erro interno de servidor.</exception>
        public async Task<List<BookDTO>> Books(string? categoryName = null)
        {
            try
            {
                List<BookEntity> books = [];

                if (categoryName != null)
                {
                    CategoryEntity category = await _bookStoreContext.Category.FirstOrDefaultAsync(category => category.Name == categoryName) ?? throw new NotFound($"nenhuma categoria com o nome: {categoryName} encontrado.");
                    books = await _bookStoreContext.Book.AsNoTracking().Include(book => book.Category).Where(category => category.Category.Name == categoryName).ToListAsync();
                    return books.Count > 0 ? _mapper.Map<List<BookDTO>>(books) : [];
                }

                books = await _bookStoreContext.Book.Include(book => book.Category).AsNoTracking().ToListAsync();
                return books.Count > 0 ? _mapper.Map<List<BookDTO>>(books) : [];
            }
            catch (Exception exception)
            {
                throw new Exception(exception.ToString());
            }

        }

        /// <summary>
        /// Recupera um livro do banco de dados pelo título e retorna um DTO com as informações do livro.
        /// </summary>
        /// <param name="title">O título do livro a ser recuperado.</param>
        /// <returns>Um DTO contendo os detalhes do livro.</returns>
        /// <exception cref="NotFound">Lançado quando nenhum livro com o título especificado é encontrado.</exception>
        /// <exception cref="Exception">Lançado em caso de erro ao acessar o banco de dados.</exception>
        public async Task<BookDTO> BookByTitle(string title)
        {
            try
            {
                BookEntity book = await _bookStoreContext.Book
                    .AsNoTracking()
                    .Include(book => book.Category)
                    .FirstOrDefaultAsync(book => book.Title == title)
                    ?? throw new NotFound($"Nenhum livro com o título: {title} encontrado.");

                return _mapper.Map<BookDTO>(book);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.ToString());
            }
        }

    }
}