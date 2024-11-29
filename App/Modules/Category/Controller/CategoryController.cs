using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_bookStore.App.Modules.Category.DTO;
using api_bookStore.App.Modules.Category.Interface;
using api_bookStore.App.Modules.Category.ViewModel;
using api_BookStore.App.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api_bookStore.App.Modules.Category.Controller
{
    [Authorize(Roles = "Admin")]
    [Route("api")]
    [ApiController]
    public class CategoryController(ICategoryRepository categoryRepository) : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));

        /// <summary>
        /// Adiciona uma nova categoria ao sistema.
        /// </summary>
        ///  <remarks>
        /// Exemplo:
        ///
        ///     POST /category
        ///     {
        ///         "name": "Aventura",
        ///         "description": "Livros de aventura são repletos de emoção, desafios e viagens inesperadas."
        ///     }
        ///
        /// </remarks>
        /// <param name="categoryViewModelCreate">O modelo de categoria contendo os dados a serem cadastradas.</param>
        /// <returns>a categoria recém-adicionado.</returns>
        /// <response code="200">Retorna a categoria com sucesso.</response>
        /// <response code="400">Caso ocorra um erro de solicitação (ex: dados inválidos).</response>
        /// <response code="500">Se ocorrer um erro interno no servidor.</response>
        [ProducesResponseType(typeof(CategoryDTO), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpPost("category")]
        public async Task<ActionResult<CategoryDTO>> CategoryAdd(CategoryViewModelCreate categoryViewModelCreate)
        {
            try
            {
                CategoryDTO category = await _categoryRepository.CategoryAdd(categoryViewModelCreate);

                return Ok(category);
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
        /// Busca todas categorias cadastradas no sistema.
        /// </summary>
        ///  <remarks>
        /// Exemplo:
        ///
        ///     GET /categories
        ///     [
        ///         {
        ///             "name": "Aventura",
        ///             "description": "Livros de aventura são repletos de emoção, desafios e viagens inesperadas."
        ///         },
        ///         {
        ///             "name": "Ficção",
        ///             "description": "A ficção cria universos imaginários e histórias que desafiam a realidade."
        ///         },
        ///         {
        ///             "name": "Romance",
        ///             "description": "Livros de romance focam nas relações humanas, em especial nas paixões e emoções intensas."
        ///         },
        ///     ]
        ///
        /// </remarks>
        /// <returns>Lista de categorias cadastradas.</returns>
        /// <response code="200">Retorna uma lista com todas categoriaes ou vazia com sucesso.</response>
        /// <response code="400">Caso ocorra um erro de solicitação (ex: dados inválidos).</response>
        /// <response code="500">Se ocorrer um erro interno no servidor.</response>
        [ProducesResponseType(typeof(List<CategoryDTO>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpGet("categories")]
        public async Task<ActionResult<List<CategoryDTO>>> Categories()
        {
            try
            {
                List<CategoryDTO> categories = await _categoryRepository.Categories();
                return Ok(categories);
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
        /// Busca uma categoria no sistema pelo seu id.
        /// </summary>
        ///  <remarks>
        /// Exemplo:
        ///
        ///     GET /category/id-da-categoria
        ///
        /// </remarks>
        /// <param name="categoryId">O id da categoria pesquisada.</param>
        /// <returns>A categoria encontrada.</returns>
        /// <response code="200">Retorna a categoria com sucesso.</response>
        /// <response code="400">Caso ocorra um erro de solicitação (ex: dados inválidos).</response>
        /// <response code="404">Caso não encontre a categoria.</response>
        /// <response code="500">Se ocorrer um erro interno no servidor.</response>
        [ProducesResponseType(typeof(CategoryDTO), 200)]    // OK
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<CategoryDTO>> Category([FromRoute] int categoryId)
        {
            try
            {
                CategoryDTO category = await _categoryRepository.Category(categoryId);
                return Ok(category);
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
        /// Atualiza os dados de uma categoria no sistema pelo seu id.
        /// </summary>
        ///  <remarks>
        /// Exemplo:
        ///
        ///     PATCH /category/id-da-categoria
        ///     {
        ///         "name": "Aventura",
        ///         "description": "Livros de aventura são repletos de emoção, desafios e viagens inesperadas."
        ///     }
        ///
        /// </remarks>
        /// <param name="categoryId">O id da categoria pesquisada.</param>
        /// <param name="categoryViewModelUpdate">O modelo de categoria contendo os dados a serem cadastrados.</param>
        /// <returns>a categoria com dados atualizados.</returns>
        /// <response code="200">Retorna a categoria atualizada com sucesso.</response>
        /// <response code="400">Caso ocorra um erro de solicitação (ex: dados inválidos).</response>
        /// <response code="404">Caso não encontre a categoria.</response>
        /// <response code="422">Caso ocorra um problema durante a atualização.</response>
        /// <response code="500">Se ocorrer um erro interno no servidor.</response>
        [ProducesResponseType(typeof(CategoryDTO), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        [HttpPatch("category/{categoryId}")]
        public async Task<ActionResult<CategoryDTO>> CategoryAtt([FromRoute] int categoryId, CategoryViewModelUpdate categoryViewModelUpdate)
        {
            try
            {
                CategoryDTO category = await _categoryRepository.CategoryAtt(categoryId, categoryViewModelUpdate);
                return Ok(category);
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
        /// Deleta uma categoria no sistema pelo seu id.
        /// </summary>
        ///  <remarks>
        /// Exemplo:
        ///
        ///     Delete /category/id-da-categoria
        ///
        /// </remarks>
        /// <param name="categoryId">O id da categoria a ser deletada.</param>
        /// <returns>True ou False.</returns>
        /// <response code="200">Retorna true se a categoria for deletada com sucesso.</response>
        /// <response code="400">Caso ocorra um erro de solicitação (ex: dados inválidos).</response>
        /// <response code="404">Caso não encontre a categoria.</response>
        /// <response code="422">Caso ocorra um problema durante a exclusão.</response>
        /// <response code="500">Se ocorrer um erro interno no servidor.</response>
        [ProducesResponseType(typeof(CategoryDTO), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        [HttpDelete("category/{categoryId}")]
        public async Task<ActionResult<bool>> CategoryDel([FromRoute] int categoryId)
        {
            try
            {
                bool categoryRemoved = await _categoryRepository.CategoryDel(categoryId);

                return Ok(categoryRemoved);
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