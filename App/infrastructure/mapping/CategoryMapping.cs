using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_bookStore.App.Modules.Category.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api_bookStore.App.infrastructure.mapping
{
    public class CategoryMapping : IEntityTypeConfiguration<CategoryEntity>
    {
        public void Configure(EntityTypeBuilder<CategoryEntity> builder)
        {
            builder
            .ToTable("category");

            builder
            .HasKey(category => category.Id);

            builder
            .HasIndex(category => category.Name)
            .HasDatabaseName("IX_CATEGORY_NAME_UNIQUE")
            .IsUnique();

            builder
            .Property(category => category.Id)
            .HasColumnName("category_id")
            .ValueGeneratedOnAdd()
            .IsRequired();

            builder
            .Property(category => category.Name)
            .HasColumnName("category_name")
            .HasMaxLength(50)
            .IsRequired();

            builder
            .Property(category => category.Description)
            .HasColumnName("category_description")
            .HasMaxLength(255);

            builder
            .Property(category => category.CreatedAt)
            .HasColumnName("category_created_at")
            .HasColumnType("datetime")
            .HasDefaultValue(DateTime.Now.ToString());

            builder
            .Property(category => category.UpdatedAt)
            .HasColumnName("category_updated_at")
            .HasColumnType("datetime");

        }
    }
}