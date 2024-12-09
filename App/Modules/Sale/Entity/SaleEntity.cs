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
        #region Propriedades

        public int Id { get; set; }
        public double TotalValue { get; set; }
        public int TotalQuantity { get; set; }
        public SaleStatusEnum Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        #endregion

        #region Relacionamento | Navegação

        public IList<SaleBookEntity> SaleBook { get; set; } = [];

        #endregion

        #region Construtores

        public SaleEntity()
        {
            Status = SaleStatusEnum.closed;
        }

        #endregion

    }
}