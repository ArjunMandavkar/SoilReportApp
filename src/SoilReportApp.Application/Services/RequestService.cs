using SoilReportApp.Application.DTOs.Lookups;
using SoilReportApp.Application.DTOs.Readings;
using SoilReportApp.Application.DTOs.Requests;
using SoilReportApp.Application.Interfaces;
using SoilReportApp.Domain.Entities;
using SoilReportApp.Domain.Enums;
using SoilReportApp.Domain.Interfaces.Repositories;

namespace SoilReportApp.Application.Services;

public class RequestService : IRequestService
{
    private readonly IRequestRepository _requestRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILookupRepository _lookupRepository;

    public RequestService(
        IRequestRepository requestRepository,
        IUserRepository userRepository,
        ILookupRepository lookupRepository)
    {
        _requestRepository = requestRepository;
        _userRepository = userRepository;
        _lookupRepository = lookupRepository;
    }

    public async Task<RequestResponse> CreateFromReadingsAsync(CreateRequestFromReadingsRequest request)
    {
        // Find farmer by device ID
        var farmer = await _userRepository.GetByDeviceIdAsync(request.DeviceId);
        if (farmer is null)
        {
            throw new InvalidOperationException($"No farmer registered with device ID {request.DeviceId}.");
        }

        var requestId = Guid.NewGuid();

        // Create readings
        var readings = request.Readings.Select(r => new Reading
        {
            Id = Guid.NewGuid(),
            Test = r.Test,
            RequestId = requestId,
            N = Math.Round(r.N, 2),
            P = Math.Round(r.P, 2),
            K = Math.Round(r.K, 2),
            Moisture = Math.Round(r.Moisture, 2)
        }).ToList();

        // Create request with calculated averages
        var soilRequest = new Request
        {
            Id = requestId,
            DeviceId = request.DeviceId,
            NAvg = readings.Average(r => r.N),
            PAvg = readings.Average(r => r.P),
            KAvg = readings.Average(r => r.K),
            MoistureAvg = readings.Average(r => r.Moisture),
            Status = RequestStatus.NotStarted,
            FarmerId = farmer.Id,
            UpdateDate = DateTime.UtcNow
        };

        // Save request and readings
        await _requestRepository.CreateWithReadingsAsync(soilRequest, readings);

        // Return response
        return MapToResponse(soilRequest, readings, farmer.Username);
    }

    public async Task<RequestResponse?> GetByIdWithDetailsAsync(Guid id)
    {
        var request = await _requestRepository.GetWithDetailsAsync(id);
        if (request is null) return null;

        return MapToFullResponse(request);
    }

    public async Task<IEnumerable<RequestListItemResponse>> GetRequestsForUserAsync(
        Guid userId, UserType userType, bool isAdmin, int page, int pageSize)
    {
        IEnumerable<Request> requests;

        if (isAdmin)
        {
            requests = await _requestRepository.GetAllPagedAsync(page, pageSize);
        }
        else if (userType == UserType.Farmer)
        {
            requests = await _requestRepository.GetByFarmerIdPagedAsync(userId, page, pageSize);
        }
        else // Expert
        {
            requests = await _requestRepository.GetPendingForExpertsPagedAsync(userId, page, pageSize);
        }

        return requests.Select(MapToListItemResponse);
    }

    public async Task<IEnumerable<RequestListItemResponse>> GetAllRequestsPagedAsync(int page, int pageSize)
    {
        var requests = await _requestRepository.GetAllPagedAsync(page, pageSize);
        return requests.Select(MapToListItemResponse);
    }

    public async Task<RequestResponse> UpdateRequestAsync(UpdateRequestRequest request, Guid userId, UserType userType)
    {
        var existingRequest = await _requestRepository.GetWithDetailsAsync(request.Id);
        if (existingRequest is null)
        {
            throw new InvalidOperationException($"Request with ID {request.Id} not found.");
        }

        // Update fields
        existingRequest.SoilTypeId = request.SoilTypeId;
        existingRequest.CropId = request.CropId;
        existingRequest.CropStageId = request.CropStageId;
        existingRequest.Report = request.Report;
        existingRequest.UpdateDate = DateTime.UtcNow;

        // Set status based on user type
        if (userType == UserType.Expert)
        {
            existingRequest.Status = RequestStatus.CompletedByExpert;
            existingRequest.ExpertId = userId;
        }
        else if (userType == UserType.Farmer)
        {
            existingRequest.Status = RequestStatus.CompletedByFarmer;
        }

        await _requestRepository.UpdateAsync(existingRequest);

        return MapToFullResponse(existingRequest);
    }

