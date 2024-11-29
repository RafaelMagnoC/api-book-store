using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_bookStore.App.Modules.Auth.ViewModel;

namespace api_bookStore.App.Modules.Auth.Interface
{
    public interface IAuthRepository
    {
        Task<string> SigIn(AuthViewModel authViewModel);
    }
}