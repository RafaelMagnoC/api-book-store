using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_bookStore.App.Modules.Sale.DTO;
using api_bookStore.App.Modules.Sale.Entity;
using api_bookStore.App.Modules.Sale.Interface;
using api_bookStore.App.Modules.Sale.ViewModel;
using api_BookStore.App.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api_bookStore.App.Modules.Sale.Controller
{
    [Authorize]
    [ApiController]
    [Route("api")]
    public class SaleController(ISaleRepository saleRepository) : ControllerBase
    {
        private readonly ISaleRepository _saleRepository = saleRepository ?? throw new ArgumentNullException(nameof(saleRepository));

        /// <summary>
        /// Registra uma nova venda.
        /// </summary>
        ///  <remarks>
        /// Exemplo:
        ///
        ///     POST /sale
        ///     [
        ///         {
        ///             "bookId": 1,
        ///             "quantity: 3
        ///         }
        ///     ]
        ///
        /// </remarks>
        /// <param name="saleViewModelCreate">Os ids dos livros e quantidades que serão comprados pelo cliente.</param>
        /// <returns>a categoria recém-adicionado.</returns>
        /// <response code="200">Retorna a categoria com sucesso.</response>
        /// <response code="400">Caso ocorra um erro de solicitação (ex: dados inválidos).</response>
        /// <response code="500">Se ocorrer um erro interno no servidor.</response>
        [ProducesResponseType(typeof(SaleDTO), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpPost("sale")]
        public async Task<ActionResult<SaleDTO>> SaleAdd(List<SaleViewModelCreate> saleViewModelCreate)
        {
            try
            {
                SaleDTO sale = await _saleRepository.SaleAdd(saleViewModelCreate);

                return Ok(sale);
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
        /// Busca todas as vendas registradas no sistema.
        /// </summary>
        ///  <remarks>
        /// Exemplo:
        ///
        ///     GET /categories
        ///     [
        ///         {
        ///             "id": 1
        ///         },
        ///         {
        ///             "id": 2
        ///         },
        ///         {
        ///             "id": 3
        ///         },
        ///     ]
        ///
        /// </remarks>
        /// <returns>Lista de vendas registradas.</returns>
        /// <response code="200">Retorna uma lista com todas vendas ou vazia com sucesso.</response>
        /// <response code="400">Caso ocorra um erro de solicitação (ex: dados inválidos).</response>
        /// <response code="500">Se ocorrer um erro interno no servidor.</response>
        [ProducesResponseType(typeof(List<SaleDTO>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpGet("sales")]
        public async Task<ActionResult<List<SaleDTO>>> Categories()
        {
            try
            {
                List<SaleDTO> sales = await _saleRepository.Sales();
                return Ok(sales);
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
    }
}