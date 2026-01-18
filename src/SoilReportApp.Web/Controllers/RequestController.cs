using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoilReportApp.Infrastructure.Data;
using SoilReportApp.Web.Models;
using X.PagedList.Extensions;
using DomainRequest = SoilReportApp.Domain.Entities.Request;
using DomainRequestStatus = SoilReportApp.Domain.Enums.RequestStatus;
using User = SoilReportApp.Domain.Entities.User;
using UserType = SoilReportApp.Domain.Enums.UserType;

namespace SoilReportApp.Web.Controllers;

[Authorize]
public class RequestController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;
    
    public RequestController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }
    
    [HttpGet("requests")]
    public IActionResult Requests(int? page)
    {
        int pageSize = 10; 
        int pageNumber = (page ?? 1);
        string username = User.Identity.Name;
        User user = _context.Users.FirstOrDefault(u => u.Username == username);

        var requests = _context.Requests
            .Include(r => r.Farmer)
            .Include(r => r.Expert)
            .Include(r => r.Crop)
            .Include(r => r.SoilType)
            .Include(r => r.CropStage)
            .OrderByDescending(r => r.UpdateDate);
            //.ToPagedList(pageNumber, pageSize);

        if (user.UserType == UserType.Expert && username != "admin")
        {
            var req = requests
                .Where(r => (r.Status != DomainRequestStatus.NotStarted) &&
                                    (
                                        (r.ExpertId == user.Id) || (r.ExpertId == null)
                                    )
                ).ToPagedList(pageNumber, pageSize);
            return View(req);
        }
        else if (user.UserType == UserType.Farmer)
        {
            return View(requests
                .Where(r => r.FarmerId == user.Id)
                .ToPagedList(pageNumber, pageSize));
        }
        else
        {
            return View(requests.ToPagedList(pageNumber, pageSize));
        }
    }
    
    [HttpGet]
    public IActionResult View(Guid id, bool edit = false)
    {
        var request = _context.Requests
            .Where(r => r.Id == id)
            .Include(r => r.Farmer)
            .Include(r => r.Expert)
            .Include(r => r.Crop)
            .Include(r => r.SoilType)
            .Include(r => r.CropStage)
            .Include(r => r.Readings)
            .FirstOrDefault();
        ViewBag.IsEditMode = edit;
        ViewBag.UserRole = User.IsInRole("Expert") ? "Expert" : "Farmer";
        ViewBag.Crops = _context.Crops.ToList();
        ViewBag.SoilTypes = _context.SoilTypes.ToList();
        ViewBag.CropStages = _context.CropStages.ToList();
        return View("ViewRequest", request);
    }
    
    [HttpPost]
    public IActionResult Edit(RequestViewModel model)
    {
        DomainRequest request = Map(model);
        string username = User.Identity.Name;
        User user = _context.Users.FirstOrDefault(u => u.Username == username);
        if (ModelState.IsValid)
        {
            if (user?.UserType == UserType.Expert)
            {
                request.Status = DomainRequestStatus.CompletedByExpert;
                request.ExpertId = user.Id;
            }
            else if (user?.UserType == UserType.Farmer)
            {
                request.Status = DomainRequestStatus.CompletedByFarmer;
            }
            
            request.UpdateDate = DateTime.UtcNow;
            request = _context.Requests.Update(request).Entity;
            _context.SaveChanges();
            return RedirectToAction("View", new { id = request.Id });
        }
        ViewBag.IsEditMode = true;
        return View("ViewRequest", request);
    }
    
    // Private methods
    private DomainRequest Map(RequestViewModel model)
    {
        var req = new DomainRequest()
        {
            Id = model.Id,
            DeviceId = model.DeviceId,
            NAvg = model.NAvg,
            PAvg = model.PAvg,
            KAvg = model.KAvg,
            MoistureAvg = model.MoistureAvg,
            SoilTypeId = model.SoilTypeId,
            CropId = model.CropId,
            CropStageId = model.CropStageId,
            Report = model.Report,
            FarmerId = model.FarmerId,
            ExpertId = model.ExpertId,
        };

        DomainRequestStatus.TryParse(model.Status.ToString(), out DomainRequestStatus staus);
        req.Status = (DomainRequestStatus)staus;
        return req;
    }
}