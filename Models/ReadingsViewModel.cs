namespace SoilReportApp.Models;

public class ReadingsViewModel
{
    public int DeviceId { get; set; }
    public List<ReadingViewModel> Readings { get; set; }
}