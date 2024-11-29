using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using api_bookStore.App.Enums;
using api_bookStore.App.Modules.Sale.ViewModel;
using static api_bookStore.App.Modules.Sale.Entity.SaleXBookEntity;

namespace api_bookStore.App.Modules.Sale.Entity
{
    [Table("sale")]
    public class SaleEntity
    {
        [Key]
        [Column("sale_id")]
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }

        [Column("sale_status")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public SaleStatusEnum Status { get; set; }
        public IList<SaleXBookEntity> SaleXBooks { get; set; } = [];
        public SaleEntity() { }
        public SaleEntity(SaleViewModelCreate saleViewModelCreate)
        {
            Status = SaleStatusEnum.closed;
        }

    }
}