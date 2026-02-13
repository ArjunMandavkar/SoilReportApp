using Microsoft.EntityFrameworkCore;
using SoilReportApp.Domain.Entities;
using SoilReportApp.Domain.Interfaces.Repositories;
using SoilReportApp.Infrastructure.Data;

namespace SoilReportApp.Infrastructure.Repositories;

public class LookupRepository : ILookupRepository
{
    private readonly ApplicationDbContext _context;

    public LookupRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Crop>> GetAllCropsAsync()
    {
        return await _context.Crops
            .OrderBy(c => c.Name)
            .ToListAsync();
    }

    public async Task<IEnumerable<SoilType>> GetAllSoilTypesAsync()
    {
        return await _context.SoilTypes
            .OrderBy(s => s.Name)
            .ToListAsync();
    }

    public async Task<IEnumerable<CropStage>> GetAllCropStagesAsync()
    {
        return await _context.CropStages
            .OrderBy(cs => cs.Name)
            .ToListAsync();
    }
}
