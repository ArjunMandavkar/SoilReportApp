using System.ComponentModel.DataAnnotations;
using SoilReportApp.Domain.Enums;

namespace SoilReportApp.Web.Models;

public class LoginViewModel
{
    [Required]
    public string Username { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    
    [Required]
    public UserType UserType { get; set; }
}