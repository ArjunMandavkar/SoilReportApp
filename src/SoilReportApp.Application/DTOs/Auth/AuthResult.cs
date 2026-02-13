using SoilReportApp.Application.DTOs.Users;

namespace SoilReportApp.Application.DTOs.Auth;

public class AuthResult
{
    public bool Success { get; set; }
    
    public UserResponse? User { get; set; }
    
    public string? ErrorMessage { get; set; }
    
    public static AuthResult Successful(UserResponse user) => new()
    {
        Success = true,
        User = user
    };
    
    public static AuthResult Failed(string errorMessage) => new()
    {
        Success = false,
        ErrorMessage = errorMessage
    };
}
