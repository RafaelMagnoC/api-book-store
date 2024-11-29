using System;

namespace api_BookStore.App.Exceptions
{
    /// <summary>
    /// Exceção personalizada lançada quando ocorre um erro ao tentar remover um recurso.
    /// Geralmente usada quando uma operação de remoção falha devido a alguma condição inesperada.
    /// </summary>
    public class RemoveException : Exception
    {
        /// <summary>
        /// Código de status HTTP associado a esta exceção. O padrão é 422 (Unprocessable Entity).
        /// </summary>
        public int StatusCode { get; private set; } = 422;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="RemoveException"/> com uma mensagem padrão.
        /// </summary>
        public RemoveException() : base("Um erro ocorreu ao remover.") { }

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="RemoveException"/> com uma mensagem personalizada.
        /// </summary>
        /// <param name="message">A mensagem de erro que descreve o motivo da exceção.</param>
        public RemoveException(string message) : base(message) { }

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="RemoveException"/> com uma mensagem personalizada e uma exceção interna.
        /// </summary>
        /// <param name="message">A mensagem de erro que descreve o motivo da exceção.</param>
        /// <param name="innerException">A exceção interna que causou esta exceção.</param>
        public RemoveException(string message, Exception innerException) : base(message, innerException) { }
    }
}
