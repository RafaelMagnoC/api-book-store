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
    [Table("author")]
    public class AuthorEntity
    {
        [Key]
        [Column("author_id")]
        public int Id { get; set; }
        [Column("author_name")]
        public string? Name { get; set; }
        [Column("author_birthday", TypeName = "Date")]
        public DateOnly BirthDay { get; set; }
        [Column("author_country")]
        public CountriesEnum Country { get; set; }
        [Column("author_created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [Column("author_updated_at")]
        public DateTime UpdatedAt { get; set; }
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