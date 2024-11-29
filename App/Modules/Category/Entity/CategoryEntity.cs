using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using api_bookStore.App.Modules.Book.Entity;
using api_bookStore.App.Modules.Category.ViewModel;

namespace api_bookStore.App.Modules.Category.Entity
{
    [Table("category")]
    public class CategoryEntity
    {
        [Key]
        [Column("category_id")]
        public int Id { get; set; }
        [Column("category_name")]
        public string? Name { get; set; }
        [Column("category_description")]
        public string? Description { get; set; }
        [Column("category_created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [Column("category_updated_at")]
        public DateTime UpdatedAt { get; set; }
        public IList<BookEntity> Books { get; set; } = [];
        public CategoryEntity() { }
        public CategoryEntity(CategoryViewModelCreate categoryViewModelCreate)
        {
            Name = categoryViewModelCreate.Name;
            Description = categoryViewModelCreate.Description;
        }
    }
}