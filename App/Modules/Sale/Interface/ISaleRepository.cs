using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_bookStore.App.Modules.Sale.DTO;
using api_bookStore.App.Modules.Sale.Entity;
using api_bookStore.App.Modules.Sale.ViewModel;

namespace api_bookStore.App.Modules.Sale.Interface
{
    public interface ISaleRepository
    {
        Task<SaleDTO> SaleAdd(List<SaleViewModelCreate> saleViewModelCreate);
        Task<List<SaleDTO>> Sales();
    }
}