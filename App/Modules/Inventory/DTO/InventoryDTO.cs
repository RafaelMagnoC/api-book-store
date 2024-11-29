using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_bookStore.App.Modules.Inventory.DTO
{
    public class InventoryDTO
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public double Value { get; set; }
    }
}