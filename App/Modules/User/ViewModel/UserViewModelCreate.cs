using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using api_bookStore.App.Enums;

namespace api_bookStore.App.Modules.User.ViewModel
{
    public class UserViewModelCreate
    {
        [Required]
        [StringLength(60)]
        public required string Name { get; set; }

        [Required]
        [StringLength(60)]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        [StringLength(20)]
        public required string Password { get; set; }

        [Required]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public RolesEnum Role { get; set; }
    }
}