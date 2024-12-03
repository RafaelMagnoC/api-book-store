using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using api_bookStore.App.Services.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace api_bookStore.App.Config
{
    /// <summary>
    /// Configuração do JWT (JSON Web Token) para a aplicação.
    /// Este serviço configura a autenticação baseada em JWT, definindo a chave secreta e os parâmetros de validação do token.
    /// </summary>
    public static class JwtConfig
    {
        /// <summary>
        /// Adiciona a configuração do JWT à coleção de serviços.
        /// Configura a autenticação baseada em JWT, com parâmetros de validação e chave secreta obtidos das configurações da aplicação.
        /// </summary>
        /// <param name="services">A coleção de serviços onde a configuração do JWT será registrada.</param>
        /// <param name="configuration">A configuração da aplicação, usada para obter a chave secreta do token.</param>
        /// <returns>A coleção de serviços com a configuração de autenticação JWT registrada.</returns>
        /// <remarks>
        /// Este método configura o esquema de autenticação JWT, incluindo:
        /// - Definição da chave secreta do JWT a partir da variável de configuração <c>Jwt:Key</c>.
        /// - Definição de parâmetros de validação do token, como a chave de assinatura e a desativação de validações de emissor e público.
        /// - Configuração de que o token não precisa de HTTPS para ser validado.
        /// </remarks>
        /// <exception cref="Exception">Lançada se a chave secreta do JWT não for encontrada nas configurações.</exception>
        public static IServiceCollection AddJwtConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            string secretKey = configuration["Jwt:Key"] ?? throw new Exception("A chave secreta do token não foi encontrada. Verificar variável de ambiente");

            byte[] key = Encoding.ASCII.GetBytes(secretKey);

            services.AddAuthentication(jwt =>
            {
                jwt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                jwt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(define =>
            {
                define.RequireHttpsMetadata = false;
                define.SaveToken = true;
                define.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            return services;
        }
    }
}