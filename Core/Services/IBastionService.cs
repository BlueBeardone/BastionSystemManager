namespace Core.Services;

using Core.Models;
using Core.Models.Enums;

public interface IBastionService
{
    Task<Bastion> GetBastionAsync(int id);
    Task<Bastion> CreateBastionAsync(string userId, string name);
    Task<bool> BuildFacilityAsync(int bastionId, FacilityType type, string name);
    Task<bool> UpgradeFacilityAsync(int facilityId);
    Task<BastionEvent> ProcessMonthlyTurnAsync(int bastionId);
    Task<BastionEvent> ProcessIndividualEventAsync(int bastionId);
    Task<bool> HireHirelingAsync(int bastionId, int? facilityId = null);
}