using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using api_bookStore.App.Enums;
using api_bookStore.App.Modules.User.ViewModel;

namespace api_bookStore.App.Modules.User.Entity
{
    [Table("user")]
    public class UserEntity
    {
        [Key]
        [Column("user_id")]
        public Guid Id { get; private set; }

        [Column("user_name", TypeName = "varchar(60)")]
        [Required]
        public string? Name { get; set; }
        [Column("user_email", TypeName = "varchar(60)")]
        [Required]
        public string? Email { get; set; }
        [Column("user_password", TypeName = "varchar(255)")]
        [Required]
        public string? Password { get; set; }

        [Column("user_role")]
        [Required]
        public RolesEnum Role { get; set; }
        [Column("user_created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [Column("user_updated_at")]
        public DateTime UpdatedAt { get; set; }
        public UserEntity(UserViewModelCreate userViewModelCreate)
        {
            Name = userViewModelCreate.Name;
            Email = userViewModelCreate.Email;
            Password = userViewModelCreate.Password;
            Role = userViewModelCreate.Role;
        }

        public UserEntity() { }

    }
}