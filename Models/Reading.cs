using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoilReportApp.Models;

public class Reading
{
    [Key]
    public Guid Id { get; set; }
    
    public int Test { get; set; }
    
    [Required]
    public Guid RequestId { get; set; }
    
    [ForeignKey("RequestId")]
    public Request Request { get; set; }
    
    [Required]
    public double N { get; set; }
    
    [Required]
    public double P { get; set; }
    
    [Required]
    public double K { get; set; }
    
    [Required]
    public double Moisture { get; set; }
}