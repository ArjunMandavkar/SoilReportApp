using SoilReportApp.Application.DTOs.Auth;
using SoilReportApp.Application.DTOs.Users;
using SoilReportApp.Application.Interfaces;
using SoilReportApp.Application.Security;
using SoilReportApp.Domain.Interfaces.Repositories;

namespace SoilReportApp.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;

    public AuthService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<AuthResult> ValidateCredentialsAsync(LoginRequest request)
    {
        var user = await _userRepository.GetByUsernameAsync(request.Username);

        if (user is null)
        {
            return AuthResult.Failed("Invalid username or password.");
        }

        // Check if user type matches
        if (user.UserType != request.UserType)
        {
            return AuthResult.Failed("Invalid username or password.");
        }

        // Validate password using secure hashing
        if (!PasswordHasher.Verify(user.Password, request.Password))
        {
            return AuthResult.Failed("Invalid username or password.");
        }

        var userResponse = new UserResponse
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            Phone = user.Phone,
            UserType = user.UserType,
            DeviceId = user.DeviceId
        };

        return AuthResult.Successful(userResponse);
    }
}
