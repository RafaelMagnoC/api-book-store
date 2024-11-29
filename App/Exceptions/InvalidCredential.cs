namespace api_BookStore.App.Exceptions
{
    /// <summary>
    /// Exceção personalizada lançada quando as credenciais fornecidas são inválidas.
    /// Geralmente usada em casos de falha de autenticação.
    /// </summary>
    public class InvalidCredential : Exception
    {
        /// <summary>
        /// Código de status HTTP associado a esta exceção. O padrão é 401 (Unauthorized).
        /// </summary>
        public int StatusCode { get; private set; } = 401;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="InvalidCredential"/> com uma mensagem padrão.
        /// </summary>
        public InvalidCredential() : base("Credenciais inválidas.") { }

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="InvalidCredential"/> com uma mensagem personalizada.
        /// </summary>
        /// <param name="message">A mensagem de erro que descreve o motivo da exceção.</param>
        public InvalidCredential(string message) : base(message) { }

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="InvalidCredential"/> com uma mensagem personalizada e uma exceção interna.
        /// </summary>
        /// <param name="message">A mensagem de erro que descreve o motivo da exceção.</param>
        /// <param name="innerException">A exceção interna que causou esta exceção.</param>
        public InvalidCredential(string message, Exception innerException) : base(message, innerException) { }
    }
}
