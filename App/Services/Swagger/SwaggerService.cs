using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace api_bookStore.App.Services.Swagger
{
    /// <summary>
    /// Serviço que aplica as descrições e valores padrão aos parâmetros de operação no Swagger.
    /// </summary>
    public class SwaggerService : IOperationFilter
    {
        /// <summary>
        /// Aplica descrições e valores padrão para os parâmetros da operação no Swagger.
        /// </summary>
        /// <param name="operation">A operação da API a ser modificada.</param>
        /// <param name="context">O contexto que fornece informações sobre a operação e parâmetros.</param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var apiDescription = context.ApiDescription;

            if (operation.Parameters == null)
            {
                return;
            }

            foreach (var parameter in operation.Parameters)
            {
                var description = apiDescription.ParameterDescriptions.First(p => p.Name == parameter.Name);

                if (parameter.Description is null)
                {
                    parameter.Description = description.ModelMetadata?.Description;
                }

                if (parameter.Schema.Default is null && description.DefaultValue is not null)
                {
                    parameter.Schema.Default = new OpenApiString(description.DefaultValue.ToString());
                }

                parameter.Required |= description.IsRequired;
            }

        }
    }
}