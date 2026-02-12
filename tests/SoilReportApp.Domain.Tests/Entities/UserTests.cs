using FluentAssertions;
using SoilReportApp.Domain.Entities;
using SoilReportApp.Domain.Enums;
using Xunit;

namespace SoilReportApp.Domain.Tests.Entities;

public class UserTests
{
    [Fact]
    public void User_CanBeCreated_WithValidProperties()
    {
        // Arrange & Act
        var user = new User
        {
            Id = Guid.NewGuid(),
            Username = "testuser",
            Email = "test@example.com",
            Password = "hashedpassword",
            Phone = "1234567890",
            UserType = UserType.Farmer,
            DeviceId = 1
        };

        // Assert
        user.Username.Should().Be("testuser");
        user.Email.Should().Be("test@example.com");
        user.UserType.Should().Be(UserType.Farmer);
        user.DeviceId.Should().Be(1);
    }

    [Fact]
    public void User_RequestsAsFarmer_InitializesAsNull()
    {
        // Arrange & Act
        var user = new User
        {
            Id = Guid.NewGuid(),
            Username = "farmer",
            Password = "password"
        };

        // Assert
        user.RequestsAsFarmer.Should().BeNull();
        user.RequestsAsExpert.Should().BeNull();
    }

    [Theory]
    [InlineData(UserType.Farmer)]
    [InlineData(UserType.Expert)]
    public void User_CanHave_DifferentUserTypes(UserType userType)
    {
        // Arrange & Act
        var user = new User
        {
            Id = Guid.NewGuid(),
            Username = "user",
            Password = "password",
            UserType = userType
        };

        // Assert
        user.UserType.Should().Be(userType);
    }
}
