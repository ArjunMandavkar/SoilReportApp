using SoilReportApp.Application.DTOs.Users;
using SoilReportApp.Application.Interfaces;
using SoilReportApp.Application.Security;
using SoilReportApp.Domain.Entities;
using SoilReportApp.Domain.Interfaces.Repositories;

namespace SoilReportApp.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserResponse> CreateUserAsync(CreateUserRequest request)
    {
        // Check if username already exists
        if (await _userRepository.UsernameExistAsync(request.Username))
        {
            throw new InvalidOperationException($"Username '{request.Username}' already exists.");
        }

        var user = new User
        {
            Id = Guid.NewGuid(),
            Username = request.Username,
            Email = request.Email,
            Password = PasswordHasher.Hash(request.Password),
            Phone = request.Phone ?? string.Empty,
            UserType = request.UserType,
            DeviceId = request.DeviceId
        };

        await _userRepository.AddAsync(user);

        return MapToResponse(user);
    }

    public async Task<UserResponse?> GetByIdAsync(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        return user is null ? null : MapToResponse(user);
    }

    public async Task<UserResponse?> GetByUsernameAsync(string username)
    {
        var user = await _userRepository.GetByUsernameAsync(username);
        return user is null ? null : MapToResponse(user);
    }

    public async Task<IEnumerable<UserResponse>> GetAllUsersPagedAsync(int page, int pageSize)
    {
        var users = await _userRepository.GetAllPagedAsync(page, pageSize);
        return users.Select(MapToResponse);
    }

    public async Task<int> GetTotalCountAsync()
    {
        return await _userRepository.GetTotalCountAsync();
    }

    // Private mapping method
    private static UserResponse MapToResponse(User user)
    {
        return new UserResponse
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            Phone = user.Phone,
            UserType = user.UserType,
            DeviceId = user.DeviceId
        };
    }
}
