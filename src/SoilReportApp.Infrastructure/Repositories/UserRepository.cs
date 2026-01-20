using Microsoft.EntityFrameworkCore;
using SoilReportApp.Domain.Entities;
using SoilReportApp.Domain.Interfaces.Repositories;
using SoilReportApp.Infrastructure.Data;

namespace SoilReportApp.Infrastructure.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context) { }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await _dbSet.FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task<User?> GetByDeviceIdAsync(int deviceId)
    {
        return await _dbSet.FirstOrDefaultAsync(u => u.DeviceId == deviceId);
    }

    public async Task<bool> UsernameExistAsync(string username)
    {
        return await _dbSet.AnyAsync(u => u.Username == username);
    }
}