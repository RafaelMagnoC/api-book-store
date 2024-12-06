using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using api_bookStore.App.Modules.Auth.ViewModel;

namespace api_bookStore.App.Modules.Auth.Entitiy
{
    public class AuthEntity
    {
        public Guid Id { get; set; }
        public string UserId { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Token { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public AuthEntity() { }
        public AuthEntity(AuthViewModel authViewModel)
        {
            Email = authViewModel.Email;
            Password = authViewModel.Password;
        }
    }
}