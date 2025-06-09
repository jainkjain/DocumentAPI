using Microsoft.Extensions.Configuration;
using Moq;
using UserManagement.API.DTO;
using UserManagement.API.Entities;
using UserManagement.API.Repository;
using UserManagement.API.Services;
using Xunit;

namespace UserManagement.Tests.Services
{
    /// <summary>
    /// Mock of the AuthServiceTests interface. Used for unit testing purposes.
    /// </summary>
    /// 
    public class AuthServiceTests
    {
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<IUserRoleRepository> _mockUserRoleRepository;
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly IAuthService _authService;

        public AuthServiceTests()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _mockUserRoleRepository = new Mock<IUserRoleRepository>();
            _mockConfiguration = new Mock<IConfiguration>();
            _authService = new AuthService(_mockUserRepository.Object, _mockUserRoleRepository.Object, _mockConfiguration.Object);
        }

        [Fact]
        public async Task CreateUserAsync_Success()
        {
            // Arrange
            var dto = new RegisterDto
            {
                Username = "john",
                Password = "pass123",
                Email = "john@example.com"
            };

            _mockUserRepository
                .Setup(repo => repo.CreateUserAsync(It.IsAny<User>()))
                .ReturnsAsync(new User { Id = Guid.NewGuid() });

            _mockUserRoleRepository
                .Setup(repo => repo.AddAsync(It.IsAny<UserRole>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _authService.RegisterAsync(dto);

            // Assert
            Assert.Equal("User registered successfully.", result);
            _mockUserRepository.Verify(repo => repo.CreateUserAsync(It.IsAny<User>()), Times.Once);
            _mockUserRoleRepository.Verify(repo => repo.AddAsync(It.IsAny<UserRole>()), Times.Once);
        }

        [Fact]
        public async Task RegisterAsync_ShouldReturnSuccessMessage_WhenValidData()
        {
            // Arrange
            var dto = new RegisterDto
            {
                Username = "john",
                Password = "pass123",
                Email = "john@example.com"
            };

            _mockUserRepository
                       .Setup(repo => repo.CreateUserAsync(It.IsAny<User>()))
                       .ThrowsAsync(new Exception("DB failure"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _authService.RegisterAsync(dto));
            _mockUserRoleRepository.Verify(repo => repo.AddAsync(It.IsAny<UserRole>()), Times.Never);
        }
    }
}
