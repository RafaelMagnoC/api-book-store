using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_bookStore.App.Modules.Auth.Interface;
using api_bookStore.App.Modules.Auth.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace api_bookStore.App.Modules.Auth.Controller
{
    /// <summary>
    /// Controlador responsável pelas operações de autenticação de usuários.
    /// Inclui ações como realizar login e gerar token de autenticação.
    /// </summary>
    /// <remarks>
    /// Inicializa uma nova instância do controlador de autenticação.
    /// </remarks>
    /// <param name="authRepository">O repositório de autenticação para operações de login e token.</param>
    [ApiController]
    [Route("api")]
    public class AuthController(IAuthRepository authRepository) : ControllerBase
    {
        private readonly IAuthRepository _authRepository = authRepository ?? throw new ArgumentNullException(nameof(authRepository));

        /// <summary>
        /// Realiza o login do usuário e gera um token de autenticação.
        /// </summary>
        /// <param name="authEntity">O modelo de autenticação com as credenciais do usuário.</param>
        /// <returns>Um token JWT que será usado para autenticação em requisições subsequentes.</returns>
        /// <response code="200">Retorna o token de autenticação gerado.</response>
        /// <response code="400">Caso as credenciais sejam inválidas ou algum erro ocorra.</response>
        [HttpPost("singIn")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<string?>> SingIn(AuthViewModel authEntity)
        {
            if (authEntity == null)
            {
                return BadRequest("Credenciais inválidas.");
            }

            string? token = await _authRepository.SigIn(authEntity);

            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("Credenciais inválidas.");
            }

            return Ok(token);
        }
    }
}
