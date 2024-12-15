using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_bookStore.App.Modules.Sale.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api_bookStore.App.infrastructure.mapping
{
    public class SaleMapping : IEntityTypeConfiguration<SaleEntity>
    {
        public void Configure(EntityTypeBuilder<SaleEntity> builder)
        {
            builder
            .ToTable("sale");

            builder
            .HasKey(sale => sale.Id);

            builder
            .Property(sale => sale.Id)
            .HasColumnName("sale_id")
            .ValueGeneratedOnAdd()
            .IsRequired();

            builder
            .Property(sale => sale.TotalValue)
            .HasColumnName("sale_total_value")
            .HasPrecision(10, 2)
            .IsRequired();

            builder
            .Property(sale => sale.TotalQuantity)
            .HasColumnName("sale_total_quantity")
            .IsRequired();

            builder
            .Property(sale => sale.Status)
            .HasColumnName("sale_status")
            .HasColumnType("INT")
            .IsRequired();

            builder
            .Property(sale => sale.CreatedAt)
            .HasColumnName("sale_created_at")
            .HasColumnType("datetime")
            .HasDefaultValue(DateTime.Now);

            builder
            .Property(sale => sale.UpdatedAt)
            .HasColumnName("sale_updated_at")
            .HasColumnType("datetime");
        }
    }
}