    public async Task<int> GetRequestCountForUserAsync(Guid userId, UserType userType, bool isAdmin)
    {
        if (isAdmin)
        {
            return await _requestRepository.GetTotalCountAsync();
        }
        else if (userType == UserType.Farmer)
        {
            return await _requestRepository.GetCountByFarmerIdAsync(userId);
        }
        else // Expert
        {
            return await _requestRepository.GetPendingCountForExpertsAsync(userId);
        }
    }

    public async Task<IEnumerable<CropDto>> GetAllCropsAsync()
    {
        var crops = await _lookupRepository.GetAllCropsAsync();
        return crops.Select(c => new CropDto { Id = c.Id, Name = c.Name });
    }

    public async Task<IEnumerable<SoilTypeDto>> GetAllSoilTypesAsync()
    {
        var soilTypes = await _lookupRepository.GetAllSoilTypesAsync();
        return soilTypes.Select(s => new SoilTypeDto { Id = s.Id, Name = s.Name });
    }

    public async Task<IEnumerable<CropStageDto>> GetAllCropStagesAsync()
    {
        var cropStages = await _lookupRepository.GetAllCropStagesAsync();
        return cropStages.Select(cs => new CropStageDto { Id = cs.Id, Name = cs.Name });
    }

    // Private mapping methods
    private static RequestResponse MapToResponse(Request request, IEnumerable<Reading> readings, string? farmerName)
    {
        return new RequestResponse
        {
            Id = request.Id,
            DeviceId = request.DeviceId,
            NAvg = request.NAvg,
            PAvg = request.PAvg,
            KAvg = request.KAvg,
            MoistureAvg = request.MoistureAvg,
            Status = request.Status,
            Report = request.Report,
            UpdateDate = request.UpdateDate,
            FarmerId = request.FarmerId,
            ExpertId = request.ExpertId,
            SoilTypeId = request.SoilTypeId,
            CropId = request.CropId,
            CropStageId = request.CropStageId,
            FarmerName = farmerName,
            Readings = readings.Select(r => new ReadingDto
            {
                Test = r.Test,
                N = r.N,
                P = r.P,
                K = r.K,
                Moisture = r.Moisture
            }).ToList()
        };
    }

    private static RequestResponse MapToFullResponse(Request request)
    {
        return new RequestResponse
        {
            Id = request.Id,
            DeviceId = request.DeviceId,
            NAvg = request.NAvg,
            PAvg = request.PAvg,
            KAvg = request.KAvg,
            MoistureAvg = request.MoistureAvg,
            Status = request.Status,
            Report = request.Report,
            UpdateDate = request.UpdateDate,
            FarmerId = request.FarmerId,
            ExpertId = request.ExpertId,
            SoilTypeId = request.SoilTypeId,
            CropId = request.CropId,
            CropStageId = request.CropStageId,
            FarmerName = request.Farmer?.Username,
            ExpertName = request.Expert?.Username,
            CropName = request.Crop?.Name,
            SoilTypeName = request.SoilType?.Name,
            CropStageName = request.CropStage?.Name,
            Readings = request.Readings?.Select(r => new ReadingDto
            {
                Test = r.Test,
                N = r.N,
                P = r.P,
                K = r.K,
                Moisture = r.Moisture
            }).ToList() ?? new List<ReadingDto>()
        };
    }

    private static RequestListItemResponse MapToListItemResponse(Request request)
    {
        return new RequestListItemResponse
        {
            Id = request.Id,
            DeviceId = request.DeviceId,
            Status = request.Status,
            UpdateDate = request.UpdateDate,
            FarmerName = request.Farmer?.Username,
            ExpertName = request.Expert?.Username,
            CropName = request.Crop?.Name,
            SoilTypeName = request.SoilType?.Name,
            NAvg = request.NAvg,
            PAvg = request.PAvg,
            KAvg = request.KAvg,
            MoistureAvg = request.MoistureAvg
        };
    }
}
