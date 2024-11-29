using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_bookStore.App.DataBase;
using api_bookStore.App.Modules.Category.DTO;
using api_bookStore.App.Modules.Category.Entity;
using api_bookStore.App.Modules.Category.Interface;
using api_bookStore.App.Modules.Category.ViewModel;
using api_BookStore.App.Exceptions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace api_bookStore.App.Modules.Category.Repository
{
    public class CategoryRepository(BookStoreContext bookStoreContext, IMapper mapper) : ICategoryRepository
    {

        private readonly BookStoreContext _bookStoreContext = bookStoreContext;
        private readonly IMapper _mapper = mapper;

        /// <summary>
        /// Adiciona uma nova categoria ao sistema.
        /// </summary>
        /// <param name="categoryViewModelCreate">O modelo de categoria que será adicionada.</param>
        /// <returns>a categoria recém-adicionada.</returns>
        /// <exception cref="Exception">Lançado quando ocorre um erro interno de servidor.</exception>
        public async Task<CategoryDTO> CategoryAdd(CategoryViewModelCreate categoryViewModelCreate)
        {
            try
            {
                EntityEntry<CategoryEntity> categoryCreated = await _bookStoreContext.Category.AddAsync(new CategoryEntity(categoryViewModelCreate));
                int categorySaved = await _bookStoreContext.SaveChangesAsync();

                return categorySaved > 0 ? _mapper.Map<CategoryDTO>(categoryCreated.Entity) : throw new CreateException("um erro ocorreu ao cadastrar a categoria");
            }
            catch (Exception exception)
            {
                throw new Exception(exception.ToString());
            }
        }

        /// <summary>
        /// Atualiza os dados de uma categoria no sistema pelo seu id.
        /// </summary>
        /// <param name="categoryId">O id da categoria pesquisado.</param>
        /// <param name="categoryViewModelUpdate">O modelo de categoria contendo os dados a serem cadastrados.</param>
        /// <returns>a categoria com dados atualizados.</returns>
        /// <exception cref="NotFound">Lançado quando a categoria não é encontrada pelo id.</exception>
        /// <exception cref="UpdateException">Lançado quando ocorre um erro durante a atualização dos dados.</exception>
        /// <exception cref="Exception">Lançado quando ocorre um erro interno de servidor.</exception>
        public async Task<CategoryDTO> CategoryAtt(int categoryId, CategoryViewModelUpdate categoryViewModelUpdate)
        {
            try
            {
                CategoryEntity categoryExists = await _bookStoreContext.Category.FirstOrDefaultAsync(category => category.Id == categoryId) ?? throw new NotFound($"nenhuma categoria com o id: {categoryId} encontrada.");
                _bookStoreContext.Entry(categoryExists).CurrentValues.SetValues(categoryViewModelUpdate);
                categoryExists.UpdatedAt = DateTime.Now;

                int categoryUpdated = await _bookStoreContext.SaveChangesAsync();

                return categoryUpdated > 0 ? _mapper.Map<CategoryDTO>(categoryExists) : throw new UpdateException("um erro ocorreu ao atualizar a categoria");
            }
            catch (Exception exception)
            {
                throw new Exception(exception.ToString());
            }

        }

        /// <summary>
        /// Deleta uma categoria no sistema pelo seu id.
        /// </summary>
        /// <param name="categoryId">O id da categoria a ser deletado.</param>
        /// <returns>True se a categoria for deletado com sucesso.</returns>
        /// <exception cref="NotFound">Lançado quando a categoria não é encontrada pelo id.</exception>
        /// <exception cref="RemoveException">Lançado quando ocorre um erro durante a exclusão.</exception>
        /// <exception cref="Exception">Lançado quando ocorre um erro interno de servidor.</exception>
        public async Task<bool> CategoryDel(int categoryId)
        {
            try
            {
                CategoryEntity categoryExists = await _bookStoreContext.Category.FirstOrDefaultAsync(category => category.Id == categoryId) ?? throw new NotFound($"nenhuma categoria com o id: {categoryId} encontrada.");

                _bookStoreContext.Category.Remove(categoryExists);

                int categorySuccessfullyRemoved = await _bookStoreContext.SaveChangesAsync();

                return categorySuccessfullyRemoved > 0 ? true : throw new RemoveException("um erro ocorreu ao remover a categoria");
            }
            catch (Exception exception)
            {
                throw new Exception(exception.ToString());
            }

        }
        /// <summary>
        /// Busca uma categoria no sistema pelo seu id.
        /// </summary>
        /// <param name="categoryId">O id da categoria pesquisado.</param>
        /// <returns>a categoria encontrada.</returns>
        /// <exception cref="Exception">Lançado quando ocorre um erro interno de servidor.</exception>
        public async Task<CategoryDTO> Category(int categoryId)
        {
            try
            {
                CategoryEntity categoryExists = await _bookStoreContext.Category.AsNoTracking().FirstOrDefaultAsync(category => category.Id == categoryId) ?? throw new NotFound($"nenhuma categoria com o id: {categoryId} encontrada.");

                return _mapper.Map<CategoryDTO>(categoryExists);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.ToString());
            }

        }

        /// <summary>
        /// Busca todas categorias cadastradas no sistema.
        /// </summary>
        /// <returns>Lista vazia ou de categorias cadastradas.</returns>
        /// <exception cref="Exception">Lançado quando ocorre um erro interno de servidor.</exception>
        public async Task<List<CategoryDTO>> Categories()
        {
            try
            {
                List<CategoryEntity> categories = await _bookStoreContext.Category.AsNoTracking().ToListAsync();
                return categories.Count > 0 ? _mapper.Map<List<CategoryDTO>>(categories) : [];
            }
            catch (Exception exception)
            {
                throw new Exception(exception.ToString());
            }

        }
    }
}