using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_bookStore.App.Modules.Inventory.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api_bookStore.App.infrastructure.mapping
{
    public class InventoryMapping : IEntityTypeConfiguration<InventoryEntity>
    {
        public void Configure(EntityTypeBuilder<InventoryEntity> builder)
        {
            builder
            .ToTable("inventory");

            builder
            .HasKey(inventory => inventory.Id);

            builder
            .Property(inventory => inventory.Id)
            .HasColumnName("inventory_id")
            .ValueGeneratedOnAdd()
            .IsRequired();

            builder
            .Property(inventory => inventory.Value)
            .HasColumnName("inventory_value")
            .HasPrecision(10, 2);

            builder
            .Property(inventory => inventory.Quantity)
            .HasColumnName("inventory_quantity")
            .IsRequired();

            builder
            .Property(inventory => inventory.BookId)
            .HasColumnName("inventory_book_id");

            builder
            .HasOne(inventory => inventory.Book)
            .WithOne(book => book.Inventory)
            .HasForeignKey<InventoryEntity>(inventory => inventory.BookId)
            .OnDelete(DeleteBehavior.Cascade);

            builder
            .Property(inventory => inventory.CreatedAt)
            .HasColumnName("inventory_created_at")
            .HasColumnType("datetime")
            .HasDefaultValue(DateTime.Now);

            builder
            .Property(inventory => inventory.UpdatedAt)
            .HasColumnName("inventory_updated_at")
            .HasColumnType("datetime");

        }
    }
}