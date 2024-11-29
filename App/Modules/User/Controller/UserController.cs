using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_bookStore.App.Modules.User.DTO;
using api_bookStore.App.Modules.User.Interface;
using api_bookStore.App.Modules.User.ViewModel;
using api_BookStore.App.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace api_bookStore.App.Modules.User.Controller
{
    [Route("api")]
    [ApiController]
    public class UserController(IUserRepository userRepository) : ControllerBase
    {
        private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));

        /// <summary>
        /// Adiciona um novo usuário ao sistema.
        /// </summary>
        ///  <remarks>
        /// Exemplo:
        ///
        ///     POST /user
        ///     {
        ///         "name": "Joao da Silva",
        ///         "email": "joao@usuario.com",
        ///         "password": "MyPassword2025",
        ///         "role": "Admin ou Default"
        ///     }
        ///
        /// </remarks>
        /// <param name="userViewModelCreate">O modelo de usuário contendo os dados a serem cadastrados.</param>
        /// <returns>O usuário recém-adicionado.</returns>
        /// <response code="200">Retorna o usuário com sucesso.</response>
        /// <response code="400">Caso ocorra um erro de solicitação (ex: dados inválidos).</response>
        /// <response code="500">Se ocorrer um erro interno no servidor.</response>
        [ProducesResponseType(typeof(UserDTO), 200)]    // OK
        [ProducesResponseType(400)]                     // Bad Request : quando o userViewModel é nulo ou invalido.
        [ProducesResponseType(500)]                     // Internal Server Error
        [HttpPost("user")]
        public async Task<ActionResult<UserDTO>> UserAdd(UserViewModelCreate userViewModelCreate)
        {
            try
            {
                UserDTO user = await _userRepository.UserAdd(userViewModelCreate);

                return Ok(user);
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
        /// Busca todos usuários cadastrados no sistema.
        /// </summary>
        ///  <remarks>
        /// Exemplo:
        ///
        ///     GET /users
        ///     [
        ///         {
        ///             "name": "Joao da Silva 1",
        ///             "email": "joao@usuario.com",
        ///             "password": "MyPassword2025",
        ///             "role": "Default"
        ///         },
        ///         {
        ///             "name": "Joao da Silva 2",
        ///             "email": "joao@usuario.com",
        ///             "password": "MyPassword2025",
        ///             "role": "Default"
        ///         },
        ///         {
        ///             "name": "Joao da Silva 3",
        ///             "email": "joao@usuario.com",
        ///             "password": "MyPassword2025",
        ///             "role": "Admin"
        ///         },
        ///     ]
        ///
        /// </remarks>
        /// <returns>Lista de usuarios cadastrados.</returns>
        /// <response code="200">Retorna uma lista com usuários ou vazia com sucesso.</response>
        /// <response code="400">Caso ocorra um erro de solicitação (ex: dados inválidos).</response>
        /// <response code="500">Se ocorrer um erro interno no servidor.</response>
        [ProducesResponseType(typeof(List<UserDTO>), 200)]    // OK
        [ProducesResponseType(400)]                     // Bad Request : se o usuário enviar algum parâmetro que a rota nào espera.
        [ProducesResponseType(500)]                     // Internal Server Error
        [HttpGet("users")]
        public async Task<ActionResult<List<UserDTO>>> Users()
        {
            try
            {
                List<UserDTO> users = await _userRepository.Users();
                return users;
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
        /// Busca um usuário no sistema pelo seu id.
        /// </summary>
        ///  <remarks>
        /// Exemplo:
        ///
        ///     GET /user/id-do-usuario
        ///
        /// </remarks>
        /// <param name="userId">O id do usuário pesquisado.</param>
        /// <returns>O usuário encontrado.</returns>
        /// <response code="200">Retorna o usuário com sucesso.</response>
        /// <response code="400">Caso ocorra um erro de solicitação (ex: dados inválidos).</response>
        /// <response code="404">Caso não encontra o usuário.</response>
        /// <response code="500">Se ocorrer um erro interno no servidor.</response>
        [ProducesResponseType(typeof(UserDTO), 200)]    // OK
        [ProducesResponseType(400)]                     // Bad Request : quando o userViewModel é nulo ou invalido.
        [ProducesResponseType(404)]                     // Not Found
        [ProducesResponseType(500)]                     // Internal Server Error
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<UserDTO>> UserById([FromRoute] string userId)
        {
            try
            {
                UserDTO user = await _userRepository.User(userId);
                return Ok(user);
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
        /// Atualiza os dados de um usuário no sistema pelo seu id.
        /// </summary>
        ///  <remarks>
        /// Exemplo:
        ///
        ///     PATCH /user/id-do-usuario
        ///     {
        ///         "name": "Joao da Silva",
        ///         "email": "joao@usuario.com",
        ///         "password": "MyPassword2025",
        ///         "role": "Admin ou Default"
        ///     }
        ///
        /// </remarks>
        /// <param name="userId">O id do usuário pesquisado.</param>
        /// <param name="userViewModelUpdate">O modelo de usuário contendo os dados a serem cadastrados.</param>
        /// <returns>O usuário com dados atualizados.</returns>
        /// <response code="200">Retorna o usuário atualizado com sucesso.</response>
        /// <response code="400">Caso ocorra um erro de solicitação (ex: dados inválidos).</response>
        /// <response code="404">Caso não encontra o usuário.</response>
        /// <response code="422">Caso ocorra um problema durante a atualização.</response>
        /// <response code="500">Se ocorrer um erro interno no servidor.</response>
        [ProducesResponseType(typeof(UserDTO), 200)]    // OK
        [ProducesResponseType(400)]                     // Bad Request : quando o userViewModel é nulo ou invalido.
        [ProducesResponseType(404)]                     // Not Found
        [ProducesResponseType(422)]                     // Update Exception
        [ProducesResponseType(500)]                     // Internal Server Error
        [HttpPatch("user/{userId}")]
        public async Task<ActionResult<UserDTO>> UserAtt([FromRoute] string userId, UserViewModelUpdate userViewModelUpdate)
        {
            try
            {
                return await _userRepository.UserAtt(userId, userViewModelUpdate);
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
        /// Deleta um usuário no sistema pelo seu id.
        /// </summary>
        ///  <remarks>
        /// Exemplo:
        ///
        ///     Delete /user/id-do-usuario
        ///
        /// </remarks>
        /// <param name="userId">O id do usuário a ser deletado.</param>
        /// <returns>True ou False.</returns>
        /// <response code="200">Retorna true se o usuário for deletado com sucesso.</response>
        /// <response code="400">Caso ocorra um erro de solicitação (ex: dados inválidos).</response>
        /// <response code="404">Caso não encontra o usuário.</response>
        /// <response code="422">Caso ocorra um problema durante a exclusão.</response>
        /// <response code="500">Se ocorrer um erro interno no servidor.</response>
        [ProducesResponseType(typeof(UserDTO), 200)]    // OK
        [ProducesResponseType(400)]                     // Bad Request
        [ProducesResponseType(404)]                     // Not Found
        [ProducesResponseType(422)]                     // Remove Exception
        [ProducesResponseType(500)]                     // Internal Server Error
        [HttpDelete("user/{userId}")]
        public async Task<ActionResult<bool>> UserDel([FromRoute] string userId)
        {
            try
            {
                bool userRemoved = await _userRepository.UserDel(userId);

                return Ok(userRemoved);
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