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
}