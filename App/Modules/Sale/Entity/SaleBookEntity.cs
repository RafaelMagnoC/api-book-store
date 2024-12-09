using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using api_bookStore.App.Modules.Book.Entity;

namespace api_bookStore.App.Modules.Sale.Entity
{
    public class SaleBookEntity
    {
        #region Propriedades

        public int Id { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double SubTotal { get; set; }

        #endregion

        #region Relacionamento | Navegação

        public int SaleId { get; set; }
        public SaleEntity Sale { get; set; } = null!;
        public int BookId { get; set; }
        public BookEntity Book { get; set; } = null!;

        #endregion

        #region construtores

        public SaleBookEntity() { }
        public SaleBookEntity(int bookId, int saleId)
        {
            BookId = bookId;
            SaleId = saleId;
        }

        #endregion
    }
}