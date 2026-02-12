using SoilReportApp.Domain.Enums;

namespace SoilReportApp.Application.DTOs.Requests;

public class RequestListItemResponse
{
    public Guid Id { get; set; }

    public int DeviceId { get; set; }

    public RequestStatus Status { get; set; }

    public DateTime UpdateDate { get; set; }

    public string? FarmerName { get; set; }

    public string? ExpertName { get; set; }

    public string? CropName { get; set; }

    public string? CropStageName { get; set; }

    public string? SoilTypeName { get; set; }

    public double NAvg { get; set; }

    public double PAvg { get; set; }

    public double KAvg { get; set; }

    public double MoistureAvg { get; set; }
}
