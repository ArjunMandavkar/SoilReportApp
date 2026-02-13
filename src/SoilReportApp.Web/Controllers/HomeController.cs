using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SoilReportApp.Application.DTOs.Readings;
using SoilReportApp.Application.DTOs.Requests;
using SoilReportApp.Application.DTOs.Users;
using SoilReportApp.Application.Interfaces;
using SoilReportApp.Web.Models;
using X.PagedList;
using X.PagedList.Extensions;

namespace SoilReportApp.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUserService _userService;
    private readonly IRequestService _requestService;

    public HomeController(
        ILogger<HomeController> logger,
        IUserService userService,
        IRequestService requestService)
    {
        _logger = logger;
        _userService = userService;
        _requestService = requestService;
    }
    
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost("createUser")]
    [Consumes("application/json")]
    public async Task<IActionResult> CreateUser([FromBody] UserViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return Json(new { success = false, message = "Invalid user data." });
        }

        var request = new CreateUserRequest
        {
            Username = model.Username,
            Email = model.Email,
            Password = model.Password,
            Phone = model.Phone,
            UserType = model.UserType,
            DeviceId = string.IsNullOrEmpty(model.DeviceId) ? 0 : int.Parse(model.DeviceId)
        };

        try
        {
            var user = await _userService.CreateUserAsync(request);
            return Json(new { success = true, user });
        }
        catch (InvalidOperationException ex)
        {
            return Json(new { success = false, message = ex.Message });
        }
    }
    
    [HttpPost("SendReading")]
    [Consumes("application/json")]
    public async Task<IActionResult> SendReading([FromBody] ReadingsViewModel reading)
    {
        if (!ModelState.IsValid)
        {
            return Json(new { success = false, message = "Please fill all fields" });
        }

        var request = new CreateRequestFromReadingsRequest
        {
            DeviceId = reading.DeviceId,
            Readings = reading.Readings.Select(r => new ReadingDto
            {
                Test = r.Test,
                N = r.N,
                P = r.P,
                K = r.K,
                Moisture = r.Moisture
            }).ToList()
        };

        try
        {
            await _requestService.CreateFromReadingsAsync(request);
            return Json(new { success = true });
        }
        catch (InvalidOperationException ex)
        {
            return Json(new { success = false, message = ex.Message });
        }
    }

    [HttpGet("users")]
    public async Task<IActionResult> Users(int? page)
    {
        int pageSize = 10; // Number of users per page
        int pageNumber = page ?? 1;

        var usersPage = await _userService.GetAllUsersPagedAsync(pageNumber, pageSize);
        var totalCount = await _userService.GetTotalCountAsync();

        var userViewModels = usersPage.Select(u => new UserViewModel
        {
            Id = u.Id,
            UserType = u.UserType,
            DeviceId = u.DeviceId.ToString(),
            Email = u.Email,
            Phone = u.Phone,
            Username = u.Username
        }).ToList();

        var pagedUsers = new StaticPagedList<UserViewModel>(userViewModels, pageNumber, pageSize, totalCount);

        return View(pagedUsers);
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