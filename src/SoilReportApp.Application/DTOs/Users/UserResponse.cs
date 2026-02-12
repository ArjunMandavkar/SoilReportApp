using SoilReportApp.Domain.Enums;

namespace SoilReportApp.Application.DTOs.Users;

public class UserResponse
{
    public Guid Id { get; set; }
    
    public string Username { get; set; } = string.Empty;
    
    public string Email { get; set; } = string.Empty;
    
    public string? Phone { get; set; }
    
    public UserType UserType { get; set; }
    
    public int DeviceId { get; set; }
}
