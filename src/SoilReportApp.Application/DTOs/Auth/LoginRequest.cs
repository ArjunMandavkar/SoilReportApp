using System.ComponentModel.DataAnnotations;
using SoilReportApp.Domain.Enums;

namespace SoilReportApp.Application.DTOs.Auth;

public class LoginRequest
{
    [Required(ErrorMessage = "Username is required")]
    public string Username { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; } = string.Empty;
    
    [Required]
    public UserType UserType { get; set; }
}
