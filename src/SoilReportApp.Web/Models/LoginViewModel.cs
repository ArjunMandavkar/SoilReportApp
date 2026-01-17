using System.ComponentModel.DataAnnotations;

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