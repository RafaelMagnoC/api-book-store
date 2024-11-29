using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_bookStore.App.Modules.Inventory.ViewModel;

namespace api_bookStore.App.Modules.Book.ViewModel
{
    public class BookViewModelCreate(string title, DateOnly publicationDate, double price, int quantity, int authorId, int categoryId)
    {
        public string Title { get; set; } = title;
        public DateOnly PublicationDate { get; set; } = publicationDate;
        public double Price { get; set; } = price;
        public int Quantity { get; set; } = quantity;
        public int AuthorId { get; set; } = authorId;
        public int CategoryId { get; set; } = categoryId;
    }
}