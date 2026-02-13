using System.ComponentModel.DataAnnotations;
using SoilReportApp.Domain.Enums;

namespace SoilReportApp.Application.DTOs.Users;

public class CreateUserRequest
{
    [Required(ErrorMessage = "Username is required")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters")]
    public string Username { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Password is required")]
    [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters")]
    public string Password { get; set; } = string.Empty;
    
    public string? Phone { get; set; }
    
    [Required]
    public UserType UserType { get; set; }
    
    public int DeviceId { get; set; }
}
