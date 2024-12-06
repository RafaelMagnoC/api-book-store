using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_bookStore.App.Modules.User.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api_bookStore.App.infrastructure.mapping
{
    public class UserMapping : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder
            .ToTable("user");

            builder
            .HasKey(user => user.Id);

            builder
            .HasIndex(user => user.Name)
            .HasDatabaseName("IX_USER_NAME_UNIQUE")
            .IsUnique();

            builder
            .HasIndex(user => user.Email)
            .HasDatabaseName("IX_USER_EMAIL_UNIQUE")
            .IsUnique();

            builder
            .Property(user => user.Id)
            .HasColumnName("user_id")
            .ValueGeneratedOnAdd()
            .IsRequired();

            builder
            .Property(user => user.Name)
            .HasColumnName("user_name")
            .HasMaxLength(100)
            .IsRequired();

            builder
            .Property(user => user.Email)
            .HasColumnName("user_email")
            .HasMaxLength(150)
            .IsRequired();

            builder
            .Property(user => user.Password)
            .HasColumnName("user_password")
            .IsRequired();

            builder
            .Property(user => user.Role)
            .HasColumnName("user_role")
            .HasColumnType("INT")
            .IsRequired();

            builder
            .Property(user => user.CreatedAt)
            .HasColumnName("user_created_at")
            .HasColumnType("datetime")
            .HasDefaultValue(DateTime.Now.ToString());

            builder
            .Property(user => user.UpdatedAt)
            .HasColumnName("user_updated_at")
            .HasColumnType("datetime");
        }
    }
}