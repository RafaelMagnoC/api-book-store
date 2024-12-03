using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_bookStore.App.Modules.Auth.Interface;
using api_bookStore.App.Modules.Auth.Repository;
using api_bookStore.App.Modules.Author.Interface;
using api_bookStore.App.Modules.Author.Repository;
using api_bookStore.App.Modules.Book.Interface;
using api_bookStore.App.Modules.Book.Repository;
using api_bookStore.App.Modules.Category.Interface;
using api_bookStore.App.Modules.Category.Repository;
using api_bookStore.App.Modules.Inventory.Interface;
using api_bookStore.App.Modules.Inventory.Repository;
using api_bookStore.App.Modules.Sale.Interface;
using api_bookStore.App.Modules.Sale.Repository;
using api_bookStore.App.Modules.User.Interface;
using api_bookStore.App.Modules.User.Repository;
using api_bookStore.App.Modules.User.Service;
using api_bookStore.App.Services.Jwt;

namespace api_bookStore.App.Config
{
    /// <summary>
    /// Configuração de serviços transientes para a aplicação.
    /// Este serviço configura as dependências que serão injetadas como transientes na aplicação.
    /// </summary>
    public static class TransientConfig
    {
        /// <summary>
        /// Adiciona as dependências transientes à coleção de serviços.
        /// Registra as interfaces e suas implementações para que sejam injetadas como transientes na aplicação.
        /// </summary>
        /// <param name="services">A coleção de serviços onde as dependências serão registradas.</param>
        /// <returns>A coleção de serviços com as dependências transientes registradas.</returns>
        /// <remarks>
        /// Este método registra as interfaces e suas respectivas implementações como serviços transientes, ou seja, 
        /// uma nova instância será criada cada vez que o serviço for solicitado:
        /// - <see cref="IUserRepository"/> e <see cref="UserRepository"/>
        /// - <see cref="IAuthRepository"/> e <see cref="AuthRepository"/>
        /// - <see cref="IAuthorRepository"/> e <see cref="AuthorRepository"/>
        /// - <see cref="ICategoryRepository"/> e <see cref="CategoryRepository"/>
        /// - <see cref="IInventoryRepository"/> e <see cref="InventoryRepository"/>
        /// - <see cref="IBookRepository"/> e <see cref="BookRepository"/>
        /// - <see cref="ISaleRepository"/> e <see cref="SaleRepository"/>
        /// </remarks>
        public static IServiceCollection AddTransientConfiguration(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IAuthRepository, AuthRepository>();
            services.AddTransient<IAuthorRepository, AuthorRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IInventoryRepository, InventoryRepository>();
            services.AddTransient<IBookRepository, BookRepository>();
            services.AddTransient<ISaleRepository, SaleRepository>();
            services.AddTransient<IPasswordServiceHash, PasswordServiceHash>();
            services.AddTransient<IJwtTokenService, JwtTokenService>();
            return services;
        }
    }
}