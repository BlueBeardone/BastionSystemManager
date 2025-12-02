using System;
using System.ComponentModel.DataAnnotations;
using BastionsOfMaura.Core.Enums;

namespace BastionsOfMaura.Core.Models
{
    public class Hireling
    {
        public int Id {get; set;}

        [Required]
        [StringLength(100)]
        public string Name {get; set;} = string.Empty;

        [StringLength(500)]
        public string Description {get; set;} = string.Empty;

        [StringLength(100)]
        public string Role {get; set;} = string.Empty;

        public int HireCost {get; set;}
        public int WeeklySalary {get; set;}
        public bool IsActive {get; set;} = true;
        public DateTime HireDate {get; set;} = DateTime.UtcNow;

        //Skills and Abilities
        public bool HasCriminalPast {get; set;}
        public bool IsSquire {get; set;}
        public bool IsKnight {get; set;}
        public bool IsGravedigger {get; set;}
        public bool IsTrainer {get; set;}
        public bool IsBartender {get; set;}
        public bool IsLieutenant {get; set;}

        //stats
        public int CharismaBonus {get; set;}
        public bool HasDisguiseKitProficiency {get; set;}
        public bool HasDisguiseKitExpertise {get; set;}

        // Foreign keys
        public int? FacilityId {get; set;}
        public virtual Facility? Facility {get; set;}

        public int BastionId {get; set;}
        public virtual Bastion Bastion {get; set;} = null!;

        // Computed Properties
        public int EffectiveCost
        {
            get
            {
                var cost = HireCost;
                var reduction = 0;

                //Charisma Bonus Reduction
                reduction += CharismaBonus >= 19 ? 100:
                             CharismaBonus >= 17 ? 75 :
                             CharismaBonus >= 15 ? 50 :
                             CharismaBonus >= 13 ? 25 : 0;
                
                //Desguise Kit Proficiency Reduction
                reduction += HasDisguiseKitExpertise ? 25 :
                             HasDisguiseKitProficiency ? 10 : 0;

                return Math.Max(0, cost - (cost * reduction / 100));
            }
        }

        public bool CanWorkInFacility(Facility facility)
        {
            if (IsSquire)
            {
                return (int)facility.Rank <= (int)facility.Rank + 1;
            }

            return true;
        }
    }
}