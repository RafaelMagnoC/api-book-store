using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public required DbSet<UserEntity> User { get; set; }
        /// <summary>
        /// Conjunto de entidades de autenticação.
        /// </summary>
        public required DbSet<AuthEntity> Auth { get; set; }
        /// <summary>
        /// Conjunto de entidades de autores.
        /// </summary>
        public required DbSet<AuthorEntity> Author { get; set; }
        /// <summary>
        /// Conjunto de entidades de categorias de livros.
        /// </summary>
        public required DbSet<CategoryEntity> Category { get; set; }
        /// <summary>
        /// Conjunto de entidades de inventário.
        /// </summary>
        public required DbSet<InventoryEntity> Inventory { get; set; }
        /// <summary>
        /// Conjunto de entidades de livros.
        /// </summary>
        public required DbSet<BookEntity> Book { get; set; }
        /// <summary>
        /// Conjunto de entidades de vendas.
        /// </summary>
        public required DbSet<SaleEntity> Sale { get; set; }
        /// <summary>
        /// Conjunto de entidades que representam a relação entre vendas e livros.
        /// </summary>
        public required DbSet<SaleXBookEntity> SaleXBook { get; set; }
        /// <summary>
        /// Configurações adicionais para o modelo de dados, incluindo a criação de índices e relacionamentos.
        /// </summary>
        /// <param name="modelBuilder">O construtor de modelos usado para configurar o esquema do banco de dados.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserEntity>()
            .HasIndex(u => u.Email)
            .IsUnique();

            modelBuilder.Entity<BookEntity>()
                .HasOne(book => book.Author)
                .WithMany(author => author.Books)
                .HasForeignKey(book => book.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BookEntity>()
                .HasOne(book => book.Category)
                .WithMany(category => category.Books)
                .HasForeignKey(book => book.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BookEntity>()
                .HasOne(book => book.Quantity)
                .WithOne(inventory => inventory.Book)
                .HasForeignKey<BookEntity>(book => book.Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SaleXBookEntity>()
            .HasKey(sb => sb.Id);

            modelBuilder.Entity<SaleXBookEntity>()
                .HasOne(sb => sb.Sale)
                .WithMany(s => s.SaleXBooks)
                .HasForeignKey(sb => sb.SaleId);

            modelBuilder.Entity<SaleXBookEntity>()
                .HasOne(sb => sb.Book)
                .WithMany(b => b.SaleXBooks)
                .HasForeignKey(sb => sb.BookId);
        }
    }
}