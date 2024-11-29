using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_bookStore.App.Modules.Inventory.ViewModel
{
    public class InventoryViewModelCreate(int quantity)
    {
        public int Quantity { get; set; } = quantity;
    }
}