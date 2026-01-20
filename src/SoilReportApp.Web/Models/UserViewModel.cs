using SoilReportApp.Domain.Enums;

namespace SoilReportApp.Web.Models;
public class UserViewModel
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Phone { get; set; }
    public UserType UserType { get; set; }
    public string DeviceId { get; set; }
}