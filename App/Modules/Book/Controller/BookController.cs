using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_bookStore.App.Modules.Book.DTO;
using api_bookStore.App.Modules.Book.Interface;
using api_bookStore.App.Modules.Book.ViewModel;
using api_BookStore.App.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api_bookStore.App.Modules.Book.Controller
{
    [Authorize(Roles = "Admin")]
    [Route("api")]
    [ApiController]
    public class BookController(IBookRepository BookRepository) : ControllerBase
    {
        private readonly IBookRepository _BookRepository = BookRepository ?? throw new ArgumentNullException(nameof(BookRepository));

        /// <summary>
        /// Adiciona um novo livro ao sistema.
        /// </summary>
        ///  <remarks>
        /// Exemplo:
        ///
        ///     POST /book
        ///     {
        ///         "title": "Star wars",
        ///         "publicationDate": "1990-01-01",
        ///         "price": 99.90,
        ///         "quantity": 5,
        ///         "authorId": 1,
        ///         "categoryId": 0
        ///     }
        ///
        /// </remarks>
        /// <param name="bookViewModelCreate">O modelo de livro que será adicionado.</param>
        /// <returns>O livro recém-adicionado.</returns>
        /// <response code="200">Retorna o livro com sucesso.</response>
        /// <response code="400">Caso ocorra um erro de solicitação (ex: dados inválidos).</response>
        /// <response code="500">Se ocorrer um erro interno no servidor.</response>
        [ProducesResponseType(typeof(BookDTO), 200)]    // OK
        [ProducesResponseType(400)]                     // Bad Request : quando o BookViewModel é nulo ou invalido.
        [ProducesResponseType(500)]                     // Internal Server Error
        [HttpPost("book")]
        public async Task<ActionResult<BookDTO>> BookAdd(BookViewModelCreate bookViewModelCreate)
        {
            try
            {
                BookDTO book = await _BookRepository.BookAdd(bookViewModelCreate);

                return Ok(book);
            }
            catch (CreateException exception)
            {
                throw new CreateException(exception.ToString());
            }
            catch (BadRequest exception)
            {
                throw new BadRequest(exception.ToString());
            }
            catch (Exception exception)
            {
                throw new Exception(exception.ToString());
            }
        }

        /// <summary>
        /// Busca todos livros cadastrados no sistema.
        /// </summary>
        ///  <remarks>
        /// Exemplo:
        ///
        ///     GET /books
        ///     [
        ///         {
        ///             "title": "Star wars",
        ///             "publicationDate": "1990-01-01",
        ///             "price": 99.90,
        ///             "quantity": 5,
        ///             "authorId": 1,
        ///             "categoryId": 0
        ///         },
        ///         {
        ///             "title": "A bela e a fera",
        ///             "publicationDate": "1990-01-01",
        ///             "price": 99.90,
        ///             "quantity": 6,
        ///             "authorId": 1,
        ///             "categoryId": 0
        ///         },
        ///         {
        ///             "title": "Star wars",
        ///             "publicationDate": "1990-01-01",
        ///             "price": 99.90,
        ///             "quantity": 5,
        ///             "authorId": 1,
        ///             "categoryId": 0
        ///         }
        ///     ]
        ///
        /// </remarks>
        /// <param name="categoryName">O nome da categoria para filtrar a listagem.</param>
        /// <returns>Lista de livros cadastrados.</returns>
        /// <response code="200">Retorna uma lista com todos livros ou vazia com sucesso.</response>
        /// <response code="400">Caso ocorra um erro de solicitação (ex: dados inválidos).</response>
        /// <response code="500">Se ocorrer um erro interno no servidor.</response>
        [ProducesResponseType(typeof(List<BookDTO>), 200)]    // OK
        [ProducesResponseType(400)]                     // Bad Request : se o livro enviar algum parâmetro que a rota nào espera.
        [ProducesResponseType(500)]                     // Internal Server Error
        [HttpGet("books")]
        public async Task<ActionResult<List<BookDTO>>> Books([FromQuery] string? categoryName)
        {
            try
            {
                List<BookDTO> books = await _BookRepository.Books(categoryName);
                return Ok(books);
            }
            catch (BadRequest exception)
            {
                throw new BadRequest(exception.ToString());
            }
            catch (Exception exception)
            {
                throw new Exception(exception.ToString());
            }
        }

        /// <summary>
        /// Busca um livro no sistema pelo seu id.
        /// </summary>
        ///  <remarks>
        /// Exemplo:
        ///
        ///     GET /book/id-do-livro
        ///
        /// </remarks>
        /// <param name="bookId">O id do livro pesquisado.</param>
        /// <returns>O livro encontrado.</returns>
        /// <response code="200">Retorna o livro com sucesso.</response>
        /// <response code="400">Caso ocorra um erro de solicitação (ex: dados inválidos).</response>
        /// <response code="404">Caso não encontra o livro.</response>
        /// <response code="500">Se ocorrer um erro interno no servidor.</response>
        [ProducesResponseType(typeof(BookDTO), 200)]    // OK
        [ProducesResponseType(400)]                     // Bad Request : quando o BookViewModel é nulo ou invalido.
        [ProducesResponseType(404)]                     // Not Found
        [ProducesResponseType(500)]                     // Internal Server Error
        [HttpGet("book/{bookId}")]
        public async Task<ActionResult<BookDTO>> Book([FromRoute] int bookId)
        {
            try
            {
                BookDTO book = await _BookRepository.Book(bookId);
                return Ok(book);
            }
            catch (BadRequest exception)
            {
                throw new BadRequest(exception.ToString());
            }
            catch (NotFound exception)
            {
                throw new NotFound(exception.ToString());
            }
            catch (Exception exception)
            {
                throw new Exception(exception.ToString());
            }

        }

        /// <summary>
        /// Busca um livro no sistema pelo seu título.
        /// </summary>
        ///  <remarks>
        /// Exemplo:
        ///
        ///     GET /book/titulo-do-livro
        ///
        /// </remarks>
        /// <param name="title">O título do livro pesquisado.</param>
        /// <returns>O livro encontrado.</returns>
        /// <response code="200">Retorna o livro com sucesso.</response>
        /// <response code="400">Caso ocorra um erro de solicitação (ex: dados inválidos).</response>
        /// <response code="404">Caso não encontra o livro.</response>
        /// <response code="500">Se ocorrer um erro interno no servidor.</response>
        [ProducesResponseType(typeof(BookDTO), 200)]    // OK
        [ProducesResponseType(400)]                     // Bad Request : quando o BookViewModel é nulo ou invalido.
        [ProducesResponseType(404)]                     // Not Found
        [ProducesResponseType(500)]                     // Internal Server Error
        [HttpGet("bookByTitle/{title}")]
        public async Task<ActionResult<BookDTO>> BookByTitle([FromRoute] string title)
        {
            try
            {
                BookDTO book = await _BookRepository.BookByTitle(title);
                return Ok(book);
            }
            catch (BadRequest exception)
            {
                throw new BadRequest(exception.ToString());
            }
            catch (NotFound exception)
            {
                throw new NotFound(exception.ToString());
            }
            catch (Exception exception)
            {
                throw new Exception(exception.ToString());
            }

        }

        /// <summary>
        /// Atualiza os dados de um livro no sistema pelo seu id.
        /// </summary>
        ///  <remarks>
        /// Exemplo:
        ///
        ///     PATCH /book/id-do-livro
        ///     {
        ///         "title": "Star wars",
        ///         "publicationDate": "1990-01-01",
        ///         "price": 99.90,
        ///         "quantity": 5,
        ///         "authorId": 1,
        ///         "categoryId": 0
        ///     }
        ///
        /// </remarks>
        /// <param name="bookId">O id do livro pesquisado.</param>
        /// <param name="bookViewModelUpdate">O modelo de livro contendo os dados a serem cadastrados.</param>
        /// <returns>O livro com dados atualizados.</returns>
        /// <response code="200">Retorna o livro atualizado com sucesso.</response>
        /// <response code="400">Caso ocorra um erro de solicitação (ex: dados inválidos).</response>
        /// <response code="404">Caso não encontra o livro.</response>
        /// <response code="422">Caso ocorra um problema durante a atualização.</response>
        /// <response code="500">Se ocorrer um erro interno no servidor.</response>
        [ProducesResponseType(typeof(BookDTO), 200)]    // OK
        [ProducesResponseType(400)]                     // Bad Request : quando o BookViewModel é nulo ou invalido.
        [ProducesResponseType(404)]                     // Not Found
        [ProducesResponseType(422)]                     // Update Exception
        [ProducesResponseType(500)]                     // Internal Server Error
        [HttpPatch("book/{bookId}")]
        public async Task<ActionResult<BookDTO>> BookAtt([FromRoute] int bookId, BookViewModelUpdate bookViewModelUpdate)
        {
            try
            {
                BookDTO book = await _BookRepository.BookAtt(bookId, bookViewModelUpdate);
                return Ok(book);
            }
            catch (UpdateException exception)
            {
                throw new UpdateException(exception.ToString());
            }
            catch (NotFound exception)
            {
                throw new NotFound(exception.ToString());
            }
            catch (BadRequest exception)
            {
                throw new BadRequest(exception.ToString());
            }
            catch (Exception exception)
            {
                throw new Exception(exception.ToString());
            }
        }

        /// <summary>
        /// Deleta um livro no sistema pelo seu id.
        /// </summary>
        ///  <remarks>
        /// Exemplo:
        ///
        ///     Delete /book/id-do-livro
        ///
        /// </remarks>
        /// <param name="bookId">O id do livro a ser deletado.</param>
        /// <returns>True ou False.</returns>
        /// <response code="200">Retorna true se o livro for deletado com sucesso.</response>
        /// <response code="400">Caso ocorra um erro de solicitação (ex: dados inválidos).</response>
        /// <response code="404">Caso não encontra o livro.</response>
        /// <response code="422">Caso ocorra um problema durante a exclusão.</response>
        /// <response code="500">Se ocorrer um erro interno no servidor.</response>
        [ProducesResponseType(typeof(BookDTO), 200)]    // OK
        [ProducesResponseType(400)]                     // Bad Request
        [ProducesResponseType(404)]                     // Not Found
        [ProducesResponseType(422)]                     // Remove Exception
        [ProducesResponseType(500)]                     // Internal Server Error
        [HttpDelete("book/{bookId}")]
        public async Task<ActionResult<bool>> BookDel([FromRoute] int bookId)
        {
            try
            {
                bool bookRemoved = await _BookRepository.BookDel(bookId);

                return Ok(bookRemoved);
            }
            catch (NotFound exception)
            {
                throw new NotFound(exception.ToString());
            }
            catch (BadRequest exception)
            {
                throw new BadRequest(exception.ToString());
            }
            catch (RemoveException exception)
            {
                throw new RemoveException(exception.ToString());
            }
            catch (Exception exception)
            {
                throw new Exception(exception.ToString());
            }
        }
    }
}