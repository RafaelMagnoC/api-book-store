using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using api_bookStore.App.Enums;

namespace api_bookStore.App.Modules.Author.ViewModel
{
    public class AuthorViewModelCreate(string name, DateOnly birthday)
    {
        [Required]
        public string Name { get; set; } = name;

        [Required]
        public DateOnly BirthDay { get; set; } = birthday;

        [Required]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public required CountriesEnum Country { get; set; }
    }

}