using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using api_bookStore.App.Modules.Book.Entity;
using api_bookStore.App.Modules.Category.ViewModel;

namespace api_bookStore.App.Modules.Category.Entity
{
    public class CategoryEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public IList<BookEntity> Books { get; set; } = [];
        public CategoryEntity() { }
        public CategoryEntity(CategoryViewModelCreate categoryViewModelCreate)
        {
            Name = categoryViewModelCreate.Name;
            Description = categoryViewModelCreate.Description;
        }
    }
}