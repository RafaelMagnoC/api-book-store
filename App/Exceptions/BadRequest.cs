
namespace api_BookStore.App.Exceptions
{
    /// <summary>
    /// Exceção personalizada lançada quando uma requisição é considerada inválida (erro 400 - Bad Request).
    /// </summary>
    public class BadRequest : Exception
    {
        /// <summary>
        /// Código de status HTTP associado a esta exceção. O padrão é 400 (Bad Request).
        /// </summary>
        public int StatusCode { get; private set; } = 400;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="BadRequest"/> com uma mensagem padrão.
        /// </summary>
        public BadRequest() : base("A requisição contém erros.") { }

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="BadRequest"/> com uma mensagem personalizada.
        /// </summary>
        /// <param name="message">A mensagem de erro que descreve o motivo da exceção.</param>
        public BadRequest(string message) : base(message) { }

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="BadRequest"/> com uma mensagem personalizada e uma exceção interna.
        /// </summary>
        /// <param name="message">A mensagem de erro que descreve o motivo da exceção.</param>
        /// <param name="innerException">A exceção interna que causou esta exceção.</param>
        public BadRequest(string message, Exception innerException) : base(message, innerException) { }
    }
}
