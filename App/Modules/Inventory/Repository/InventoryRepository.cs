using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_bookStore.App.DataBase;
using api_bookStore.App.Modules.Inventory.DTO;
using api_bookStore.App.Modules.Inventory.Entity;
using api_bookStore.App.Modules.Inventory.Interface;
using api_bookStore.App.Modules.Inventory.ViewModel;
using api_BookStore.App.Exceptions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace api_bookStore.App.Modules.Inventory.Repository
{
    public class InventoryRepository(BookStoreContext bookStoreContext, IMapper mapper) : IInventoryRepository
    {

        private readonly BookStoreContext _bookStoreContext = bookStoreContext;
        private readonly IMapper _mapper = mapper;

        /// <summary>
        /// Adiciona uma quantidade de produto ao sistema.
        /// </summary>
        /// <param name="inventoryViewModelCreate">O modelo de inventário que será adicionada.</param>
        /// <returns>a quantidade do produto recém-adicionada.</returns>
        /// <exception cref="Exception">Lançado quando ocorre um erro interno de servidor.</exception>
        public async Task<InventoryDTO> InventoryAdd(InventoryViewModelCreate inventoryViewModelCreate)
        {
            try
            {
                EntityEntry<InventoryEntity> inventoryCreated = await _bookStoreContext.Inventory.AddAsync(new InventoryEntity(inventoryViewModelCreate));
                int inventorySaved = await _bookStoreContext.SaveChangesAsync();

                return inventorySaved > 0 ? _mapper.Map<InventoryDTO>(inventoryCreated.Entity) : throw new CreateException("um erro ocorreu ao cadastrar o inventário");
            }
            catch (Exception exception)
            {
                throw new Exception(exception.ToString());
            }
        }

        /// <summary>
        /// Atualiza os dados de um inventário no sistema pelo seu id.
        /// </summary>
        /// <param name="inventoryId">O id da inventário pesquisado.</param>
        /// <param name="inventoryViewModelUpdate">O modelo de inventário contendo os dados a serem cadastrados.</param>
        /// <returns>a inventário com dados atualizados.</returns>
        /// <exception cref="NotFound">Lançado quando a inventário não é encontrado pelo id.</exception>
        /// <exception cref="UpdateException">Lançado quando ocorre um erro durante a atualização dos dados.</exception>
        /// <exception cref="Exception">Lançado quando ocorre um erro interno de servidor.</exception>
        public async Task<InventoryDTO> InventoryAtt(string inventoryId, InventoryViewModelUpdate inventoryViewModelUpdate)
        {
            try
            {
                InventoryEntity inventoryExists = await _bookStoreContext.Inventory.FirstOrDefaultAsync(u => u.Id.ToString() == inventoryId) ?? throw new NotFound($"nenhum inventário com o id: {inventoryId} encontrado.");
                _bookStoreContext.Entry(inventoryExists).CurrentValues.SetValues(inventoryViewModelUpdate);
                inventoryExists.UpdatedAt = DateTime.Now;

                int inventoryUpdated = await _bookStoreContext.SaveChangesAsync();

                return inventoryUpdated > 0 ? _mapper.Map<InventoryDTO>(inventoryExists) : throw new UpdateException("um erro ocorreu ao atualizar a inventário");
            }
            catch (Exception exception)
            {
                throw new Exception(exception.ToString());
            }

        }

        /// <summary>
        /// Deleta um inventário no sistema pelo seu id.
        /// </summary>
        /// <param name="inventoryId">O id da inventário a ser deletado.</param>
        /// <returns>True se o inventário for deletado com sucesso.</returns>
        /// <exception cref="NotFound">Lançado quando o inventário não é encontrado pelo id.</exception>
        /// <exception cref="RemoveException">Lançado quando ocorre um erro durante a exclusão.</exception>
        /// <exception cref="Exception">Lançado quando ocorre um erro interno de servidor.</exception>
        public async Task<bool> InventoryDel(string inventoryId)
        {
            try
            {
                InventoryEntity inventoryExists = await _bookStoreContext.Inventory.FirstOrDefaultAsync(u => u.Id.ToString() == inventoryId) ?? throw new NotFound($"nenhum inventário com o id: {inventoryId} encontrado.");

                _bookStoreContext.Inventory.Remove(inventoryExists);

                int inventorySuccessfullyRemoved = await _bookStoreContext.SaveChangesAsync();

                return inventorySuccessfullyRemoved > 0 ? true : throw new RemoveException("um erro ocorreu ao remover a inventário");
            }
            catch (Exception exception)
            {
                throw new Exception(exception.ToString());
            }

        }
        /// <summary>
        /// Busca um inventário no sistema pelo seu id.
        /// </summary>
        /// <param name="inventoryId">O id do inventário pesquisado.</param>
        /// <returns>a inventário encontrado.</returns>
        /// <exception cref="Exception">Lançado quando ocorre um erro interno de servidor.</exception>
        public async Task<InventoryDTO> Inventory(string inventoryId)
        {
            try
            {
                InventoryEntity inventoryExists = await _bookStoreContext.Inventory.AsNoTracking().FirstOrDefaultAsync(u => u.Id.ToString() == inventoryId) ?? throw new NotFound($"nenhum inventário com o id: {inventoryId} encontrado.");

                return _mapper.Map<InventoryDTO>(inventoryExists);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.ToString());
            }

        }

        /// <summary>
        /// Busca todos inventários cadastradas no sistema.
        /// </summary>
        /// <returns>Lista vazia ou de inventários cadastrados.</returns>
        /// <exception cref="Exception">Lançado quando ocorre um erro interno de servidor.</exception>
        public async Task<List<InventoryDTO>> Inventories()
        {
            try
            {
                List<InventoryEntity> inventories = await _bookStoreContext.Inventory.AsNoTracking().ToListAsync();
                return inventories.Count > 0 ? _mapper.Map<List<InventoryDTO>>(inventories) : [];
            }
            catch (Exception exception)
            {
                throw new Exception(exception.ToString());
            }

        }
    }
}