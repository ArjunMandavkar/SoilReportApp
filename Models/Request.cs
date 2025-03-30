using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoilReportApp.Models;

public class Request
{
    [Key]
    public Guid Id { get; set; }
    
    public int DeviceId { get; set; }
    
    public double NAvg { get; set; }
    
    public double PAvg { get; set; }
    
    public double KAvg { get; set; }
    
    public double MoistureAvg { get; set; }
    
    public virtual ICollection<Reading> Readings { get; set; }
    
    public Guid? SoilTypeId { get; set; }
    
    [ForeignKey("SoilTypeId")]
    public SoilType? SoilType { get; set; }
    
    public Guid? CropId { get; set; }
    
    [ForeignKey("CropId")]
    public Crop? Crop { get; set; }
    
    public Guid? CropStageId { get; set; }
    
    [ForeignKey("CropStageId")]
    public CropStage? CropStage { get; set; }
    
    public string? Report { get; set; }
    
    public RequestStatus Status { get; set; }
    
    public Guid FarmerId { get; set; }
    public Guid? ExpertId { get; set; }
    
    [ForeignKey("FarmerId")]
    public User? Farmer { get; set; }
    [ForeignKey("ExpertId")]
    public User? Expert { get; set; }
}