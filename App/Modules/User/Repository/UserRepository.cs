using api_bookStore.App.DataBase;
using api_bookStore.App.Modules.User.DTO;
using api_bookStore.App.Modules.User.Entity;
using api_bookStore.App.Modules.User.Interface;
using api_bookStore.App.Modules.User.Service;
using api_bookStore.App.Modules.User.ViewModel;
using api_BookStore.App.Exceptions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace api_bookStore.App.Modules.User.Repository
{
    public class UserRepository(BookStoreContext bookStoreContext, IMapper mapper, IPasswordServiceHash passwordServiceHash) : IUserRepository
    {

        private readonly BookStoreContext _bookStoreContext = bookStoreContext;
        private readonly IMapper _mapper = mapper;
        private readonly IPasswordServiceHash _passwordServiceHash = passwordServiceHash;

        /// <summary>
        /// Adiciona um novo usuário ao sistema.
        /// </summary>
        /// <param name="userViewModelCreate">O modelo de usuário que será adicionado.</param>
        /// <returns>O usuário recém-adicionado.</returns>
        /// <exception cref="Exception">Lançado quando ocorre um erro interno de servidor.</exception>
        public async Task<UserDTO> UserAdd(UserViewModelCreate userViewModelCreate)
        {
            try
            {
                userViewModelCreate.Password = _passwordServiceHash.HashPassword(userViewModelCreate.Password);
                EntityEntry<UserEntity> userCreated = await _bookStoreContext.User.AddAsync(new UserEntity(userViewModelCreate));
                int userSaved = await _bookStoreContext.SaveChangesAsync();

                return userSaved > 0 ? _mapper.Map<UserDTO>(userCreated.Entity) : throw new CreateException("um erro ocorreu ao cadastrar o usuário");
            }
            catch (Exception exception)
            {
                throw new Exception(exception.ToString());
            }
        }

        /// <summary>
        /// Atualiza os dados de um usuário no sistema pelo seu id.
        /// </summary>
        /// <param name="userId">O id do usuário pesquisado.</param>
        /// <param name="userViewModelUpdate">O modelo de usuário contendo os dados a serem cadastrados.</param>
        /// <returns>O usuário com dados atualizados.</returns>
        /// <exception cref="NotFound">Lançado quando o usuário não é encontrado pelo id.</exception>
        /// <exception cref="UpdateException">Lançado quando ocorre um erro durante a atualização dos dados.</exception>
        /// <exception cref="Exception">Lançado quando ocorre um erro interno de servidor.</exception>
        public async Task<UserDTO> UserAtt(string userId, UserViewModelUpdate userViewModelUpdate)
        {
            try
            {
                UserEntity userExists = await _bookStoreContext.User.FirstOrDefaultAsync(u => u.Id.ToString() == userId) ?? throw new NotFound($"nenhum usuário com o id: {userId} encontrado.");
                if (userViewModelUpdate.Password != null)
                {
                    userViewModelUpdate.Password = _passwordServiceHash.HashPassword(userViewModelUpdate.Password);
                }
                _bookStoreContext.Entry(userExists).CurrentValues.SetValues(userViewModelUpdate);
                userExists.UpdatedAt = DateTime.Now;

                int userUpdated = await _bookStoreContext.SaveChangesAsync();

                return userUpdated > 0 ? _mapper.Map<UserDTO>(userExists) : throw new UpdateException("um erro ocorreu ao atualizar o usuário");
            }
            catch (Exception exception)
            {
                throw new Exception(exception.ToString());
            }

        }

        /// <summary>
        /// Deleta um usuário no sistema pelo seu id.
        /// </summary>
        /// <param name="userId">O id do usuário a ser deletado.</param>
        /// <returns>True se o usuário for deletado com sucesso.</returns>
        /// <exception cref="NotFound">Lançado quando o usuário não é encontrado pelo id.</exception>
        /// <exception cref="RemoveException">Lançado quando ocorre um erro durante a exclusão.</exception>
        /// <exception cref="Exception">Lançado quando ocorre um erro interno de servidor.</exception>
        public async Task<bool> UserDel(string userId)
        {
            try
            {
                UserEntity userExists = await _bookStoreContext.User.AsNoTracking().FirstOrDefaultAsync(u => u.Id.ToString() == userId) ?? throw new NotFound($"nenhum usuário com o id: {userId} encontrado.");

                _bookStoreContext.User.Remove(userExists);

                int userSuccessfullyRemoved = await _bookStoreContext.SaveChangesAsync();

                return userSuccessfullyRemoved > 0 ? true : throw new RemoveException("um erro ocorreu ao remover o usuário");
            }
            catch (Exception exception)
            {
                throw new Exception(exception.ToString());
            }

        }
        /// <summary>
        /// Busca um usuário no sistema pelo seu id.
        /// </summary>
        /// <param name="userId">O id do usuário pesquisado.</param>
        /// <returns>O usuário encontrado.</returns>
        /// <exception cref="Exception">Lançado quando ocorre um erro interno de servidor.</exception>
        public async Task<UserDTO> User(string userId)
        {
            try
            {
                UserEntity userExists = await _bookStoreContext.User.AsNoTracking().FirstOrDefaultAsync(u => u.Id.ToString() == userId) ?? throw new NotFound($"nenhum usuário com o id: {userId} encontrado.");

                return _mapper.Map<UserDTO>(userExists);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.ToString());
            }

        }

        /// <summary>
        /// Busca um usuário no sistema pelo seu email.
        /// </summary>
        /// <param name="email">O id do usuário pesquisado.</param>
        /// <returns>O usuário encontrado.</returns>
        /// <exception cref="Exception">Lançado quando ocorre um erro interno de servidor.</exception>
        public async Task<UserEntity> UserByEmail(string email)
        {
            try
            {
                UserEntity userExists = await _bookStoreContext.User.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email) ?? throw new NotFound($"nenhum usuário com o e-mail: {email} encontrado.");

                return userExists;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.ToString());
            }

        }

        /// <summary>
        /// Busca todos usuários cadastrados no sistema.
        /// </summary>
        /// <returns>Lista vazia ou de usuarios cadastrados.</returns>
        /// <exception cref="Exception">Lançado quando ocorre um erro interno de servidor.</exception>
        public async Task<List<UserDTO>> Users()
        {
            try
            {
                List<UserEntity> users = await _bookStoreContext.User.AsNoTracking().ToListAsync();
                return users.Count > 0 ? _mapper.Map<List<UserDTO>>(users) : [];
            }
            catch (Exception exception)
            {
                throw new Exception(exception.ToString());
            }

        }
    }
}