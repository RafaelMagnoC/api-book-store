using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_bookStore.App.infrastructure.mapping;
using api_bookStore.App.Modules.Auth.Entitiy;
using api_bookStore.App.Modules.Author.Entity;
using api_bookStore.App.Modules.Book.Entity;
using api_bookStore.App.Modules.Category.Entity;
using api_bookStore.App.Modules.Inventory.Entity;
using api_bookStore.App.Modules.Sale.Entity;
using api_bookStore.App.Modules.User.Entity;
using Microsoft.EntityFrameworkCore;

namespace api_bookStore.App.DataBase
{
    /// <summary>
    /// Contexto de banco de dados da aplicação BookStore.
    /// Herda de <see cref="DbContext"/> e representa a unidade de trabalho para a interação com o banco de dados.
    /// Construtor do contexto que recebe as opções de configuração do banco de dados.
    /// </summary>
    /// <param name="options">Opções de configuração para o DbContext.</param>
    public class BookStoreContext(DbContextOptions options) : DbContext(options)
    {
        /// <summary>
        /// Conjunto de entidades de usuários.
        /// </summary>
        public DbSet<UserEntity> User { get; set; } = null!;
        /// <summary>
        /// Conjunto de entidades de autenticação.
        /// </summary>
        public DbSet<AuthEntity> Auth { get; set; } = null!;
        /// <summary>
        /// Conjunto de entidades de autores.
        /// </summary>
        public DbSet<AuthorEntity> Author { get; set; } = null!;
        /// <summary>
        /// Conjunto de entidades de categorias de livros.
        /// </summary>
        public DbSet<CategoryEntity> Category { get; set; } = null!;
        /// <summary>
        /// Conjunto de entidades de inventário.
        /// </summary>
        public DbSet<InventoryEntity> Inventory { get; set; } = null!;
        /// <summary>
        /// Conjunto de entidades de livros.
        /// </summary>
        public DbSet<BookEntity> Book { get; set; } = null!;
        /// <summary>
        /// Conjunto de entidades de vendas.
        /// </summary>
        public DbSet<SaleEntity> Sale { get; set; } = null!;
        /// <summary>
        /// Conjunto de entidades que representam a relação entre vendas e livros.
        /// </summary>
        public DbSet<SaleBookEntity> SaleBook { get; set; } = null!;
        /// <summary>
        /// Configurações adicionais para o modelo de dados, incluindo a criação de índices e relacionamentos.
        /// </summary>
        /// <param name="modelBuilder">O construtor de modelos usado para configurar o esquema do banco de dados.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new AuthMapping());
            modelBuilder.ApplyConfiguration(new AuthorMapping());
            modelBuilder.ApplyConfiguration(new BookMapping());
            modelBuilder.ApplyConfiguration(new CategoryMapping());
            modelBuilder.ApplyConfiguration(new InventoryMapping());
            modelBuilder.ApplyConfiguration(new SaleBookMapping());
            modelBuilder.ApplyConfiguration(new SaleMapping());
            modelBuilder.ApplyConfiguration(new UserMapping());
        }
    }
}