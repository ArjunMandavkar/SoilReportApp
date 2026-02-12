using SoilReportApp.Application.DTOs.Users;

namespace SoilReportApp.Application.Interfaces;

public interface IUserService
{
    /// <summary>
    /// Creates a new user with the provided information.
    /// </summary>
    Task<UserResponse> CreateUserAsync(CreateUserRequest request);
    
    /// <summary>
    /// Gets a user by their unique identifier.
    /// </summary>
    Task<UserResponse?> GetByIdAsync(Guid id);
    
    /// <summary>
    /// Gets a user by their username.
    /// </summary>
    Task<UserResponse?> GetByUsernameAsync(string username);
    
    /// <summary>
    /// Gets a paginated list of all users.
    /// </summary>
    Task<IEnumerable<UserResponse>> GetAllUsersPagedAsync(int page, int pageSize);
    
    /// <summary>
    /// Gets the total count of users.
    /// </summary>
    Task<int> GetTotalCountAsync();
}
