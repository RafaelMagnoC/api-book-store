using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using api_bookStore.App.Enums;
using api_bookStore.App.Modules.Author.ViewModel;
using api_bookStore.App.Modules.Book.Entity;

namespace api_bookStore.App.Modules.Author.Entity
{
    public class AuthorEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateOnly BirthDay { get; set; }
        public CountriesEnum Country { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public IList<BookEntity> Books { get; set; } = [];
        public AuthorEntity() { }
        public AuthorEntity(AuthorViewModelCreate authorViewModel)
        {
            Name = authorViewModel.Name;
            BirthDay = authorViewModel.BirthDay;
            Country = authorViewModel.Country;
        }
    }
}