namespace api_BookStore.App.Exceptions
{
    /// <summary>
    /// Exceção personalizada lançada quando a quantidade disponível de um produto é insuficiente.
    /// </summary>
    public class AvailableQuantity : Exception
    {
        /// <summary>
        /// Código de status HTTP associado a esta exceção. O padrão é 423 (Locked).
        /// </summary>
        public int StatusCode { get; private set; } = 423;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="AvailableQuantity"/> com uma mensagem padrão.
        /// </summary>
        public AvailableQuantity() : base("Produto sem saldo disponível.") { }

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="AvailableQuantity"/> com uma mensagem personalizada.
        /// </summary>
        /// <param name="message">A mensagem de erro que descreve o motivo da exceção.</param>
        public AvailableQuantity(string message) : base(message) { }

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="AvailableQuantity"/> com uma mensagem personalizada e uma exceção interna.
        /// </summary>
        /// <param name="message">A mensagem de erro que descreve o motivo da exceção.</param>
        /// <param name="innerException">A exceção interna que causou esta exceção.</param>
        public AvailableQuantity(string message, Exception innerException) : base(message, innerException) { }
    }
}
