using SoilReportApp.Domain.Entities;

namespace SoilReportApp.Domain.Interfaces.Repositories;

public interface IRequestRepository : IRepository<Request>
{
    Task<IEnumerable<Request>> GetByFarmerIdAsync(Guid farmerId);
    
    Task<IEnumerable<Request>> GetPendingForExpertsAsync(Guid? expertId);

    Task<Request?> GetWithDetailsAsync(Guid id); // Includes Reading, Crop, etc.
    
    /// <summary>
    /// Gets all requests with pagination and includes related entities.
    /// </summary>
    Task<IEnumerable<Request>> GetAllPagedAsync(int page, int pageSize);
    
    /// <summary>
    /// Gets requests by farmer ID with pagination.
    /// </summary>
    Task<IEnumerable<Request>> GetByFarmerIdPagedAsync(Guid farmerId, int page, int pageSize);
    
    /// <summary>
    /// Gets requests for experts (assigned to them or unassigned) with pagination.
    /// </summary>
    Task<IEnumerable<Request>> GetPendingForExpertsPagedAsync(Guid? expertId, int page, int pageSize);
    
    /// <summary>
    /// Gets count of requests for a farmer.
    /// </summary>
    Task<int> GetCountByFarmerIdAsync(Guid farmerId);
    
    /// <summary>
    /// Gets count of requests pending for experts.
    /// </summary>
    Task<int> GetPendingCountForExpertsAsync(Guid? expertId);
    
    /// <summary>
    /// Gets total count of all requests.
    /// </summary>
    Task<int> GetTotalCountAsync();
    
    /// <summary>
    /// Creates a request along with its readings in a single transaction.
    /// </summary>
    Task<Request> CreateWithReadingsAsync(Request request, IEnumerable<Reading> readings);
}