using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_bookStore.App.Modules.Category.DTO;
using api_bookStore.App.Modules.Category.ViewModel;

namespace api_bookStore.App.Modules.Category.Interface
{
    public interface ICategoryRepository
    {
        Task<CategoryDTO> CategoryAdd(CategoryViewModelCreate CategoryViewModelCreate);

        Task<CategoryDTO> CategoryAtt(int id, CategoryViewModelUpdate CategoryViewModelUpdate);

        Task<bool> CategoryDel(int id);

        Task<List<CategoryDTO>> Categories();

        Task<CategoryDTO> Category(int id);
    }
}