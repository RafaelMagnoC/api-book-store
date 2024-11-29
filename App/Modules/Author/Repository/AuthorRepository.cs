using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_bookStore.App.DataBase;
using api_bookStore.App.Modules.Author.DTO;
using api_bookStore.App.Modules.Author.Entity;
using api_bookStore.App.Modules.Author.Interface;
using api_bookStore.App.Modules.Author.ViewModel;
using api_BookStore.App.Exceptions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace api_bookStore.App.Modules.Author.Repository
{
    public class AuthorRepository(BookStoreContext bookStoreContext, IMapper mapper) : IAuthorRepository
    {

        private readonly BookStoreContext _bookStoreContext = bookStoreContext;
        private readonly IMapper _mapper = mapper;

        /// <summary>
        /// Adiciona um novo autor ao sistema.
        /// </summary>
        /// <param name="authorViewModelCreate">O modelo de autor que será adicionado.</param>
        /// <returns>O autor recém-adicionado.</returns>
        /// <exception cref="Exception">Lançado quando ocorre um erro interno de servidor.</exception>
        public async Task<AuthorDTO> AuthorAdd(AuthorViewModelCreate authorViewModelCreate)
        {
            try
            {
                EntityEntry<AuthorEntity> authorCreated = await _bookStoreContext.Author.AddAsync(new AuthorEntity(authorViewModelCreate));
                int authorSaved = await _bookStoreContext.SaveChangesAsync();

                return authorSaved > 0 ? _mapper.Map<AuthorDTO>(authorCreated.Entity) : throw new CreateException("um erro ocorreu ao cadastrar o autor");
            }
            catch (Exception exception)
            {
                throw new Exception(exception.ToString());
            }
        }

        /// <summary>
        /// Atualiza os dados de um autor no sistema pelo seu id.
        /// </summary>
        /// <param name="authorId">O id do autor pesquisado.</param>
        /// <param name="authorViewModelUpdate">O modelo de autor contendo os dados a serem cadastrados.</param>
        /// <returns>O autor com dados atualizados.</returns>
        /// <exception cref="NotFound">Lançado quando o autor não é encontrado pelo id.</exception>
        /// <exception cref="UpdateException">Lançado quando ocorre um erro durante a atualização dos dados.</exception>
        /// <exception cref="Exception">Lançado quando ocorre um erro interno de servidor.</exception>
        public async Task<AuthorDTO> AuthorAtt(int authorId, AuthorViewModelUpdate authorViewModelUpdate)
        {
            try
            {
                AuthorEntity authorExists = await _bookStoreContext.Author.FirstOrDefaultAsync(u => u.Id == authorId) ?? throw new NotFound($"nenhum autor com o id: {authorId} encontrado.");
                _bookStoreContext.Entry(authorExists).CurrentValues.SetValues(authorViewModelUpdate);
                authorExists.UpdatedAt = DateTime.Now;

                _bookStoreContext.Author.Update(authorExists);

                int AuthorUpdated = await _bookStoreContext.SaveChangesAsync();

                return AuthorUpdated > 0 ? _mapper.Map<AuthorDTO>(authorExists) : throw new UpdateException("um erro ocorreu ao atualizar o autor");
            }
            catch (Exception exception)
            {
                throw new Exception(exception.ToString());
            }

        }

        /// <summary>
        /// Deleta um autor no sistema pelo seu id.
        /// </summary>
        /// <param name="authorId">O id do autor a ser deletado.</param>
        /// <returns>True se o autor for deletado com sucesso.</returns>
        /// <exception cref="NotFound">Lançado quando o autor não é encontrado pelo id.</exception>
        /// <exception cref="RemoveException">Lançado quando ocorre um erro durante a exclusão.</exception>
        /// <exception cref="Exception">Lançado quando ocorre um erro interno de servidor.</exception>
        public async Task<bool> AuthorDel(int authorId)
        {
            try
            {
                AuthorEntity authorExists = await _bookStoreContext.Author.FirstOrDefaultAsync(u => u.Id == authorId) ?? throw new NotFound($"nenhum autor com o id: {authorId} encontrado.");

                _bookStoreContext.Author.Remove(authorExists);

                int authorSuccessfullyRemoved = await _bookStoreContext.SaveChangesAsync();

                return authorSuccessfullyRemoved > 0 ? true : throw new RemoveException("um erro ocorreu ao remover o autor");
            }
            catch (Exception exception)
            {
                throw new Exception(exception.ToString());
            }

        }
        /// <summary>
        /// Busca um autor no sistema pelo seu id.
        /// </summary>
        /// <param name="authorId">O id do autor pesquisado.</param>
        /// <returns>O autor encontrado.</returns>
        /// <exception cref="Exception">Lançado quando ocorre um erro interno de servidor.</exception>
        public async Task<AuthorDTO> Author(int authorId)
        {
            try
            {
                AuthorEntity authorExists = await _bookStoreContext.Author.AsNoTracking().FirstOrDefaultAsync(u => u.Id == authorId) ?? throw new NotFound($"nenhum autor com o id: {authorId} encontrado.");

                return _mapper.Map<AuthorDTO>(authorExists);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.ToString());
            }

        }

        /// <summary>
        /// Busca todos autores cadastrados no sistema.
        /// </summary>
        /// <returns>Lista vazia ou de autores cadastrados.</returns>
        /// <exception cref="Exception">Lançado quando ocorre um erro interno de servidor.</exception>
        public async Task<List<AuthorDTO>> Authors()
        {
            try
            {
                List<AuthorEntity> authors = await _bookStoreContext.Author.AsNoTracking().ToListAsync();
                return authors.Count > 0 ? _mapper.Map<List<AuthorDTO>>(authors) : [];
            }
            catch (Exception exception)
            {
                throw new Exception(exception.ToString());
            }

        }
    }
}