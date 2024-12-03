using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_bookStore.App.DataBase;
using api_bookStore.App.Modules.Auth.Interface;
using api_bookStore.App.Modules.Auth.ViewModel;
using api_bookStore.App.Modules.User.Entity;
using api_bookStore.App.Modules.User.Interface;
using api_bookStore.App.Modules.User.Service;
using api_bookStore.App.Services.Jwt;
using api_BookStore.App.Exceptions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace api_bookStore.App.Modules.Auth.Repository
{
    /// <summary>
    /// Repositório responsável pelas operações de autenticação de usuários, como login e verificação de credenciais.
    /// </summary>
    /// <remarks>
    /// Inicializa uma nova instância do repositório de autenticação.
    /// </remarks>
    /// <param name="userRepository">O repositorio de usuários</param>
    /// <param name="configuration">Configurações da aplicação, como chaves de JWT.</param>
    /// <param name="passwordServiceHash">O Recurso de math para senhas criptografadas.</param>
    /// <param name="jwtTokenService">O Recurso de geração de tokens Jwt</param>
    public class AuthRepository(IUserRepository userRepository, IConfiguration configuration, IPasswordServiceHash passwordServiceHash, IJwtTokenService jwtTokenService) : IAuthRepository
    {
        private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        private readonly IConfiguration _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        private readonly IPasswordServiceHash _passwordServiceHash = passwordServiceHash ?? throw new ArgumentNullException(nameof(passwordServiceHash));
        private readonly IJwtTokenService _jwtTokenService = jwtTokenService ?? throw new ArgumentNullException(nameof(jwtTokenService));


        /// <summary>
        /// Realiza o login de um usuário, verificando as credenciais e gerando um token JWT.
        /// </summary>
        /// <param name="authViewModel">O modelo contendo as credenciais do usuário (email e senha).</param>
        /// <returns>Um token JWT para autenticação.</returns>
        /// <exception cref="NotFound">Lançado quando o usuário com o email fornecido não é encontrado.</exception>
        /// <exception cref="InvalidCredential">Lançado quando as credenciais fornecidas são inválidas.</exception>
        public async Task<string> SigIn(AuthViewModel authViewModel)
        {
            try
            {
                (string email, string password) = authViewModel;

                UserEntity user = await _userRepository.UserByEmail(email);

                string userPassword = user.Password ?? string.Empty;

                if (_passwordServiceHash.VerifyPassword(userPassword, authViewModel.Password))
                {
                    return _jwtTokenService.GenerateJwtToken(user.Id.ToString(), user.Role.ToString());
                }
                else
                {
                    throw new InvalidCredential("Credenciais inválidas.");
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.ToString());
            }
        }
    }
}
