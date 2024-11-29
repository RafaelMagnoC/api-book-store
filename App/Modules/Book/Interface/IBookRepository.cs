using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_bookStore.App.Modules.Book.DTO;
using api_bookStore.App.Modules.Book.ViewModel;

namespace api_bookStore.App.Modules.Book.Interface
{
    public interface IBookRepository
    {
        Task<BookDTO> BookAdd(BookViewModelCreate BookViewModel);

        Task<BookDTO> BookAtt(int id, BookViewModelUpdate BookViewModel);

        Task<bool> BookDel(int id);

        Task<List<BookDTO>> Books(string? categoryaName = null);

        Task<BookDTO> Book(int id);
        Task<BookDTO> BookByTitle(string title);


    }
}