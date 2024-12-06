using api_bookStore.App.Enums;
using api_bookStore.App.Modules.User.ViewModel;

namespace api_bookStore.App.Modules.User.Entity
{
    public class UserEntity
    {
        public Guid Id { get; private set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public RolesEnum Role { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public UserEntity(UserViewModelCreate userViewModelCreate)
        {
            Name = userViewModelCreate.Name;
            Email = userViewModelCreate.Email;
            Password = userViewModelCreate.Password;
            Role = userViewModelCreate.Role;
        }
        public UserEntity() { }

    }
}