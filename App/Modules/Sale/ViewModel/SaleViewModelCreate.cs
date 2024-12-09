using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_bookStore.App.Enums;

namespace api_bookStore.App.Modules.Sale.ViewModel
{
    public class SaleViewModelCreate(int bookId, int quantity)
    {
        public int BookId { get; set; } = bookId;
        public int Quantity { get; set; } = quantity;

    }
}