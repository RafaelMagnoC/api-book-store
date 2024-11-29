using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_bookStore.App.Modules.Inventory.DTO;
using api_bookStore.App.Modules.Inventory.ViewModel;

namespace api_bookStore.App.Modules.Inventory.Interface
{
    public interface IInventoryRepository
    {
        Task<InventoryDTO> InventoryAdd(InventoryViewModelCreate InventoryViewModelCreate);

        Task<InventoryDTO> InventoryAtt(string id, InventoryViewModelUpdate InventoryViewModelUpdate);

        Task<bool> InventoryDel(string id);

        Task<List<InventoryDTO>> Inventories();

        Task<InventoryDTO> Inventory(string id);
    }
}