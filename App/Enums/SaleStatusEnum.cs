namespace api_bookStore.App.Enums
{
    /// <summary>
    /// Enumeração que representa os diferentes status de uma venda.
    /// Cada valor corresponde ao status atual de uma venda no sistema.
    /// </summary>
    public enum SaleStatusEnum
    {
        /// <summary>
        /// Status indicando que a venda está aberta e em andamento.
        /// </summary>
        open = 0,
        /// <summary>
        /// Status indicando que a venda está pendente, aguardando uma ação ou conclusão.
        /// </summary>
        pending = 1,
        /// <summary>
        /// Status indicando que a venda foi cancelada.
        /// </summary>
        canceled = 2,
        /// <summary>
        /// Status indicando que a venda falhou devido a algum erro.
        /// </summary>
        failed = 3,
        /// <summary>
        /// Status indicando que a venda foi concluída e fechada.
        /// </summary>
        closed = 4
    }
}