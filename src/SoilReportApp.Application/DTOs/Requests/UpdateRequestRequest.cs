using System.ComponentModel.DataAnnotations;

namespace SoilReportApp.Application.DTOs.Requests;

public class UpdateRequestRequest
{
    [Required]
    public Guid Id { get; set; }
    
    public Guid? SoilTypeId { get; set; }
    
    public Guid? CropId { get; set; }
    
    public Guid? CropStageId { get; set; }
    
    public string? Report { get; set; }
}
