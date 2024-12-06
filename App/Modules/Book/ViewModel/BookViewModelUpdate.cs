using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_bookStore.App.Modules.Book.ViewModel
{
    public class BookViewModelUpdate
    {
        public string? Title { get; set; }
        public DateOnly? PublicationDate { get; set; }
        public double? Price { get; set; }
    }
}