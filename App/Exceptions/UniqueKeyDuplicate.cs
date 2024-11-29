using System;

namespace api_BookStore.App.Exceptions
{
    /// <summary>
    /// Exceção personalizada lançada quando ocorre um erro devido à violação de uma chave única.
    /// Geralmente usada quando uma operação de inserção ou atualização tenta inserir um valor duplicado
    /// em uma coluna que exige unicidade no banco de dados.
    /// </summary>
    public class UniqueKeyException : Exception
    {
        /// <summary>
        /// Código de status HTTP associado a esta exceção. O padrão é 423 (Locked).
        /// </summary>
        public int StatusCode { get; private set; } = 423;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="UniqueKeyException"/> com uma mensagem padrão.
        /// </summary>
        public UniqueKeyException() : base("Chave única já existe.") { }

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="UniqueKeyException"/> com uma mensagem personalizada.
        /// </summary>
        /// <param name="message">A mensagem de erro que descreve o motivo da exceção.</param>
        public UniqueKeyException(string message) : base(message) { }

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="UniqueKeyException"/> com uma mensagem personalizada e uma exceção interna.
        /// </summary>
        /// <param name="message">A mensagem de erro que descreve o motivo da exceção.</param>
        /// <param name="innerException">A exceção interna que causou esta exceção.</param>
        public UniqueKeyException(string message, Exception innerException) : base(message, innerException) { }
    }
}
