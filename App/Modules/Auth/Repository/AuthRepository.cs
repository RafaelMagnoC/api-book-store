using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_bookStore.App.DataBase;
using api_bookStore.App.Modules.Auth.Interface;
using api_bookStore.App.Modules.Auth.ViewModel;
using api_bookStore.App.Modules.User.Entity;
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
    public class AuthRepository : IAuthRepository
    {
        private readonly BookStoreContext _bookStoreContext;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Inicializa uma nova instância do repositório de autenticação.
        /// </summary>
        /// <param name="bookStoreContext">O contexto do banco de dados para acessar as entidades do usuário.</param>
        /// <param name="configuration">Configurações da aplicação, como chaves de JWT.</param>
        public AuthRepository(BookStoreContext bookStoreContext, IConfiguration configuration)
        {
            _bookStoreContext = bookStoreContext ?? throw new ArgumentNullException(nameof(bookStoreContext));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

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
                UserEntity user = await _bookStoreContext.User
                    .FirstOrDefaultAsync(u => u.Email == authViewModel.Email)
                    ?? throw new NotFound($"Nenhum usuário com o e-mail: {authViewModel.Email} encontrado.");

                string userPassword = user.Password
                    ?? throw new InvalidCredential("Credenciais inválidas.");

                if (PasswordServiceHash.VerifyPassword(userPassword, authViewModel.Password))
                {
                    return JwtTokenService.GenerateJwtToken(_configuration, user.Id.ToString(), user.Role.ToString());
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
