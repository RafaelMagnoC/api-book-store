using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using api_bookStore.App.Enums;
using api_bookStore.App.Modules.Sale.ViewModel;

namespace api_bookStore.App.Modules.Sale.Entity
{
    public class SaleEntity
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public SaleStatusEnum Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public IList<SaleBookEntity> SaleBook { get; set; } = [];
        public SaleEntity() { }
        public SaleEntity(SaleViewModelCreate saleViewModelCreate)
        {
            Status = SaleStatusEnum.closed;
        }

    }
}