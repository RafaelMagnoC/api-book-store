using api_bookStore.App.Services.AutoMapper;
namespace api_bookStore.App.Config
{
    /// <summary>
    /// Configuração para o AutoMapper na aplicação.
    /// Este serviço configura o AutoMapper para ser utilizado em toda a aplicação.
    /// </summary>
    public static class AutoMapperConfig
    {
        /// <summary>
        /// Adiciona a configuração do AutoMapper à coleção de serviços.
        /// </summary>
        /// <param name="services">A coleção de serviços onde o AutoMapper será registrado.</param>
        /// <returns>A coleção de serviços com a configuração do AutoMapper registrada.</returns>
        /// <remarks>
        /// Este método registra a configuração do AutoMapper com base na classe <see cref="AutoMapperService"/>.
        /// </remarks>
        public static IServiceCollection AddAutoMapperConfiguration(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperService));

            return services;
        }

    }
}