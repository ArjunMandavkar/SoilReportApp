using System.ComponentModel.DataAnnotations;

namespace SoilReportApp.Models;

public class User
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public string Username { get; set; }
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    public string Phone { get; set; }
    public UserType UserType { get; set; }
    public int DeviceId { get; set; }
    
    public ICollection<Request> RequestsAsFarmer { get; set; }
    public ICollection<Request> RequestsAsExpert { get; set; }
}