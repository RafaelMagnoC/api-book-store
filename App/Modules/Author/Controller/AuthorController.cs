using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_bookStore.App.Modules.Author.DTO;
using api_bookStore.App.Modules.Author.Interface;
using api_bookStore.App.Modules.Author.ViewModel;
using api_BookStore.App.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api_bookStore.App.Modules.Author.Controller
{
    [Authorize(Roles = "Admin")]
    [Route("api")]
    [ApiController]
    public class AuthorController(IAuthorRepository authorRepository) : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository = authorRepository ?? throw new ArgumentNullException(nameof(authorRepository));

        /// <summary>
        /// Adiciona um novo autor ao sistema.
        /// </summary>
        ///  <remarks>
        /// Exemplo:
        ///
        ///     POST /author
        ///     {
        ///         "id": 1,
        ///         "name": "Joao da Silva",
        ///         "birthday": "1990-01-01",
        ///         "country": "Brazil",
        ///     }
        ///
        /// </remarks>
        /// <param name="authorViewModelCreate">O modelo de autor contendo os dados a serem cadastrados.</param>
        /// <returns>O autor recém-adicionado.</returns>
        /// <response code="200">Retorna o autor com sucesso.</response>
        /// <response code="400">Caso ocorra um erro de solicitação (ex: dados inválidos).</response>
        /// <response code="500">Se ocorrer um erro interno no servidor.</response>
        [ProducesResponseType(typeof(AuthorDTO), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpPost("author")]
        public async Task<ActionResult<AuthorDTO>> AuthorAdd(AuthorViewModelCreate authorViewModelCreate)
        {
            try
            {
                AuthorDTO author = await _authorRepository.AuthorAdd(authorViewModelCreate);

                return Ok(author);
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
        /// Busca todos autores cadastrados no sistema.
        /// </summary>
        ///  <remarks>
        /// Exemplo:
        ///
        ///     GET /authors
        ///     [
        ///         {
        ///             "id": 1,
        ///             "name": "Joao da Silva 1",
        ///             "birthday": "1990-01-01",
        ///             "country": "Brazil",
        ///         },
        ///         {
        ///             "id": 1,
        ///             "name": "Joao da Silva 2",
        ///             "birthday": "1990-01-01",
        ///             "country": "Brazil",
        ///         },
        ///         {
        ///             "id": 1,
        ///             "name": "Joao da Silva 3",
        ///             "birthday": "1990-01-01",
        ///             "country": "Brazil",
        ///         },
        ///     ]
        ///
        /// </remarks>
        /// <returns>Lista de autors cadastrados.</returns>
        /// <response code="200">Retorna uma lista com todos autores ou vazia com sucesso.</response>
        /// <response code="400">Caso ocorra um erro de solicitação (ex: dados inválidos).</response>
        /// <response code="500">Se ocorrer um erro interno no servidor.</response>
        [ProducesResponseType(typeof(List<AuthorDTO>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpGet("authors")]
        public async Task<ActionResult<List<AuthorDTO>>> Authors()
        {
            try
            {
                List<AuthorDTO> authors = await _authorRepository.Authors();
                return Ok(authors);
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
        /// Busca um autor no sistema pelo seu id.
        /// </summary>
        ///  <remarks>
        /// Exemplo:
        ///
        ///     GET /author/id-do-autor
        ///
        /// </remarks>
        /// <param name="authorId">O id do autor pesquisado.</param>
        /// <returns>O autor encontrado.</returns>
        /// <response code="200">Retorna o autor com sucesso.</response>
        /// <response code="400">Caso ocorra um erro de solicitação (ex: dados inválidos).</response>
        /// <response code="404">Caso não encontra o autor.</response>
        /// <response code="500">Se ocorrer um erro interno no servidor.</response>
        [ProducesResponseType(typeof(AuthorDTO), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [HttpGet("author/{authorId}")]
        public async Task<ActionResult<AuthorDTO>> Author([FromRoute] int authorId)
        {
            try
            {
                AuthorDTO author = await _authorRepository.Author(authorId);
                return Ok(author);
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
        /// Atualiza os dados de um autor no sistema pelo seu id.
        /// </summary>
        ///  <remarks>
        /// Exemplo:
        ///
        ///     PATCH /author/id-do-autor
        ///     {
        ///         "id": 1,
        ///         "name": "Joao da Silva",
        ///         "birthday": "1990-01-01",
        ///         "country": "Brazil",
        ///     }
        ///
        /// </remarks>
        /// <param name="authorId">O id do autor pesquisado.</param>
        /// <param name="authorViewModelUpdate">O modelo de autor contendo os dados a serem cadastrados.</param>
        /// <returns>O autor com dados atualizados.</returns>
        /// <response code="200">Retorna o autor atualizado com sucesso.</response>
        /// <response code="400">Caso ocorra um erro de solicitação (ex: dados inválidos).</response>
        /// <response code="404">Caso não encontra o autor.</response>
        /// <response code="422">Caso ocorra um problema durante a atualização.</response>
        /// <response code="500">Se ocorrer um erro interno no servidor.</response>
        [ProducesResponseType(typeof(AuthorDTO), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        [HttpPatch("author/{authorId}")]
        public async Task<ActionResult<AuthorDTO>> AuthorAtt([FromRoute] int authorId, AuthorViewModelUpdate authorViewModelUpdate)
        {
            try
            {
                AuthorDTO author = await _authorRepository.AuthorAtt(authorId, authorViewModelUpdate);
                return Ok(author);
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
        /// Deleta um autor no sistema pelo seu id.
        /// </summary>
        ///  <remarks>
        /// Exemplo:
        ///
        ///     Delete /author/id-do-autor
        ///
        /// </remarks>
        /// <param name="authorId">O id do autor a ser deletado.</param>
        /// <returns>True ou False.</returns>
        /// <response code="200">Retorna true se o autor for deletado com sucesso.</response>
        /// <response code="400">Caso ocorra um erro de solicitação (ex: dados inválidos).</response>
        /// <response code="404">Caso não encontra o autor.</response>
        /// <response code="422">Caso ocorra um problema durante a exclusão.</response>
        /// <response code="500">Se ocorrer um erro interno no servidor.</response>
        [ProducesResponseType(typeof(AuthorDTO), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        [HttpDelete("author/{authorId}")]
        public async Task<ActionResult<bool>> AuthorDel([FromRoute] int authorId)
        {
            try
            {
                bool authorRemoved = await _authorRepository.AuthorDel(authorId);

                return Ok(authorRemoved);
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