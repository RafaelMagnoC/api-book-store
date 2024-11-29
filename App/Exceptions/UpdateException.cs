using System;

namespace api_BookStore.App.Exceptions
{
    /// <summary>
    /// Exceção personalizada lançada quando ocorre um erro durante o processo de atualização de um recurso.
    /// Geralmente usada quando uma operação de atualização falha, seja por falhas de validação,
    /// inconsistências de dados ou outras condições inesperadas.
    /// </summary>
    public class UpdateException : Exception
    {
        /// <summary>
        /// Código de status HTTP associado a esta exceção. O padrão é 422 (Unprocessable Entity).
        /// </summary>
        public int StatusCode { get; private set; } = 422;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="UpdateException"/> com uma mensagem padrão.
        /// </summary>
        public UpdateException() : base("Um erro ocorreu durante a atualização.") { }

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="UpdateException"/> com uma mensagem personalizada.
        /// </summary>
        /// <param name="message">A mensagem de erro que descreve o motivo da exceção.</param>
        public UpdateException(string message) : base(message) { }

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="UpdateException"/> com uma mensagem personalizada e uma exceção interna.
        /// </summary>
        /// <param name="message">A mensagem de erro que descreve o motivo da exceção.</param>
        /// <param name="innerException">A exceção interna que causou esta exceção.</param>
        public UpdateException(string message, Exception innerException) : base(message, innerException) { }
    }
}
