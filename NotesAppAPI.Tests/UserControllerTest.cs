using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using NotesAppAPI.Controllers;
using NotesAppAPI.DataAccess.EF.Models;
using NotesAppAPI.DataAccess.EF.Repositories;
using System;

namespace NotesAppAPI.Tests
{
    public class UserControllerTests
    {
        private readonly Mock<UserRepository> _userRepositoryMock;
        private readonly UserController _controller;

        public UserControllerTests()
        {
            _userRepositoryMock = new Mock<UserRepository>();
            _controller = new UserController(_userRepositoryMock.Object);
        }

        [Fact]
        public void GetUserById_ReturnsOk_WhenUserExists()
        {
            // Arrange
            int userId = 1;
            var user = new User("test@example.com", "password123");
            _userRepositoryMock.Setup(repo => repo.GetUserById(userId)).Returns(user);

            // Act
            var result = _controller.GetuserById(userId);

            // Assert
            var okResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
            Assert.NotNull(okResult.Value);
        }

        [Fact]
        public void GetUserById_ReturnsNotFound_WhenExceptionThrown()
        {
            // Arrange
            int userId = 1;
            _userRepositoryMock.Setup(repo => repo.GetUserById(userId)).Throws(new Exception("User not found"));

            // Act
            var result = _controller.GetuserById(userId);

            // Assert
            var notFoundResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(404, notFoundResult.StatusCode);
            Assert.NotNull(notFoundResult.Value);
        }

        [Fact]
        public void CreateUser_ReturnsOk_WhenUserCreated()
        {
            // Arrange
            string email = "test@example.com";
            string password = "password123";

            // Act
            var result = _controller.CreateUser(email, password);

            // Assert
            var okResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public void CreateUser_ReturnsNotFound_WhenExceptionThrown()
        {
            // Arrange
            string email = "test@example.com";
            string password = "password123";
            _userRepositoryMock.Setup(repo => repo.CreateUser(It.IsAny<User>())).Throws(new Exception("Error creating user"));

            // Act
            var result = _controller.CreateUser(email, password);

            // Assert
            var notFoundResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public void UpdateUser_ReturnsOk_WhenPasswordUpdated()
        {
            // Arrange
            int userId = 1;
            string newPassword = "newpassword123";

            // Act
            var result = _controller.UpdateUser(userId, newPassword);

            // Assert
            var okResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public void UpdateUser_ReturnsUnauthorized_WhenExceptionThrown()
        {
            // Arrange
            int userId = 1;
            string newPassword = "newpassword123";
            _userRepositoryMock.Setup(repo => repo.UpdatePassword(userId, newPassword)).Throws(new Exception("Unauthorized"));

            // Act
            var result = _controller.UpdateUser(userId, newPassword);

            // Assert
            var unauthorizedResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(401, unauthorizedResult.StatusCode);
        }

        [Fact]
        public void DeleteUser_ReturnsOk_WhenUserDeleted()
        {
            // Arrange
            int userId = 1;

            // Act
            var result = _controller.DeleteUser(userId);

            // Assert
            var okResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public void DeleteUser_ReturnsUnauthorized_WhenExceptionThrown()
        {
            // Arrange
            int userId = 1;
            _userRepositoryMock.Setup(repo => repo.DeleteUser(userId)).Throws(new Exception("Unauthorized"));

            // Act
            var result = _controller.DeleteUser(userId);

            // Assert
            var unauthorizedResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(401, unauthorizedResult.StatusCode);
        }
    }
}
