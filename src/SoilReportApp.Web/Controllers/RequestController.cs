using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoilReportApp.Application.DTOs.Requests;
using SoilReportApp.Application.Interfaces;
using SoilReportApp.Domain.Enums;
using SoilReportApp.Web.Models;
using X.PagedList;

namespace SoilReportApp.Web.Controllers;

[Authorize]
public class RequestController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IRequestService _requestService;
    
    public RequestController(ILogger<HomeController> logger, IRequestService requestService)
    {
        _logger = logger;
        _requestService = requestService;
    }
    
    [HttpGet("requests")]
    public async Task<IActionResult> Requests(int? page)
    {
        int pageSize = 10;
        int pageNumber = page ?? 1;

        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var roleClaim = User.FindFirstValue(ClaimTypes.Role);
        var username = User.Identity?.Name ?? string.Empty;

        if (!Guid.TryParse(userIdClaim, out var userId) || !Enum.TryParse<UserType>(roleClaim, out var userType))
        {
            return Forbid();
        }

        bool isAdmin = username.Equals("admin", StringComparison.OrdinalIgnoreCase);

        var requests = await _requestService.GetRequestsForUserAsync(userId, userType, isAdmin, pageNumber, pageSize);

        var pagedRequests = new StaticPagedList<RequestListItemResponse>(
            requests.ToList(),
            pageNumber,
            pageSize,
            await _requestService.GetRequestCountForUserAsync(userId, userType, isAdmin));

        return View(pagedRequests);
    }
    
    [HttpGet]
    public async Task<IActionResult> View(Guid id, bool edit = false)
    {
        var request = await _requestService.GetByIdWithDetailsAsync(id);
        if (request is null)
        {
            return NotFound();
        }

        ViewBag.IsEditMode = edit;
        ViewBag.UserRole = User.IsInRole("Expert") ? "Expert" : "Farmer";
        ViewBag.Crops = await _requestService.GetAllCropsAsync();
        ViewBag.SoilTypes = await _requestService.GetAllSoilTypesAsync();
        ViewBag.CropStages = await _requestService.GetAllCropStagesAsync();

        return View("ViewRequest", request);
    }
    
    [HttpPost]
    public async Task<IActionResult> Edit(RequestViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.IsEditMode = true;

            var existing = await _requestService.GetByIdWithDetailsAsync(model.Id);
            return View("ViewRequest", existing);
        }

        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var roleClaim = User.FindFirstValue(ClaimTypes.Role);

        if (!Guid.TryParse(userIdClaim, out var userId) || !Enum.TryParse<UserType>(roleClaim, out var userType))
        {
            return Forbid();
        }

        var updateRequest = new UpdateRequestRequest
        {
            Id = model.Id,
            SoilTypeId = model.SoilTypeId,
            CropId = model.CropId,
            CropStageId = model.CropStageId,
            Report = model.Report
        };

        var updated = await _requestService.UpdateRequestAsync(updateRequest, userId, userType);

        return RedirectToAction("View", new { id = updated.Id });
    }
}