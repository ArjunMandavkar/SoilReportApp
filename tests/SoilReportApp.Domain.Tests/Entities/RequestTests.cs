using FluentAssertions;
using SoilReportApp.Domain.Entities;
using SoilReportApp.Domain.Enums;
using Xunit;

namespace SoilReportApp.Domain.Tests.Entities;

public class RequestTests
{
    [Fact]
    public void Request_CanBeCreated_WithDefaultStatus()
    {
        // Arrange & Act
        var request = new Request
        {
            Id = Guid.NewGuid(),
            DeviceId = 1,
            NAvg = 10.5,
            PAvg = 20.3,
            KAvg = 15.7,
            MoistureAvg = 45.0,
            FarmerId = Guid.NewGuid()
        };

        // Assert
        request.Status.Should().Be(RequestStatus.NotStarted);
    }

    [Fact]
    public void Request_UpdateDate_HasDefaultValue()
    {
        // Arrange & Act
        var beforeCreation = DateTime.UtcNow;
        var request = new Request
        {
            Id = Guid.NewGuid(),
            DeviceId = 1,
            FarmerId = Guid.NewGuid()
        };
        var afterCreation = DateTime.UtcNow;

        // Assert
        request.UpdateDate.Should().BeOnOrAfter(beforeCreation);
        request.UpdateDate.Should().BeOnOrBefore(afterCreation);
    }

    [Fact]
    public void Request_OptionalFields_CanBeNull()
    {
        // Arrange & Act
        var request = new Request
        {
            Id = Guid.NewGuid(),
            DeviceId = 1,
            FarmerId = Guid.NewGuid()
        };

        // Assert
        request.ExpertId.Should().BeNull();
        request.SoilTypeId.Should().BeNull();
        request.CropId.Should().BeNull();
        request.CropStageId.Should().BeNull();
        request.Report.Should().BeNull();
    }

    [Fact]
    public void Request_CanHave_AllStatusValues()
    {
        // Arrange
        var request = new Request
        {
            Id = Guid.NewGuid(),
            DeviceId = 1,
            FarmerId = Guid.NewGuid()
        };

        // Act & Assert - NotStarted
        request.Status = RequestStatus.NotStarted;
        request.Status.Should().Be(RequestStatus.NotStarted);

        // Act & Assert - CompletedByFarmer
        request.Status = RequestStatus.CompletedByFarmer;
        request.Status.Should().Be(RequestStatus.CompletedByFarmer);

        // Act & Assert - CompletedByExpert
        request.Status = RequestStatus.CompletedByExpert;
        request.Status.Should().Be(RequestStatus.CompletedByExpert);
    }

    [Fact]
    public void Request_Averages_StorePreciseValues()
    {
        // Arrange & Act
        var request = new Request
        {
            Id = Guid.NewGuid(),
            DeviceId = 1,
            NAvg = 12.345,
            PAvg = 23.456,
            KAvg = 34.567,
            MoistureAvg = 56.789,
            FarmerId = Guid.NewGuid()
        };

        // Assert
        request.NAvg.Should().Be(12.345);
        request.PAvg.Should().Be(23.456);
        request.KAvg.Should().Be(34.567);
        request.MoistureAvg.Should().Be(56.789);
    }
}
