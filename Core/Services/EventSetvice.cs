namespace Services;

using Core.Models;
using Core.Models.Enums;
using Core.Services;
using Data;

public class EventService : IEventService
{
    private readonly Random _random = new();

    public async Task<BastionEvent> GenerateMonthlyEventAsync(Bastion bastion)
    {
        var roll = _random.Next(1, 101);
        var eventType = roll switch
        {
            1 => MonthlyEventType.GoldenAge,
            >= 2 and <= 19 => MonthlyEventType.NothingHappens,
            >= 20 and <= 30 => MonthlyEventType.Siege,
            >= 31 and <= 40 => MonthlyEventType.Invasion,
            >= 41 and <= 55 => MonthlyEventType.Drought,
            >= 56 and <= 65 => MonthlyEventType.Marauders,
            >= 66 and <= 72 => MonthlyEventType.Storm,
            >= 73 and <= 85 => MonthlyEventType.Refugees,
            >= 86 and <= 93 => MonthlyEventType.SearchParty,
            >= 94 and <= 98 => MonthlyEventType.Festival,
            _ => MonthlyEventType.AbnormalPhenomenon
        };

        var bastionEvent = new BastionEvent
        {
            BastionId = bastion.Id,
            EventType = eventType.ToString(),
            Description = GetEventDescription(eventType),
            OccurredDate = DateTime.UtcNow,
            IsProcessed = false
        };

        await ApplyEventEffectsAsync(bastion, eventType);
        return bastionEvent;
    }

    private string GetEventDescription(MonthlyEventType eventType) => eventType switch
    {
        MonthlyEventType.GoldenAge => "A golden age begins! All gold rewards increased by 10%.",
        MonthlyEventType.Siege => "Your bastion is under siege! Movement in and out is difficult.",
        MonthlyEventType.Invasion => "A hostile kingdom musters its armies! Rally the troops!",
        MonthlyEventType.Drought => "Water becomes scarce. All hirelings are exhausted.",
        MonthlyEventType.Marauders => "The entire countryside is under attack! 50% more attackers this month.",
        MonthlyEventType.Storm => "A terrible storm damages your bastion!",
        MonthlyEventType.Refugees => "Refugees arrive seeking protection and work.",
        MonthlyEventType.SearchParty => "A VIP has disappeared! A search party is called.",
        MonthlyEventType.Festival => "A grand festival begins in the region.",
        MonthlyEventType.AbnormalPhenomenon => "Strange and unexplained events occur...",
        _ => "Nothing of significance happens this month."
    };

    private async Task ApplyEventEffectsAsync(Bastion bastion, MonthlyEventType eventType)
    {
        switch (eventType)
        {
            case MonthlyEventType.GoldenAge:
                // Implement golden age effects
                break;
            case MonthlyEventType.Siege:
                // Implement siege effects
                break;
            case MonthlyEventType.Storm:
                // Damage random facility
                var facilities = bastion.Facilities.ToList();
                if (facilities.Any())
                {
                    var randomFacility = facilities[_random.Next(facilities.Count)];
                    // Mark facility as damaged
                }
                break;
        }
    }
}