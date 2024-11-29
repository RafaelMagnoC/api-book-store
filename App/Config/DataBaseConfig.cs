using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_bookStore.App.DataBase;
using Microsoft.EntityFrameworkCore;

namespace api_bookStore.App.Config
{
    /// <summary>
    /// Configuração do banco de dados para a aplicação.
    /// Este serviço configura o contexto do Entity Framework para se conectar ao banco de dados.
    /// </summary>
    public static class DataBaseConfig
    {
        /// <summary>
        /// Adiciona a configuração do banco de dados à coleção de serviços.
        /// Configura o contexto do Entity Framework para utilizar o SQL Server com a string de conexão definida no arquivo de configuração.
        /// </summary>
        /// <param name="services">A coleção de serviços onde o contexto de banco de dados será registrado.</param>
        /// <param name="configuration">A configuração da aplicação, usada para obter a string de conexão.</param>
        /// <returns>A coleção de serviços com a configuração do banco de dados registrada.</returns>
        /// <remarks>
        /// Este método registra o contexto <see cref="BookStoreContext"/> e configura a conexão com o banco de dados 
        /// utilizando o SQL Server, utilizando a string de conexão chamada "DefaultConnection" presente nas configurações 
        /// da aplicação (por exemplo, no arquivo appsettings.json).
        /// </remarks>
        public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BookStoreContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            return services;
        }

    }
}