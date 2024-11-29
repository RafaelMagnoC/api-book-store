using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using api_bookStore.App.Enums;
using api_bookStore.App.Services.Swagger;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

namespace api_bookStore.App.Config
{
    /// <summary>
    /// Configuração do Swagger para a aplicação.
    /// Este serviço configura o Swagger, incluindo a documentação de API, filtros de operações, segurança, e enumerações.
    /// </summary>
    public static class SwaggerConfig
    {
        /// <summary>
        /// Adiciona a configuração do Swagger à coleção de serviços.
        /// Configura o Swagger para gerar a documentação da API, incluindo filtros de operações, segurança com JWT,
        /// e mapeamento de enums para a documentação.
        /// </summary>
        /// <param name="services">A coleção de serviços onde o Swagger será configurado.</param>
        /// <returns>A coleção de serviços com a configuração do Swagger registrada.</returns>
        /// <remarks>
        /// Este método:
        /// - Adiciona um filtro de operação para personalizar a documentação de operações via o <see cref="SwaggerService"/>.
        /// - Configura o Swagger para suportar segurança com tokens Bearer (JWT).
        /// - Mapeia enums como <see cref="RolesEnum"/>, <see cref="CountriesEnum"/>, e <see cref="SaleStatusEnum"/> para documentação.
        /// - Inclui comentários XML gerados para as operações da API, utilizando um arquivo XML de documentação.
        /// </remarks>
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(swagger =>
            {
                swagger.OperationFilter<SwaggerService>();
                swagger.UseAllOfToExtendReferenceSchemas();
                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                    },
                    new List<string>()
                }
                });

                swagger.MapType<RolesEnum>(() => new OpenApiSchema
                {
                    Type = "string",
                    Enum = Enum.GetNames(typeof(RolesEnum))
                    .Select(name => new OpenApiString(name))
                    .ToArray()
                });

                swagger.MapType<CountriesEnum>(() => new OpenApiSchema
                {
                    Type = "string",
                    Enum = Enum.GetNames(typeof(CountriesEnum))
                    .Select(name => new OpenApiString(name))
                    .ToArray()
                });

                swagger.MapType<SaleStatusEnum>(() => new OpenApiSchema
                {
                    Type = "string",
                    Enum = Enum.GetNames(typeof(SaleStatusEnum))
                    .Select(name => new OpenApiString(name))
                    .ToArray()
                });

                string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                swagger.IncludeXmlComments(xmlPath);

            });

            return services;
        }
    }

}