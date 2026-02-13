using SoilReportApp.Domain.Entities;

namespace SoilReportApp.Domain.Interfaces.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByUsernameAsync(string username);
    
    Task<User?> GetByDeviceIdAsync(int deviceId);
    
    Task<bool> UsernameExistAsync(string username);
    
    /// <summary>
    /// Gets users with pagination.
    /// </summary>
    Task<IEnumerable<User>> GetAllPagedAsync(int page, int pageSize);
    
    /// <summary>
    /// Gets total count of users.
    /// </summary>
    Task<int> GetTotalCountAsync();
}