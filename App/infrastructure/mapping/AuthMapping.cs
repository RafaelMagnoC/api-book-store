using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_bookStore.App.Modules.Auth.Entitiy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api_bookStore.App.infrastructure.mapping
{
    public class AuthMapping : IEntityTypeConfiguration<AuthEntity>
    {
        public void Configure(EntityTypeBuilder<AuthEntity> builder)
        {
            builder
            .ToTable("auth");

            builder
            .HasKey(auth => auth.Id);

            builder
            .Property(auth => auth.Id)
            .HasColumnName("auth_id")
            .HasDefaultValueSql("NEWID()")
            .IsRequired();

            builder
            .Property(auth => auth.UserId)
            .HasColumnName("auth_user_id")
            .HasMaxLength(255)
            .IsRequired();

            builder
            .Property(auth => auth.Email)
            .HasColumnName("auth_user_email")
            .HasMaxLength(100)
            .IsRequired();

            builder
            .Property(auth => auth.Password)
            .HasColumnName("auth_user_Password")
            .HasMaxLength(255)
            .IsRequired();

            builder
            .Property(auth => auth.Token)
            .HasColumnName("auth_user_token")
            .HasMaxLength(255)
            .IsRequired();

            builder
            .Property(auth => auth.CreatedAt)
            .HasColumnName("auth_created_at")
            .HasColumnType("datetime")
            .HasDefaultValue(DateTime.Now.ToString());

            builder
            .Property(auth => auth.UpdatedAt)
            .HasColumnName("auth_updated_at")
            .HasColumnType("datetime");
        }
    }
}