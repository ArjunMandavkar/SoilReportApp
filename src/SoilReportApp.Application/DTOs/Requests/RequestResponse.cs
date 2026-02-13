using SoilReportApp.Application.DTOs.Readings;
using SoilReportApp.Domain.Enums;

namespace SoilReportApp.Application.DTOs.Requests;

public class RequestResponse
{
    public Guid Id { get; set; }
    
    public int DeviceId { get; set; }
    
    public double NAvg { get; set; }
    
    public double PAvg { get; set; }
    
    public double KAvg { get; set; }
    
    public double MoistureAvg { get; set; }
    
    public RequestStatus Status { get; set; }
    
    public string? Report { get; set; }
    
    public DateTime UpdateDate { get; set; }
    
    // Related entity IDs
    public Guid FarmerId { get; set; }
    
    public Guid? ExpertId { get; set; }
    
    public Guid? SoilTypeId { get; set; }
    
    public Guid? CropId { get; set; }
    
    public Guid? CropStageId { get; set; }
    
    // Related entity names (for display)
    public string? FarmerName { get; set; }
    
    public string? ExpertName { get; set; }
    
    public string? CropName { get; set; }
    
    public string? SoilTypeName { get; set; }
    
    public string? CropStageName { get; set; }
    
    // Readings
    public List<ReadingDto> Readings { get; set; } = new();
}
