using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace api_bookStore.App.Modules.User.Service
{
    public class PasswordServiceHash : IPasswordServiceHash
    {
        private readonly PasswordHasher<object> _passwordHasher = new();

        public string HashPassword(string password)
        {
            return _passwordHasher.HashPassword(string.Empty, password);
        }

        public bool VerifyPassword(string hashedPassword, string providedPassword)
        {
            var result = _passwordHasher.VerifyHashedPassword(string.Empty, hashedPassword, providedPassword);
            return result == PasswordVerificationResult.Success;
        }
    }
}