using System;

namespace api_BookStore.App.Exceptions
{
    /// <summary>
    /// Exceção personalizada lançada quando um usuário não tem permissão para acessar um recurso.
    /// Geralmente usada quando o usuário está autenticado, mas não possui os privilégios necessários.
    /// </summary>
    public class NotAuthorized : Exception
    {
        /// <summary>
        /// Código de status HTTP associado a esta exceção. O padrão é 403 (Forbidden).
        /// </summary>
        public int StatusCode { get; private set; } = 403;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="NotAuthorized"/> com uma mensagem padrão.
        /// </summary>
        public NotAuthorized() : base("Usuário não autorizado.") { }

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="NotAuthorized"/> com uma mensagem personalizada.
        /// </summary>
        /// <param name="message">A mensagem de erro que descreve o motivo da exceção.</param>
        public NotAuthorized(string message) : base(message) { }

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="NotAuthorized"/> com uma mensagem personalizada e uma exceção interna.
        /// </summary>
        /// <param name="message">A mensagem de erro que descreve o motivo da exceção.</param>
        /// <param name="innerException">A exceção interna que causou esta exceção.</param>
        public NotAuthorized(string message, Exception innerException) : base(message, innerException) { }
    }
}
