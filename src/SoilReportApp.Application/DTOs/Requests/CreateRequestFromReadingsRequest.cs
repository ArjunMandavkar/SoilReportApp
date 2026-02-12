using System.ComponentModel.DataAnnotations;
using SoilReportApp.Application.DTOs.Readings;

namespace SoilReportApp.Application.DTOs.Requests;

public class CreateRequestFromReadingsRequest
{
    [Required]
    public int DeviceId { get; set; }
    
    [Required]
    [MinLength(1, ErrorMessage = "At least one reading is required")]
    public List<ReadingDto> Readings { get; set; } = new();
}
