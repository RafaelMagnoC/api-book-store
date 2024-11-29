using api_BookStore.App.Exceptions;

namespace api_BookStore.App.Middlewares
{
    /// <summary>
    /// Middleware personalizado para interceptar e tratar exceções específicas durante a execução da requisição.
    /// Essa classe captura exceções definidas, registra os erros e retorna uma resposta JSON apropriada com o código de status HTTP correspondente.
    /// </summary>
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionMiddleware> _logger;

        /// <summary>
        /// Inicializa uma nova instância do middleware de exceções personalizadas.
        /// </summary>
        /// <param name="next">O próximo delegado de requisição.</param>
        /// <param name="logger">O logger usado para registrar os erros.</param>
        public CustomExceptionMiddleware(RequestDelegate next, ILogger<CustomExceptionMiddleware> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Intercepta as requisições e captura exceções específicas, retornando uma resposta adequada para cada tipo de erro.
        /// </summary>
        /// <param name="context">O contexto HTTP da requisição.</param>
        /// <returns>Uma tarefa assíncrona que representa o resultado da operação de processamento da requisição.</returns>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Passa o controle para o próximo middleware
                await _next(context);
            }
            catch (NotAuthenticated exception)
            {
                _logger.LogError(exception, "Erro de autenticação.");
                await HandleExceptionAsync(context, exception, StatusCodes.Status401Unauthorized);
            }
            catch (NotAuthorized exception)
            {
                _logger.LogError(exception, "Erro de autorização.");
                await HandleExceptionAsync(context, exception, StatusCodes.Status403Forbidden);
            }
            catch (BadRequest exception)
            {
                _logger.LogError(exception, "Erro na requisição.");
                await HandleExceptionAsync(context, exception, StatusCodes.Status400BadRequest);
            }
            catch (AvailableQuantity exception)
            {
                _logger.LogError(exception, "Produto sem saldo disponível.");
                await HandleExceptionAsync(context, exception, StatusCodes.Status423Locked);
            }
            catch (NotFound exception)
            {
                _logger.LogError(exception, "Nenhum resultado encontrado.");
                await HandleExceptionAsync(context, exception, StatusCodes.Status404NotFound);
            }
            catch (CreateException exception)
            {
                _logger.LogError(exception, "Um erro ocorreu durante a criação.");
                await HandleExceptionAsync(context, exception, StatusCodes.Status422UnprocessableEntity);
            }
            catch (UpdateException exception)
            {
                _logger.LogError(exception, "Um erro ocorreu durante a atualização.");
                await HandleExceptionAsync(context, exception, StatusCodes.Status422UnprocessableEntity);
            }
            catch (RemoveException exception)
            {
                _logger.LogError(exception, "Um erro ocorreu ao remover.");
                await HandleExceptionAsync(context, exception, StatusCodes.Status422UnprocessableEntity);
            }
            catch (UniqueKeyException exception)
            {
                _logger.LogError(exception, "Chave única já existe.");
                await HandleExceptionAsync(context, exception, StatusCodes.Status422UnprocessableEntity);
            }
            catch (InvalidCredential exception)
            {
                _logger.LogError(exception, "Credenciais inválidas.");
                await HandleExceptionAsync(context, exception, StatusCodes.Status422UnprocessableEntity);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Erro interno.");
                await HandleExceptionAsync(context, exception, StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Processa a exceção e retorna uma resposta JSON contendo a mensagem e o rastreamento de pilha do erro.
        /// </summary>
        /// <param name="context">O contexto HTTP da requisição.</param>
        /// <param name="ex">A exceção que ocorreu.</param>
        /// <param name="statusCode">O código de status HTTP a ser retornado na resposta.</param>
        /// <returns>Uma tarefa assíncrona que representa a operação de escrita da resposta.</returns>
        private static Task HandleExceptionAsync(HttpContext context, Exception ex, int statusCode)
        {
            // Define o tipo de conteúdo como JSON
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            // Cria a resposta com a mensagem e detalhes da exceção
            var response = new
            {
                message = ex.Message,
                details = ex.StackTrace
            };

            // Retorna a resposta como JSON
            return context.Response.WriteAsJsonAsync(response);
        }
    }
}
