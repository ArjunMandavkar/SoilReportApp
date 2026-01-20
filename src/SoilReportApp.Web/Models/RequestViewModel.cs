using SoilReportApp.Domain.Enums;

namespace SoilReportApp.Web.Models;

public class RequestViewModel
{
    public Guid Id { get; set; }
    
    public int DeviceId { get; set; }
    
    public double NAvg { get; set; }
    
    public double PAvg { get; set; }
    
    public double KAvg { get; set; }
    
    public double MoistureAvg { get; set; }
    
    public Guid? SoilTypeId { get; set; }
    
    public Guid? CropId { get; set; }
    
    public Guid? CropStageId { get; set; }
    
    public string? Report { get; set; }
    
    public RequestStatus Status { get; set; }
    
    public Guid FarmerId { get; set; }
    
    public Guid? ExpertId { get; set; }
}