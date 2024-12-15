using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_bookStore.App.Modules.Sale.DTO
{
    public class SaleBookDTO
    {
        public string BookTitle { get; set; } = null!;
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double Subtotal { get; set; }

    }
}