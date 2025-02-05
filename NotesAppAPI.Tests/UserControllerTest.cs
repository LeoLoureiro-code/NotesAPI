using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using NotesAppAPI.Controllers;
using NotesAppAPI.DataAccess.EF.Repositories;
using NotesAppAPI.DataAccess.EF.Models;
using Microsoft.AspNetCore.Http;
using System;

namespace NotesAppAPI.Tests
{
    public class UserControllerTests
    {
        private readonly Mock<UserRepository> _userRepositoryMock;
        private readonly UserController _userController;

        public UserControllerTests()
        {
            _userRepositoryMock = new Mock<UserRepository>();
            _userController = new UserController(_userRepositoryMock.Object);
        }

        [Fact]
        public void GetUserByEmail_ReturnsOk_WhenUserExists()
        {
            // Arrange
            string email = "test@example.com";
            var user = new User { UserEmail = email };
            _userRepositoryMock.Setup(repo => repo.GetUserByEmail(email)).Returns(user);

            // Act
            var result = _userController.GetuserById(email) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
        }

        [Fact]
        public void GetUserByEmail_ReturnsNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            string email = "nonexistent@example.com";
            _userRepositoryMock.Setup(repo => repo.GetUserByEmail(email)).Throws(new Exception("User not found"));

            // Act
            var result = _userController.GetuserById(email) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
        }

        [Fact]
        public void CreateUser_ReturnsOk_WhenUserIsCreated()
        {
            // Arrange
            string email = "newuser@example.com";
            string password = "password123";
            _userRepositoryMock.Setup(repo => repo.CreateUser(It.IsAny<User>(), password)).Returns(1);

            // Act
            var result = _userController.CreateUser(email, password) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
        }

        [Fact]
        public void CreateUser_ReturnsNotFound_OnException()
        {
            // Arrange
            string email = "newuser@example.com";
            string password = "password123";
            _userRepositoryMock.Setup(repo => repo.CreateUser(It.IsAny<User>(), password)).Throws(new Exception("Error"));

            // Act
            var result = _userController.CreateUser(email, password) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
        }

        [Fact]
        public void UpdateUser_ReturnsOk_WhenUserIsUpdated()
        {
            // Arrange
            int userId = 1;
            string newPassword = "newPassword";
            _userRepositoryMock.Setup(repo => repo.UpdatePassword(userId, newPassword)).Returns(userId);

            // Act
            var result = _userController.UpdateUser(userId, newPassword) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
        }

        [Fact]
        public void UpdateUser_ReturnsUnauthorized_OnException()
        {
            // Arrange
            int userId = 1;
            string newPassword = "newPassword";
            _userRepositoryMock.Setup(repo => repo.UpdatePassword(userId, newPassword)).Throws(new Exception("Unauthorized"));

            // Act
            var result = _userController.UpdateUser(userId, newPassword) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status401Unauthorized, result.StatusCode);
        }

        [Fact]
        public void DeleteUser_ReturnsOk_WhenUserIsDeleted()
        {
            // Arrange
            int userId = 1;
            _userRepositoryMock.Setup(repo => repo.DeleteUser(userId)).Returns(true);

            // Act
            var result = _userController.DeleteUser(userId) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
        }

        [Fact]
        public void DeleteUser_ReturnsUnauthorized_OnException()
        {
            // Arrange
            int userId = 1;
            _userRepositoryMock.Setup(repo => repo.DeleteUser(userId)).Throws(new Exception("Unauthorized"));

            // Act
            var result = _userController.DeleteUser(userId) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status401Unauthorized, result.StatusCode);
        }
    }
}
