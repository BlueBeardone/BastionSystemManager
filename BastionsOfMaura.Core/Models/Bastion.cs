using System;
using System.Collections.Generic;
using BastionsOfMaura.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace BastionsOfMaura.Core.Models
{
    public class Bastion
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; } = string.Empty;

        public string UserId { get; set; } = string.Empty;

        public Rank Rank { get; set; } = Rank.D;

        [Range(0, int.MaxValue)]
        public int Gold { get; set; } = 1000;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime LastTurnDate { get; set; } = DateTime.UtcNow;

        // Navigation property for related facilities
        public virtual ICollection<Facility> Facilities { get; set; } = new List<Facility>();
        public virtual ICollection<Hireling> Hirelings { get; set; } = new List<Hireling>();
        public virtual ICollection<BastionEvent> Events { get; set; } = new List<BastionEvent>();
        public virtual ICollection<BastionDefender> Defenders { get; set; } = new List<BastionDefender>();
        public virtual ICollection<SiegeWeapon> SiegeWeapons { get; set; } = new List<SiegeWeapon>();
        public virtual ICollection<Reward> Rewards { get; set; } = new List<Reward>();

        //Combined Bastions
        public int? CombinedBastionGroupId { get; set; }
        public virtual CombinedBastionGroup? CombinedBastionGroup { get; set; }

        // Computed properties
        public int TotalDefenders => Defenders.Count(d => d.IsActive);
        public int AvailableDefenderSlots => Defenders.Count(d => d.IsActive && !d.IsDeployed);
        public int TotalHirelings => Hirelings.Count(h => h.IsActive);
        public int ActiveFacilities => Facilities.Count(f => f.IsCompleted && !f.Destroyed);
        public bool IsUnderSiege {get; set; } = false;
        public DateTime? SiegeStartDate {get; set; } = null;

        //Methods
        public bool CanBuildBasicFacility()
        {
            var basicCount = Facilities.Count(f => f.FacilityType == FacilityType.Basic);
            return basicCount < GetMaxBasicFacilities();
        }

        public bool CanBuildSpecialFacility()
        {
            var specialCount = Facilities.Count(f => f.FacilityType == FacilityType.Special);
            return specialCount < GetMaxSpecialFacilities();
        }

        private int GetMaxBasicFacilities() => 10;

        private int GetMaxSpecialFacilities()
        {
            return Rank switch
            {
                Rank.D => 0,
                Rank.C => 2,
                Rank.B => 4,
                Rank.A => 5,
                Rank.S => 6,
                _ => 0,
            };
        }

        public int GetConstructionCostReduction()
        {
            //ToDo
            return 0;
        }

    }

    public class CombinedBastionGroup
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        
        public virtual ICollection<Bastion> Bastions { get; set; } = new List<Bastion>();
    }
}
