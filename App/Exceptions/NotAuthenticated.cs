using System;

namespace api_BookStore.App.Exceptions
{
    /// <summary>
    /// Exceção personalizada lançada quando um usuário não está autenticado.
    /// Geralmente usada quando o usuário tenta acessar um recurso protegido sem estar autenticado.
    /// </summary>
    public class NotAuthenticated : Exception
    {
        /// <summary>
        /// Código de status HTTP associado a esta exceção. O padrão é 401 (Unauthorized).
        /// </summary>
        public int StatusCode { get; private set; } = 401;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="NotAuthenticated"/> com uma mensagem padrão.
        /// </summary>
        public NotAuthenticated() : base("Usuário não autenticado.") { }

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="NotAuthenticated"/> com uma mensagem personalizada.
        /// </summary>
        /// <param name="message">A mensagem de erro que descreve o motivo da exceção.</param>
        public NotAuthenticated(string message) : base(message) { }

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="NotAuthenticated"/> com uma mensagem personalizada e uma exceção interna.
        /// </summary>
        /// <param name="message">A mensagem de erro que descreve o motivo da exceção.</param>
        /// <param name="innerException">A exceção interna que causou esta exceção.</param>
        public NotAuthenticated(string message, Exception innerException) : base(message, innerException) { }
    }
}
