using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoilReportApp.DataAccess;
using SoilReportApp.Models;
using X.PagedList.Extensions;

namespace SoilReportApp.Controllers;

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
    
    [HttpPost]
    public IActionResult Login([FromBody] LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            List<User> users = _context.Users.ToList();
            foreach (User user in users)
            {
                if (model.Username == user.Username && model.Password == user.Password && model.UserType == user.UserType)
                {
                    string secureString = EncryptionHelper.Encrypt(model.Username);
                    return Json(new { success = true, secureString = secureString });
                }
            }
            return Json(new { success = false, message = "Invalid username or password" });
        }
        return Json(new { success = false, message = "Please fill all fields" });
    }

    [HttpPost]
    [Consumes("application/json")]
    public IActionResult CreateUser([FromBody] User user)
    {
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
                N = r.N,
                P = r.P,
                K = r.K,
                Moisture = r.Moisture
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
    
    [HttpGet("requests")]
    public IActionResult Requests(int? page)
    {
        int pageSize = 10; 
        int pageNumber = (page ?? 1);

        var requests = _context.Requests
            .Include(r => r.Farmer)
            .Include(r => r.Crop)
            .Include(r => r.SoilType)
            .Include(r => r.CropStage)
            .ToPagedList(pageNumber, pageSize);
        return View(requests);
    }
    
    [HttpGet("reports")]
    public IActionResult Reports()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}