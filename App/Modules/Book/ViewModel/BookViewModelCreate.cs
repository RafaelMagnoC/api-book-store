using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using api_bookStore.App.Modules.Inventory.ViewModel;

namespace api_bookStore.App.Modules.Book.ViewModel
{
    public class BookViewModelCreate(string title, DateOnly publicationDate, double price, int quantity, int authorId, int categoryId)
    {
        [Required]
        public string Title { get; set; } = title;

        [Required]
        public DateOnly PublicationDate { get; set; } = publicationDate;

        [Required]
        public double Price { get; set; } = price;

        [Required]
        public int Quantity { get; set; } = quantity;

        [Required]
        public int AuthorId { get; set; } = authorId;

        [Required]
        public int CategoryId { get; set; } = categoryId;
    }
}