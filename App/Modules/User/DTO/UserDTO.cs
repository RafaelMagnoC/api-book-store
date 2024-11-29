using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using api_bookStore.App.Enums;

namespace api_bookStore.App.Modules.User.DTO
{
    public class UserDTO
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public required RolesEnum Role { get; set; }
    }
}