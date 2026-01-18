using SoilReportApp.Domain.Entities;

namespace SoilReportApp.Domain.Interfaces.Repositories;

public interface IRequestRepository : IRepository<Request>
{
    Task<IEnumerable<Request>> GetByFarmerIdAsync(Guid farmerId);
    
    Task<IEnumerable<Request>> GetPendingForExpertsAsync(Guid? expertId);

    Task<Request?> GetWithDetailsAsync(Guid id); // Includes Reading, Crop, etc.
}