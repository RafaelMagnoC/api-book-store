using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using api_bookStore.App.Enums;

namespace api_bookStore.App.Modules.User.ViewModel
{
    public class UserViewModelUpdate
    {
        [StringLength(60)]
        public string? Name { get; set; }

        [StringLength(60)]
        [EmailAddress]
        public string? Email { get; set; }

        [StringLength(20)]
        public string? Password { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public RolesEnum? Role { get; set; }
    }
}