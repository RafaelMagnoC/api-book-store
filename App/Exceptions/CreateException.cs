namespace api_BookStore.App.Exceptions
{
    /// <summary>
    /// Exceção personalizada lançada quando ocorre um erro durante o processo de criação de um recurso.
    /// </summary>
    public class CreateException : Exception
    {
        /// <summary>
        /// Código de status HTTP associado a esta exceção. O padrão é 422 (Unprocessable Entity).
        /// </summary>
        public int StatusCode { get; private set; } = 422;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="CreateException"/> com uma mensagem padrão.
        /// </summary>
        public CreateException() : base("Um erro ocorreu durante a criação.") { }

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="CreateException"/> com uma mensagem personalizada.
        /// </summary>
        /// <param name="message">A mensagem de erro que descreve o motivo da exceção.</param>
        public CreateException(string message) : base(message) { }

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="CreateException"/> com uma mensagem personalizada e uma exceção interna.
        /// </summary>
        /// <param name="message">A mensagem de erro que descreve o motivo da exceção.</param>
        /// <param name="innerException">A exceção interna que causou esta exceção.</param>
        public CreateException(string message, Exception innerException) : base(message, innerException) { }
    }
}
