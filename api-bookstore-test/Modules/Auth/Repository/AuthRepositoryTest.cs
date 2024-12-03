using api_bookStore.App.Modules.Auth.ViewModel;
using api_bookStore.App.Modules.User.Entity;
using api_bookStore.App.Modules.User.Service;
using Moq;
using api_bookStore.App.Modules.Auth.Interface;
using api_bookStore.App.Modules.User.Interface;
using api_bookStore.App.Enums;
using api_bookStore.App.Services.Jwt;
using Xunit;

namespace api_bookstore_test.Modules.Auth.Repository
{
    public class AuthRepositoryTest
    {
        private readonly Mock<IAuthRepository> _authRepository = new();
        private readonly Mock<IUserRepository> _userRepository = new();
        private readonly Mock<IPasswordServiceHash> passwordServiceHash = new();
        private readonly Mock<IJwtTokenService> _jwtTokenService = new();

        [Fact]
        public async Task SigIn_DeveRetornarTokenJwt_ComCredenciaisValidas()
        {
            // Arrange
            var credential = new AuthViewModel("admin@example.com", "string");
            var userIsValid = new UserEntity
            {
                Email = "admin@example.com",
                Password = "string",
                Role = RolesEnum.Admin
            };

            _userRepository.Setup(u => u.UserByEmail(credential.Email)).ReturnsAsync(userIsValid);

            UserEntity userFound = await _userRepository.Object.UserByEmail(credential.Email);

            _jwtTokenService.Setup(t => t.GenerateJwtToken(userFound.Id.ToString(), userFound.Role.ToString())).Returns("tokenJwtValido");

            // Act
            var expectedToken = _jwtTokenService.Object.GenerateJwtToken(userFound.Id.ToString(), userFound.Role.ToString());

            // Assert
            Assert.Equal(credential.Email, userFound.Email);
            Assert.Equal(credential.Password, userFound.Password);
            Assert.NotNull(expectedToken);
            Assert.Contains("tokenJwtValido", expectedToken);
        }
    }
}
