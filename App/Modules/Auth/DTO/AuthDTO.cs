using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api_bookStore.App.Modules.Auth.DTO
{
    public class AuthDTO
    {
        public string? UserId { get; set; }
        public string? Email { get; set; }
        public string? Token { get; set; }

    }
}   