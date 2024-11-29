using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_bookStore.App.Config
{
    /// <summary>
    /// Configuração do CORS (Cross-Origin Resource Sharing) para a aplicação.
    /// Este serviço permite configurar as permissões de acesso entre diferentes origens.
    /// </summary>
    public static class CorsConfig
    {
        /// <summary>
        /// Adiciona a configuração de CORS à coleção de serviços.
        /// </summary>
        /// <param name="services">A coleção de serviços onde a configuração de CORS será registrada.</param>
        /// <returns>A coleção de serviços com a configuração de CORS registrada.</returns>
        /// <remarks>
        /// Este método configura a política de CORS para permitir requisições da origem <c>http://localhost:8080</c>
        /// e permite qualquer cabeçalho e método HTTP. 
        /// O nome da política de CORS é "MyPolicy".
        /// </remarks>
        public static IServiceCollection AddCorsConfiguration(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: "MyPolicy", policy =>
                {
                    policy.WithOrigins("http://localhost:8080")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            return services;
        }

    }
}