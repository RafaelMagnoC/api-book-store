using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_bookStore.App.Modules.Author.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api_bookStore.App.infrastructure.mapping
{
    public class AuthorMapping : IEntityTypeConfiguration<AuthorEntity>
    {
        public void Configure(EntityTypeBuilder<AuthorEntity> builder)
        {
            builder
            .ToTable("author");

            builder
            .HasKey(author => author.Id);

            builder
            .Property(author => author.Id)
            .HasColumnName("author_id")
            .ValueGeneratedOnAdd()
            .IsRequired();

            builder
            .HasIndex(author => author.Name)
            .HasDatabaseName("IX_AUTHOR_NAME_UNIQUE")
            .IsUnique();

            builder
            .Property(author => author.Name)
            .HasColumnName("author_name")
            .IsRequired();

            builder
            .Property(author => author.BirthDay)
            .HasColumnName("author_birthday")
            .HasColumnType("DATE")
            .IsRequired();

            builder
            .Property(author => author.Country)
            .HasColumnName("author_country")
            .HasColumnType("INT")
            .IsRequired();

            builder
            .Property(author => author.CreatedAt)
            .HasColumnName("author_created_at")
            .HasColumnType("datetime")
            .HasDefaultValue(DateTime.Now.ToString());

            builder
            .Property(author => author.UpdatedAt)
            .HasColumnName("author_updated_at")
            .HasColumnType("datetime");
        }
    }
}