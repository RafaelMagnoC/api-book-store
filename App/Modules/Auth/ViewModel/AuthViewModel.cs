using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api_bookStore.App.Modules.Auth.ViewModel
{
    public class AuthViewModel(string email, string password)
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = email;
        [Required]
        public string Password { get; set; } = password;

        public void Deconstruct(out string email, out string password)
        {
            email = Email;
            password = Password;
        }
    }
}