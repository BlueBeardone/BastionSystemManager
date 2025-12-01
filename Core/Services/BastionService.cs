namespace Services;

using Core.Models;
using Core.Models.Enums;
using Core.Services;
using Data;
using Microsoft.EntityFrameworkCore;


public class BastionService : IBastionService
{
    private readonly ApplicationDbContext _context;
    private readonly IEventService _eventService;

    public BastionService(ApplicationDbContext context, IEventService eventService)
    {
        _context = context;
        _eventService = eventService;
    }

    public async Task<Bastion> CreateBastionAsync(string userId, string name)
    {
        var bastion = new Bastion
        {
            Name = name,
            userId = userId,
            Rank = Rank.D,
            Gold = 1000,
            CreatedAt = DateTime.UtcNow
        };

        _context.Bastions.Add(bastion);
        await _context.SaveChangesAsync();
        return bastion;
    }

    public async Task<book> BuildFacilityAsync(int bastionId, FacilityType type, string name)
    {
        var bastion = await _context.Bastions.FindAsync(bastionId);
        if (bastion == null) return false;

        Facility facility;
        if (type == FacilityType.Basic)
        {
            if (!Enum.TryParse<BasicFacilityType>(facilityName, out var basicType))
                return false;

            facility = new BasicFacility
            {
                Name = facilityName,
                Type = FacilityType.Basic,
                SubType = basicType,
                Rank = Rank.D,
                Size = FacilitySize.Cramped,
                BastionId = bastionId,
                ConstructionProgress = 0,
                ConstructionTimeRequired = GetConstructionTime(FacilitySize.Cramped, type)
            };
        }
        else
        {
            if (!Enum.TryParse<SpecialFacilityType>(facilityName, out var specialType))
                return false;

            facility = new SpecialFacility
            {
                Name = facilityName,
                Type = FacilityType.Special,
                SubType = specialType,
                Rank = Rank.C, // Special facilities start at C rank
                Size = FacilitySize.Cramped,
                BastionId = bastionId,
                ConstructionProgress = 0,
                ConstructionTimeRequired = GetConstructionTime(FacilitySize.Cramped, type)
            };
        }

        _context.Facilities.Add(facility);
        await _context.SaveChangesAsync();
        return true;
    }

    private int GetConstructionTime(FacilitySize size, FacilityType type)
    {
        int baseTime = type == FacilityType.Basic ? 30 : 60; // days
        return size switch
        {
            FacilitySize.Cramped => baseTime,
            FacilitySize.Roomy => baseTime * 2,
            FacilitySize.Vast => baseTime * 3,
            _ => baseTime
        };
    }

    public async Task<BastionEvent> ProcessMonthlyTurnAsync(int bastionId)
    {
        var bastion = await _context.Bastions
            .Include(b => b.Facilities)
            .FirstOrDefaultAsync(b => b.Id == bastionId);

        if (bastion == null) return null;

        // Process construction progress
        foreach (var facility in bastion.Facilities.Where(f => f.IsUnderConstruction))
        {
            facility.ConstructionProgress++;
        }

        // Generate monthly event
        var bastionEvent = await _eventService.GenerateMonthlyEventAsync(bastion);
        
        _context.BastionEvents.Add(bastionEvent);
        await _context.SaveChangesAsync();
        
        return bastionEvent;
    }
}