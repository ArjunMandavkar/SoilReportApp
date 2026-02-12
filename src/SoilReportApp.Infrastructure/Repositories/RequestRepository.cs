using Microsoft.EntityFrameworkCore;
using SoilReportApp.Domain.Entities;
using SoilReportApp.Domain.Enums;
using SoilReportApp.Domain.Interfaces.Repositories;
using SoilReportApp.Infrastructure.Data;

namespace SoilReportApp.Infrastructure.Repositories;

public class RequestRepository : Repository<Request>, IRequestRepository
{
    public RequestRepository(ApplicationDbContext context) : base(context) { }

    public async Task<IEnumerable<Request>> GetByFarmerIdAsync(Guid farmerId)
    {
        return await _dbSet
            .Where(r => r.FarmerId == farmerId)
            .Include(r => r.Crop)
            .Include(r => r.SoilType)
            .OrderByDescending(r => r.UpdateDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<Request>> GetPendingForExpertsAsync(Guid? expertId)
    {
        return await _dbSet
            .Where(r => r.Status != RequestStatus.NotStarted && 
                        (r.ExpertId == expertId || r.ExpertId == null))
            .Include(r => r.Crop)
            .Include(r => r.Farmer)
            .OrderByDescending(r => r.UpdateDate)
            .ToListAsync();
    }

    public async Task<Request?> GetWithDetailsAsync(Guid id)
    {
        return await _dbSet
            .Where(r => r.Id == id)
            .Include(r => r.Farmer)
            .Include(r => r.Expert)
            .Include(r => r.Crop)
            .Include(r => r.SoilType)
            .Include(r => r.CropStage)
            .Include(r => r.Readings)
            .FirstOrDefaultAsync();
    }
    
    public async Task<IEnumerable<Request>> GetAllPagedAsync(int page, int pageSize)
    {
        return await _dbSet
            .Include(r => r.Farmer)
            .Include(r => r.Expert)
            .Include(r => r.Crop)
            .Include(r => r.SoilType)
            .Include(r => r.CropStage)
            .OrderByDescending(r => r.UpdateDate)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }
    
    public async Task<IEnumerable<Request>> GetByFarmerIdPagedAsync(Guid farmerId, int page, int pageSize)
    {
        return await _dbSet
            .Where(r => r.FarmerId == farmerId)
            .Include(r => r.Crop)
            .Include(r => r.SoilType)
            .Include(r => r.CropStage)
            .OrderByDescending(r => r.UpdateDate)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }
    
    public async Task<IEnumerable<Request>> GetPendingForExpertsPagedAsync(Guid? expertId, int page, int pageSize)
    {
        return await _dbSet
            .Where(r => r.Status != RequestStatus.NotStarted && 
                        (r.ExpertId == expertId || r.ExpertId == null))
            .Include(r => r.Crop)
            .Include(r => r.Farmer)
            .Include(r => r.SoilType)
            .Include(r => r.CropStage)
            .OrderByDescending(r => r.UpdateDate)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }
    
    public async Task<int> GetCountByFarmerIdAsync(Guid farmerId)
    {
        return await _dbSet.CountAsync(r => r.FarmerId == farmerId);
    }
    
    public async Task<int> GetPendingCountForExpertsAsync(Guid? expertId)
    {
        return await _dbSet.CountAsync(r => 
            r.Status != RequestStatus.NotStarted && 
            (r.ExpertId == expertId || r.ExpertId == null));
    }
    
    public async Task<int> GetTotalCountAsync()
    {
        return await _dbSet.CountAsync();
    }
    
    public async Task<Request> CreateWithReadingsAsync(Request request, IEnumerable<Reading> readings)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            await _context.Readings.AddRangeAsync(readings);
            await _dbSet.AddAsync(request);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            return request;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}