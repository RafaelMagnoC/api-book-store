using System;

namespace api_BookStore.App.Exceptions
{
    /// <summary>
    /// Exceção personalizada lançada quando um recurso não é encontrado.
    /// Geralmente usada quando uma consulta ou operação retorna nenhum resultado.
    /// </summary>
    public class NotFound : Exception
    {
        /// <summary>
        /// Código de status HTTP associado a esta exceção. O padrão é 404 (Not Found).
        /// </summary>
        public int StatusCode { get; private set; } = 404;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="NotFound"/> com uma mensagem padrão.
        /// </summary>
        public NotFound() : base("Nenhum resultado encontrado.") { }

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="NotFound"/> com uma mensagem personalizada.
        /// </summary>
        /// <param name="message">A mensagem de erro que descreve o motivo da exceção.</param>
        public NotFound(string message) : base(message) { }

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="NotFound"/> com uma mensagem personalizada e uma exceção interna.
        /// </summary>
        /// <param name="message">A mensagem de erro que descreve o motivo da exceção.</param>
        /// <param name="innerException">A exceção interna que causou esta exceção.</param>
        public NotFound(string message, Exception innerException) : base(message, innerException) { }
    }
}
