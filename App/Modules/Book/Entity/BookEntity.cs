using System.ComponentModel.DataAnnotations.Schema;
using api_bookStore.App.Modules.Author.Entity;
using api_bookStore.App.Modules.Book.ViewModel;
using api_bookStore.App.Modules.Category.Entity;
using api_bookStore.App.Modules.Inventory.Entity;
using api_bookStore.App.Modules.Sale.Entity;

namespace api_bookStore.App.Modules.Book.Entity
{
    [Table("book")]
    public class BookEntity
    {
        #region Propriedades
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public DateOnly PublicationDate { get; set; }
        public double Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        #endregion

        #region Relacionamentos | Navegação
        public int AuthorId { get; set; }
        public AuthorEntity Author { get; set; } = null!;
        public int CategoryId { get; set; }
        public CategoryEntity Category { get; set; } = null!;
        public InventoryEntity Inventory { get; set; } = null!;
        public IList<SaleBookEntity> SaleBook { get; set; } = [];

        #endregion

        #region Construtores
        public BookEntity() { }
        public BookEntity(BookViewModelCreate bookViewModelCreate)
        {
            Title = bookViewModelCreate.Title;
            PublicationDate = bookViewModelCreate.PublicationDate;
            Price = bookViewModelCreate.Price;
        }

        #endregion

    }
}