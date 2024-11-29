namespace api_bookStore.App.Enums
{
    /// <summary>
    /// Enumeração que representa os papéis de usuário no sistema.
    /// Cada valor corresponde a um papel específico, que define as permissões de acesso no sistema.
    /// </summary>
    public enum RolesEnum
    {
        /// <summary>
        /// Papel de administrador, com permissões completas.
        /// </summary>
        Admin = 1,

        /// <summary>
        /// Papel padrão, com permissões básicas.
        /// </summary>
        Default = 2
    }
}