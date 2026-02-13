using SoilReportApp.Application.DTOs.Lookups;
using SoilReportApp.Application.DTOs.Requests;
using SoilReportApp.Domain.Enums;

namespace SoilReportApp.Application.Interfaces;

public interface IRequestService
{
    /// <summary>
    /// Creates a new request from soil readings submitted by an IoT device.
    /// </summary>
    Task<RequestResponse> CreateFromReadingsAsync(CreateRequestFromReadingsRequest request);
    
    /// <summary>
    /// Gets a request by ID with all related details (readings, crop, soil type, etc.).
    /// </summary>
    Task<RequestResponse?> GetByIdWithDetailsAsync(Guid id);
    
    /// <summary>
    /// Gets paginated requests filtered by user role.
    /// - Farmer: sees only their own requests
    /// - Expert: sees requests assigned to them or unassigned (excluding NotStarted)
    /// - Admin: sees all requests
    /// </summary>
    Task<IEnumerable<RequestListItemResponse>> GetRequestsForUserAsync(
        Guid userId, UserType userType, bool isAdmin, int page, int pageSize);
    
    /// <summary>
    /// Gets all requests with pagination (for admin use).
    /// </summary>
    Task<IEnumerable<RequestListItemResponse>> GetAllRequestsPagedAsync(int page, int pageSize);
    
    /// <summary>
    /// Updates a request with crop/soil information and report.
    /// Automatically sets status based on user type.
    /// </summary>
    Task<RequestResponse> UpdateRequestAsync(UpdateRequestRequest request, Guid userId, UserType userType);
    
    /// <summary>
    /// Gets the total count of requests for a user based on their role.
    /// </summary>
    Task<int> GetRequestCountForUserAsync(Guid userId, UserType userType, bool isAdmin);
    
    // Lookup methods for dropdowns
    
    /// <summary>
    /// Gets all available crops.
    /// </summary>
    Task<IEnumerable<CropDto>> GetAllCropsAsync();
    
    /// <summary>
    /// Gets all available soil types.
    /// </summary>
    Task<IEnumerable<SoilTypeDto>> GetAllSoilTypesAsync();
    
    /// <summary>
    /// Gets all available crop stages.
    /// </summary>
    Task<IEnumerable<CropStageDto>> GetAllCropStagesAsync();
}
