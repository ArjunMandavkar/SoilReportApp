using System.ComponentModel.DataAnnotations;

namespace SoilReportApp.Application.DTOs.Readings;

public class ReadingDto
{
    [Required]
    public int Test { get; set; }
    
    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "N value must be non-negative")]
    public double N { get; set; }
    
    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "P value must be non-negative")]
    public double P { get; set; }
    
    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "K value must be non-negative")]
    public double K { get; set; }
    
    [Required]
    [Range(0, 100, ErrorMessage = "Moisture must be between 0 and 100")]
    public double Moisture { get; set; }
}
