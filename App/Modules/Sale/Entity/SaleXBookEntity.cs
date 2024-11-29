using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using api_bookStore.App.Modules.Book.Entity;

namespace api_bookStore.App.Modules.Sale.Entity
{
    [Table("sale_x_book")]
    public class SaleXBookEntity
    {

        [Key]
        [Column("sale_book_id")]
        public int Id { get; set; }
        [Column("sale_price")]
        public double Price { get; set; }
        [Column("sale_quantity")]
        public int Quantity { get; set; }

        [Column("sale_id")]
        public int SaleId { get; set; }
        public SaleEntity? Sale { get; set; }

        [Column("book_id")]
        public int BookId { get; set; }
        public BookEntity? Book { get; set; }
        public SaleXBookEntity() { }
        public SaleXBookEntity(int bookId, int saleId)
        {
            BookId = bookId;
            SaleId = saleId;
        }
    }
}