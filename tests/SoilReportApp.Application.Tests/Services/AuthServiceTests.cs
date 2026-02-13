using FluentAssertions;
using Moq;
using SoilReportApp.Application.DTOs.Auth;
using SoilReportApp.Application.Security;
using SoilReportApp.Application.Services;
using SoilReportApp.Domain.Entities;
using SoilReportApp.Domain.Enums;
using SoilReportApp.Domain.Interfaces.Repositories;
using Xunit;

namespace SoilReportApp.Application.Tests.Services;

public class AuthServiceTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly AuthService _sut;

    public AuthServiceTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _sut = new AuthService(_userRepositoryMock.Object);
    }

    [Fact]
    public async Task ValidateCredentialsAsync_WithValidCredentials_ReturnsSuccessResult()
    {
        // Arrange
        var request = new LoginRequest
        {
            Username = "testuser",
            Password = "correctpassword",
            UserType = UserType.Farmer
        };

        var user = new User
        {
            Id = Guid.NewGuid(),
            Username = "testuser",
            Password = PasswordHasher.Hash("correctpassword"),
            Email = "test@example.com",
            UserType = UserType.Farmer,
            DeviceId = 1
        };

        _userRepositoryMock
            .Setup(x => x.GetByUsernameAsync(request.Username))
            .ReturnsAsync(user);

        // Act
        var result = await _sut.ValidateCredentialsAsync(request);

        // Assert
        result.Success.Should().BeTrue();
        result.User.Should().NotBeNull();
        result.User!.Username.Should().Be(user.Username);
        result.ErrorMessage.Should().BeNull();
    }

    [Fact]
    public async Task ValidateCredentialsAsync_WithInvalidUsername_ReturnsFailedResult()
    {
        // Arrange
        var request = new LoginRequest
        {
            Username = "nonexistent",
            Password = "password",
            UserType = UserType.Farmer
        };

        _userRepositoryMock
            .Setup(x => x.GetByUsernameAsync(request.Username))
            .ReturnsAsync((User?)null);

        // Act
        var result = await _sut.ValidateCredentialsAsync(request);

        // Assert
        result.Success.Should().BeFalse();
        result.User.Should().BeNull();
        result.ErrorMessage.Should().Be("Invalid username or password.");
    }

    [Fact]
    public async Task ValidateCredentialsAsync_WithWrongPassword_ReturnsFailedResult()
    {
        // Arrange
        var request = new LoginRequest
        {
            Username = "testuser",
            Password = "wrongpassword",
            UserType = UserType.Farmer
        };

        var user = new User
        {
            Id = Guid.NewGuid(),
            Username = "testuser",
            Password = PasswordHasher.Hash("correctpassword"),
            UserType = UserType.Farmer
        };

        _userRepositoryMock
            .Setup(x => x.GetByUsernameAsync(request.Username))
            .ReturnsAsync(user);

        // Act
        var result = await _sut.ValidateCredentialsAsync(request);

        // Assert
        result.Success.Should().BeFalse();
        result.ErrorMessage.Should().Be("Invalid username or password.");
    }

    [Fact]
    public async Task ValidateCredentialsAsync_WithWrongUserType_ReturnsFailedResult()
    {
        // Arrange
        var request = new LoginRequest
        {
            Username = "testuser",
            Password = "correctpassword",
            UserType = UserType.Expert // Wrong type
        };

        var user = new User
        {
            Id = Guid.NewGuid(),
            Username = "testuser",
            Password = "correctpassword",
            UserType = UserType.Farmer // Actual type
        };

        _userRepositoryMock
            .Setup(x => x.GetByUsernameAsync(request.Username))
            .ReturnsAsync(user);

        // Act
        var result = await _sut.ValidateCredentialsAsync(request);

        // Assert
        result.Success.Should().BeFalse();
        result.ErrorMessage.Should().Be("Invalid username or password.");
    }
}
