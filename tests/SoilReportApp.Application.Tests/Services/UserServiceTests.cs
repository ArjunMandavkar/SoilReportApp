using FluentAssertions;
using Moq;
using SoilReportApp.Application.DTOs.Users;
using SoilReportApp.Application.Services;
using SoilReportApp.Domain.Entities;
using SoilReportApp.Domain.Enums;
using SoilReportApp.Domain.Interfaces.Repositories;
using Xunit;

namespace SoilReportApp.Application.Tests.Services;

public class UserServiceTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly UserService _sut; // System Under Test

    public UserServiceTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _sut = new UserService(_userRepositoryMock.Object);
    }

    [Fact]
    public async Task CreateUserAsync_WithValidRequest_ReturnsUserResponse()
    {
        // Arrange
        var request = new CreateUserRequest
        {
            Username = "testuser",
            Email = "test@example.com",
            Password = "password123",
            Phone = "1234567890",
            UserType = UserType.Farmer,
            DeviceId = 1
        };

        _userRepositoryMock
            .Setup(x => x.UsernameExistAsync(request.Username))
            .ReturnsAsync(false);

        _userRepositoryMock
            .Setup(x => x.AddAsync(It.IsAny<User>()))
            .ReturnsAsync((User user) => user);

        // Act
        var result = await _sut.CreateUserAsync(request);

        // Assert
        result.Should().NotBeNull();
        result.Username.Should().Be(request.Username);
        result.Email.Should().Be(request.Email);
        result.UserType.Should().Be(request.UserType);
        result.DeviceId.Should().Be(request.DeviceId);

        _userRepositoryMock.Verify(x => x.AddAsync(It.IsAny<User>()), Times.Once);
    }

    [Fact]
    public async Task CreateUserAsync_WithExistingUsername_ThrowsInvalidOperationException()
    {
        // Arrange
        var request = new CreateUserRequest
        {
            Username = "existinguser",
            Email = "test@example.com",
            Password = "password123",
            UserType = UserType.Farmer
        };

        _userRepositoryMock
            .Setup(x => x.UsernameExistAsync(request.Username))
            .ReturnsAsync(true);

        // Act
        var act = async () => await _sut.CreateUserAsync(request);

        // Assert
        await act.Should()
            .ThrowAsync<InvalidOperationException>()
            .WithMessage($"Username '{request.Username}' already exists.");

        _userRepositoryMock.Verify(x => x.AddAsync(It.IsAny<User>()), Times.Never);
    }

    [Fact]
    public async Task GetByIdAsync_WithExistingUser_ReturnsUserResponse()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var user = new User
        {
            Id = userId,
            Username = "testuser",
            Email = "test@example.com",
            Password = "hashedpassword",
            Phone = "1234567890",
            UserType = UserType.Expert,
            DeviceId = 0
        };

        _userRepositoryMock
            .Setup(x => x.GetByIdAsync(userId))
            .ReturnsAsync(user);

        // Act
        var result = await _sut.GetByIdAsync(userId);

        // Assert
        result.Should().NotBeNull();
        result!.Id.Should().Be(userId);
        result.Username.Should().Be(user.Username);
        result.Email.Should().Be(user.Email);
    }

    [Fact]
    public async Task GetByIdAsync_WithNonExistingUser_ReturnsNull()
    {
        // Arrange
        var userId = Guid.NewGuid();

        _userRepositoryMock
            .Setup(x => x.GetByIdAsync(userId))
            .ReturnsAsync((User?)null);

        // Act
        var result = await _sut.GetByIdAsync(userId);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetAllUsersPagedAsync_ReturnsPagedUsers()
    {
        // Arrange
        var users = new List<User>
        {
            new() { Id = Guid.NewGuid(), Username = "user1", Email = "user1@test.com", Password = "pass", UserType = UserType.Farmer },
            new() { Id = Guid.NewGuid(), Username = "user2", Email = "user2@test.com", Password = "pass", UserType = UserType.Expert }
        };

        _userRepositoryMock
            .Setup(x => x.GetAllPagedAsync(1, 10))
            .ReturnsAsync(users);

        // Act
        var result = await _sut.GetAllUsersPagedAsync(1, 10);

        // Assert
        result.Should().HaveCount(2);
        result.First().Username.Should().Be("user1");
    }

    [Fact]
    public async Task GetTotalCountAsync_ReturnsTotalCount()
    {
        // Arrange
        _userRepositoryMock
            .Setup(x => x.GetTotalCountAsync())
            .ReturnsAsync(42);

        // Act
        var result = await _sut.GetTotalCountAsync();

        // Assert
        result.Should().Be(42);
    }
}
