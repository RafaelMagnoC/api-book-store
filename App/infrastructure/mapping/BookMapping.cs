using api_bookStore.App.Modules.Book.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api_bookStore.App.infrastructure.mapping
{
    public class BookMapping : IEntityTypeConfiguration<BookEntity>
    {
        public void Configure(EntityTypeBuilder<BookEntity> builder)
        {
            builder
            .ToTable("book");

            builder
            .HasKey(book => book.Id);

            builder
            .Property(book => book.Id)
            .HasColumnName("book_id")
            .ValueGeneratedOnAdd()
            .IsRequired();

            builder
            .Property(book => book.Title)
            .HasColumnName("book_title")
            .HasMaxLength(100)
            .IsRequired();

            builder
            .Property(book => book.PublicationDate)
            .HasColumnName("book_publication_date")
            .HasColumnType("DATE")
            .IsRequired();

            builder
            .Property(book => book.Price)
            .HasColumnName("book_price")
            .HasColumnType("DECIMAL")
            .HasPrecision(10, 2)
            .IsRequired();

            builder
            .Property(book => book.CreatedAt)
            .HasColumnName("book_created_at")
            .HasColumnType("datetime")
            .HasDefaultValue(DateTime.Now);

            builder
            .Property(book => book.UpdatedAt)
            .HasColumnName("book_updated_at")
            .HasColumnType("datetime");

            builder
            .Property(book => book.AuthorId)
            .HasColumnName("book_author_id")
            .IsRequired();

            builder
            .HasOne(book => book.Author)
            .WithMany(author => author.Books)
            .HasForeignKey(book => book.AuthorId)
            .OnDelete(DeleteBehavior.Cascade);

            builder
            .Property(book => book.CategoryId)
            .HasColumnName("book_category_id")
            .IsRequired();

            builder
            .HasOne(book => book.Category)
            .WithMany(category => category.Books)
            .HasForeignKey(book => book.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}