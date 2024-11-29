using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using api_bookStore.App.Enums;

namespace api_bookStore.App.Modules.Author.DTO
{
    public class AuthorDTO
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public DateOnly BirthDay { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public CountriesEnum Country { get; set; }
    }
}