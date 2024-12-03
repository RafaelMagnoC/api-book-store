using System.ComponentModel.DataAnnotations;
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
        [Key]
        [Column("book_id")]
        public int Id { get; set; }
        [Column("book_title")]
        public string Title { get; set; } = null!;
        [Column("book_publicationDate", TypeName = "Date")]
        public DateOnly PublicationDate { get; set; }
        [Column("book_price")]
        public double Price { get; set; }

        [Column("book_created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [Column("book_updated_at")]
        public DateTime UpdatedAt { get; set; }
        public BookEntity() { }
        public BookEntity(BookViewModelCreate bookViewModelCreate)
        {
            Title = bookViewModelCreate.Title;
            PublicationDate = bookViewModelCreate.PublicationDate;
            Price = bookViewModelCreate.Price;
        }
        [Column("book_authorId")]
        public int AuthorId { get; set; }
        public AuthorEntity Author { get; set; } = null!;
        [Column("book_categoryId")]
        public int CategoryId { get; set; }
        public CategoryEntity Category { get; set; } = null!;
        [Column("book_inventoryId")]
        public int InventoryId { get; set; }
        public InventoryEntity Quantity { get; set; } = null!;
        public IList<SaleXBookEntity> SaleXBooks { get; set; } = [];

    }
}