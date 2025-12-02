using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BastionsOfMaura.Core.Enums;

namespace BastionsOfMaura.Core.Models
{
    public abstract class Facility
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public FacilityType FacilityType { get; set; }
        public Rank Rank { get; set; } = Rank.D;
        public FacilitySize Size { get; set; } = FacilitySize.Cramped;

        public int ConstructionProgress { get; set; }
        public int ConstructionTimeRequired { get; set; }
        public DateTime ConstructionStartDate { get; set; } = DateTime.UtcNow;

        public bool IsDestroyed { get; set; } 
        public DateTime? DestroyedDate { get; set; }

        public int MaxHirelings => Size switch
        {
            FacilitySize.Cramped => FacilityType == FacilityType.Basic ? 1 : 0,
            FacilitySize.Roomy => 3,
            FacilitySize.Vast => FacilityType == FacilityType.Basic ? 6 : 9,
            _ => 0
        };

        public bool IsUnderConstruction => ConstructionProgress < ConstructionTimeRequired;
        public bool IsCompleted => !IsUnderConstruction && !IsDestroyed;

        // Foreign keys
        public int BastionId { get; set; }
        public virtual Bastion Bastion { get; set; } = null!;

        // Methods
        public int GetUpgradeCost()
        {
            if (Size == FacilitySize.Cramped)
            {
                return 500;
            }
            else if (Size == FacilitySize.Roomy)
            {
                return 2000;
            }
            else
            {
                return 0; 
            }
        }

        public int GetUpgradeTime()
        {
            if (Size == FacilitySize.Cramped)
            {
                return 4; 
            }
            else if (Size == FacilitySize.Roomy)
            {
                return 11; 
            }
            else
            {
                return 0; 
            }
        }
    }

    public class BasicFacility : Facility
    {
        public BasicFacilityType SubType { get; set;}

        // Basic Facility specific properties
        public bool HasFreeHireling{ get; set;}
        public DateTime LastOrderDate {get; set;}

        public int DefenderCapacity
        {
            get
            {
               var baseCapacity = 4 * ((int)Rank + 1);
               return Size switch
               {
                   FacilitySize.Cramped => baseCapacity,
                   FacilitySize.Roomy => baseCapacity + 2 * ((int)Rank + 1),
                   FacilitySize.Vast => baseCapacity + 4 * ((int)Rank + 1),
                   _ => baseCapacity
               }; 
            }
        }

        // Harvestarium specific 
        public HarvestariumSpecialization? Specialization { get; set;}
        public DateTime? LastHarvestDate {get; set;}

        //Battlements specific
        public bool IsRepelling {get; set;}
        public int DefenseBonus => Size switch
        {
            FacilitySize.Cramped => 1,
            FacilitySize.Roomy => 2,
            FacilitySize.Vast => 3,
            _ => 0
        };
    
    }

    public class SpecialFacility : Facility
    {
        public SpecialFacilityType SubType { get; set; }

        [StringLength(500)]
        public string Prerequisite { get; set; } = string.Empty;

        public DateTime LastOrderDate { get; set; }

        //Workshop specific
        public string? Specialization { get; set; } 

        //Library specific
        public bool HasArcaneInsight {get; set;}

        //Grayveyard specific
        public int BurialSlots => 2 + (2 * (int)Rank);
        public int UsedBurialSlots {get; set;}

        //Siege Workshop specific
        public int SiegeWeaponSlots => 2 + (2 * (int)Rank);
        public int UsedSiegeWeaponSlots {get; set;}

        //Menagerie specific
        public int EnclosureSlots => 8 + (Rank == Rank.S ? 4 : 0);
        public int UsedEnclosureSlots {get; set;}

        //Stables specific
        public bool isMythic {get; set;}
        public int LargeCreatureSlots => isMythic ? 6 : 4;
        public int UsedLargeCreatureSlots {get; set;}

        //Storehouse specific
        public bool HasSpecializedShelving {get; set;}
        public int ProcurementLimit => (int)Rank * 1000 + (HasSpecializedShelving ? 500 : 0);

        //Pub specific
        public string? PubSpecial {get; set;}
        public int StockCount {get; set;}

        // War Room specific
        public int LieutenantSlots => 10;
        public int UsedLieutenantSlots {get; set;}

        //Guild Hall specific
        public string? GuildType {get; set;}
        public int MemberCount {get; set;} = 50;

    }

}