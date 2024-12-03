using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using api_bookStore.App.Modules.Auth.ViewModel;

namespace api_bookStore.App.Modules.Auth.Entitiy
{
    [Table("auth")]
    public class AuthEntity
    {
        [Key]
        [Column("auth_id")]
        public Guid Id { get; set; }

        [Required]
        [Column("auth_user_id", TypeName = "varchar(60)")]
        public string UserId { get; set; } = null!;

        [Required]
        [Column("auth_user_email", TypeName = "varchar(60)")]
        public string Email { get; set; } = null!;

        [Required]
        [Column("auth_user_password", TypeName = "varchar(255)")]
        public string Password { get; set; } = null!;

        [Required]
        [Column("auth_user_token", TypeName = "varchar(60)")]
        public string Token { get; set; } = null!;

        [Column("auth_created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column("auth_updated_at")]
        public DateTime UpdatedAt { get; set; }

        public AuthEntity() { }

        public AuthEntity(AuthViewModel authViewModel)
        {
            Email = authViewModel.Email;
            Password = authViewModel.Password;
        }
    }
}