using SoilReportApp.Application.DTOs.Auth;

namespace SoilReportApp.Application.Interfaces;

public interface IAuthService
{
    /// <summary>
    /// Validates user credentials and returns authentication result.
    /// </summary>
    Task<AuthResult> ValidateCredentialsAsync(LoginRequest request);
}
