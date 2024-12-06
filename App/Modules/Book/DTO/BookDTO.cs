using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_bookStore.App.Modules.Inventory.Entity;

namespace api_bookStore.App.Modules.Book.DTO
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public DateOnly PublicationDate { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string Category { get; set; } = null!;
        public string Author { get; set; } = null!;
    }
}