using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_bookStore.App.Modules.User.DTO;
using api_bookStore.App.Modules.User.Entity;
using api_bookStore.App.Modules.User.ViewModel;

namespace api_bookStore.App.Modules.User.Interface
{
    public interface IUserRepository
    {
        Task<UserDTO> UserAdd(UserViewModelCreate userViewModelCreate);

        Task<UserDTO> UserAtt(string id, UserViewModelUpdate userViewModelUpdate);

        Task<bool> UserDel(string id);

        Task<List<UserDTO>> Users();

        Task<UserDTO> User(string id);

        Task<UserEntity> UserByEmail(string email);
    }
}