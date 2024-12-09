using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_bookStore.App.Enums;
using api_bookStore.App.Modules.Book.DTO;
using api_bookStore.App.Modules.Book.Entity;

namespace api_bookStore.App.Modules.Sale.DTO
{

    public class SaleDTO
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public SaleStatusEnum Status { get; set; }
        public IList<SaleBookDTO> SaleBook { get; set; } = null!;
    }
}