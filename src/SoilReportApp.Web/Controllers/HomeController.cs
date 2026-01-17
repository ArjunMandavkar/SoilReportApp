using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoilReportApp.Web.DataAccess;
using SoilReportApp.Web.Models;
using X.PagedList.Extensions;

namespace SoilReportApp.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }
    
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost("createUser")]
    [Consumes("application/json")]
    public IActionResult CreateUser([FromBody] UserViewModel model)
    {
        User user = new User
        {
            Id = Guid.NewGuid(),
            Username = model.Username,
            Password = model.Password,
            Email = model.Email,
            Phone = model.Phone,
            UserType = model.UserType,
            DeviceId = string.IsNullOrEmpty(model.DeviceId)?0:int.Parse(model.DeviceId),
        };
        
        _context.Users.Add(user);
        _context.SaveChanges();
        return Json(new { success = true });
    }
    
    [HttpPost("SendReading")]
    [Consumes("application/json")]
    public IActionResult SendReading([FromBody] ReadingsViewModel reading)
    {
        if (!ModelState.IsValid)
        {
            return Json(new { success = false, message = "Please fill all fields" });
        }
        
        var requestId = Guid.NewGuid();
        var readings = new List<Reading>();
        foreach (var r in reading.Readings)
        {
            readings.Add(new Reading()
            {
                Id = Guid.NewGuid(),
                Test = r.Test,
                RequestId = requestId,
                N = Convert.ToDouble(r.N.ToString("N2")),
                P = Convert.ToDouble(r.P.ToString("N2")),
                K = Convert.ToDouble(r.K.ToString("N2")),
                Moisture = Convert.ToDouble(r.Moisture.ToString("N2"))
            });
        }

        var request = new Request()
        {
            Id = requestId,
            DeviceId = reading.DeviceId,
            NAvg = readings.Average(r => r.N),
            PAvg = readings.Average(r => r.P),
            KAvg = readings.Average(r => r.K),
            MoistureAvg = readings.Average(r => r.Moisture),
            Status = RequestStatus.NotStarted,
            FarmerId = _context.Users.Where(u => u.DeviceId == reading.DeviceId)
                .Select(u => u.Id).FirstOrDefault()
        };
        
        readings.ForEach(r => _context.Readings.Add(r));
        _context.Requests.Add(request);
        _context.SaveChanges();
        
        return Json(new { success = true });
    }

    [HttpGet("users")]
    public IActionResult Users(int? page)
    {
        int pageSize = 10; // Number of users per page
        int pageNumber = (page ?? 1);
        
        var users = _context.Users.OrderBy(u => u.Username).ToPagedList(pageNumber, pageSize);
        
        return View(users);
    }
    

    
    [HttpGet("guidance")]
    public IActionResult Guidance()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}