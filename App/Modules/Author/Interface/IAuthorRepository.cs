using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_bookStore.App.Modules.Author.DTO;
using api_bookStore.App.Modules.Author.ViewModel;

namespace api_bookStore.App.Modules.Author.Interface
{
    public interface IAuthorRepository
    {
        Task<AuthorDTO> AuthorAdd(AuthorViewModelCreate AuthorViewModel);

        Task<AuthorDTO> AuthorAtt(int id, AuthorViewModelUpdate AuthorViewModel);

        Task<bool> AuthorDel(int id);

        Task<List<AuthorDTO>> Authors();

        Task<AuthorDTO> Author(int id);

    }
}