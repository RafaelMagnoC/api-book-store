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
    [Table("inventory_book")]
    public class InventoryEntity
    {
        [Key]
        [Column("inventory_id")]
        public int Id { get; set; }
        [Column("inventory_value")]
        public double Value { get; set; }
        [Column("inventory_quantity")]
        public int Quantity { get; set; }
        [Column("inventory_created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [Column("inventory_updated_at")]
        public DateTime UpdatedAt { get; set; }
        public BookEntity? Book { get; set; }
        public InventoryEntity() { }
        public InventoryEntity(InventoryViewModelCreate inventoryViewModelCreate)
        {
            Quantity = inventoryViewModelCreate.Quantity;
        }

    }
}