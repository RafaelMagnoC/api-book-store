using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_bookStore.App.Modules.Sale.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api_bookStore.App.infrastructure.mapping
{
    public class SaleBookMapping : IEntityTypeConfiguration<SaleBookEntity>
    {
        public void Configure(EntityTypeBuilder<SaleBookEntity> builder)
        {
            builder
            .ToTable("sb_sale_x_book");

            builder
            .HasKey(saleBook => new { saleBook.SaleId, saleBook.BookId });

            builder
            .Property(saleBook => saleBook.Id)
            .HasColumnName("sb_id");

            builder
            .Property(salebook => salebook.Price)
            .HasColumnName("sb_price");

            builder
            .Property(saleBook => saleBook.Quantity)
            .HasColumnName("sb_quantity");

            builder
            .Property(saleBook => saleBook.SubTotal)
            .HasColumnName("sb_subtotal");

            builder
            .Property(saleBook => saleBook.BookId)
            .HasColumnName("sb_book_id")
            .IsRequired();

            builder
            .Property(saleBook => saleBook.SaleId)
            .HasColumnName("sb_sale_id")
            .IsRequired();

            builder
            .HasOne(salebook => salebook.Book)
            .WithMany(book => book.SaleBook)
            .HasForeignKey(salebook => salebook.BookId);

            builder
            .HasOne(salebook => salebook.Sale)
            .WithMany(sale => sale.SaleBook)
            .HasForeignKey(salebook => salebook.SaleId);

        }
    }
}