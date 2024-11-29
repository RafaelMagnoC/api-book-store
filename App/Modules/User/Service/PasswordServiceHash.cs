using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace api_bookStore.App.Modules.User.Service
{
    public static class PasswordServiceHash
    {
        private static readonly PasswordHasher<object> _passwordHasher = new();

        public static string HashPassword(string password)
        {
            return _passwordHasher.HashPassword(string.Empty, password);
        }
        public static bool VerifyPassword(string hashedPassword, string providedPassword)
        {
            var result = _passwordHasher.VerifyHashedPassword(string.Empty, hashedPassword, providedPassword);
            return result == PasswordVerificationResult.Success;
        }
    }
}