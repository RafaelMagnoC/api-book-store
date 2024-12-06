using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using api_bookStore.App.Modules.Book.Entity;
using api_bookStore.App.Modules.Inventory.ViewModel;

namespace api_bookStore.App.Modules.Inventory.Entity
{
    public class InventoryEntity
    {
        public int Id { get; set; }
        public double Value { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int BookId { get; set; }
        public BookEntity? Book { get; set; }
        public InventoryEntity() { }
        public InventoryEntity(InventoryViewModelCreate inventoryViewModelCreate)
        {
            Quantity = inventoryViewModelCreate.Quantity;
        }

    }
}