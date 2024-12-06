using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using api_bookStore.App.Modules.Book.Entity;

namespace api_bookStore.App.Modules.Sale.Entity
{
    public class SaleBookEntity
    {
        public int Id { get; set; }
        public int SaleId { get; set; }
        public SaleEntity Sale { get; set; } = null!;
        public int BookId { get; set; }
        public BookEntity Book { get; set; } = null!;
        public SaleBookEntity() { }
        public SaleBookEntity(int bookId, int saleId)
        {
            BookId = bookId;
            SaleId = saleId;
        }
    }
}