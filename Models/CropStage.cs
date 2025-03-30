using System.ComponentModel.DataAnnotations;

namespace SoilReportApp.Models;

public class CropStage
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    public virtual ICollection<Request> Requests { get; set; }
}