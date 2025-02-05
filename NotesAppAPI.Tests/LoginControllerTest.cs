using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NotesAppAPI.Controllers;
using NotesAppAPI.DataAccess.EF.Models;
using NotesAppAPI.DataAccess.EF.Repositories;
using NotesAppAPI.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NotesAppAPI.Tests.Controllers
{
    public class LoginControllerTests
    {
        private readonly Mock<UserRepository> _mockUserRepo;
        private readonly Mock<IConfiguration> _mockConfig;
        private readonly LoginController _loginController;

        public LoginControllerTests()
        {
            _mockUserRepo = new Mock<UserRepository>();
            _mockConfig = new Mock<IConfiguration>();
            _loginController = new LoginController(_mockUserRepo.Object, _mockConfig.Object);
        }

        [Fact]
        public void Login_ValidCredentials_ReturnsAccessToken()
        {
            // Arrange
            var testUser = new User { UserId = 1, UserEmail = "test@example.com", UserPassword = "hashedPassword" };
            var loginRequest = new LoginRequest { UserEmail = "test@example.com", UserPassword = "password" };

            _mockUserRepo.Setup(repo => repo.GetUserByEmail("test@example.com")).Returns(testUser);

            // Act
            var result = _loginController.Login(loginRequest) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void Login_InvalidCredentials_ReturnsUnauthorized()
        {
            // Arrange
            var loginRequest = new LoginRequest { UserEmail = "wrong@example.com", UserPassword = "password" };
            _mockUserRepo.Setup(repo => repo.GetUserByEmail("wrong@example.com")).Returns((User)null);

            // Act
            var result = _loginController.Login(loginRequest) as UnauthorizedObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(401, result.StatusCode);
        }
    }
}
