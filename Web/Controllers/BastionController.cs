namespace Controllers;

using System.Security.Claims;
using Core.Models;
using Core.Models.Enums;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


[Authorize]
public class BastionsController : Controller
{
    private readonly IBastionService _bastionService;
    private readonly IEventService _eventService;

    public BastionsController(IBastionService bastionService, IEventService eventService)
    {
        _bastionService = bastionService;
        _eventService = eventService;
    }

    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var bastions = await _bastionService.GetUserBastionsAsync(userId);
        return View(bastions);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateBastionViewModel model)
    {
        if (ModelState.IsValid)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var bastion = await _bastionService.CreateBastionAsync(userId, model.Name);
            return RedirectToAction(nameof(Details), new { id = bastion.Id });
        }
        return View(model);
    }

    public async Task<IActionResult> Details(int id)
    {
        var bastion = await _bastionService.GetBastionAsync(id);
        if (bastion == null) return NotFound();

        var viewModel = new BastionDetailsViewModel
        {
            Bastion = bastion,
            CanBuildBasic = bastion.Facilities.Count(f => f.Type == FacilityType.Basic) < GetMaxBasicFacilities(bastion.Rank),
            CanBuildSpecial = bastion.Facilities.Count(f => f.Type == FacilityType.Special) < GetMaxSpecialFacilities(bastion.Rank)
        };

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> BuildFacility(int bastionId, string facilityType, string facilityName)
    {
        var type = Enum.Parse<FacilityType>(facilityType);
        var success = await _bastionService.BuildFacilityAsync(bastionId, type, facilityName);
        
        if (success)
            TempData["Success"] = $"{facilityName} construction started!";
        else
            TempData["Error"] = "Failed to start construction.";

        return RedirectToAction(nameof(Details), new { id = bastionId });
    }

    [HttpPost]
    public async Task<IActionResult> ProcessMonthlyTurn(int bastionId)
    {
        var bastionEvent = await _bastionService.ProcessMonthlyTurnAsync(bastionId);
        
        if (bastionEvent != null)
        {
            TempData["Event"] = $"Monthly Event: {bastionEvent.Description}";
        }

        return RedirectToAction(nameof(Details), new { id = bastionId });
    }

    private int GetMaxBasicFacilities(Rank rank) => rank switch
    {
        Rank.D => 3,
        Rank.C => 6,
        Rank.B => 9,
        Rank.A => 12,
        Rank.S => 15,
        _ => 3
    };

    private int GetMaxSpecialFacilities(Rank rank) => rank switch
    {
        Rank.D => 0,
        Rank.C => 2,
        Rank.B => 4,
        Rank.A => 5,
        Rank.S => 6,
        _ => 0
    };
}