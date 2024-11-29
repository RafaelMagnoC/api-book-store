using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_bookStore.App.Enums;

namespace api_bookStore.App.Modules.Sale.ViewModel
{
    public class SaleViewModelUpdate
    {
        public int? BookId { get; set; }
        public int? Quantity { get; set; }
        public SaleStatusEnum? Status { get; set; }

    }
}