using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NotesAppAPI.Controllers;
using NotesAppAPI.DataAccess.EF.Repositories;
using NotesAppAPI.DataAccess.EF.Models;
using NotesAppAPI.DTO;
using System.Collections.Generic;

public class AuthControllerTests
{
    private readonly Mock<UserRepository> _mockUserRepository;
    private readonly Mock<IConfiguration> _mockConfiguration;
    private readonly AuthController _authController;

    public AuthControllerTests()
    {
        _mockUserRepository = new Mock<UserRepository>(null);
        _mockConfiguration = new Mock<IConfiguration>();
        _authController = new AuthController(_mockUserRepository.Object, _mockConfiguration.Object);
    }

    [Fact]
    public void RefreshToken_ShouldReturnBadRequest_WhenRequestIsNull()
    {
        var result = _authController.RefreshToken(null);
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public void RefreshToken_ShouldReturnUnauthorized_WhenTokenIsInvalid()
    {
        var request = new RefreshTokenRequest { RefreshToken = "invalid_token" };
        _mockUserRepository.Setup(repo => repo.GetUserByRefreshToken(It.IsAny<string>())).Returns((User)null);

        var result = _authController.RefreshToken(request);
        Assert.IsType<UnauthorizedObjectResult>(result);
    }

    [Fact]
    public void RefreshToken_ShouldReturnOk_WhenTokenIsValid()
    {
        var request = new RefreshTokenRequest { RefreshToken = "valid_token" };
        var user = new User { UserId = 1, UserEmail = "test@example.com" };
        _mockUserRepository.Setup(repo => repo.GetUserByRefreshToken(request.RefreshToken)).Returns(user);

        var result = _authController.RefreshToken(request) as OkObjectResult;

        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
    }
}
