using SoilReportApp.Domain.Entities;

namespace SoilReportApp.Domain.Interfaces.Repositories;

public interface ILookupRepository
{
    Task<IEnumerable<Crop>> GetAllCropsAsync();
    
    Task<IEnumerable<SoilType>> GetAllSoilTypesAsync();
    
    Task<IEnumerable<CropStage>> GetAllCropStagesAsync();
}
