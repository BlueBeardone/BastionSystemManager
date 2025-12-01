namespace Web.ViewModels;
using Core.Models;
using Core.Models.Enums;
using System.Collections.Generic;

public class BastionDetailsViewModel
{
    public Bastion Bastion { get; set; }
    public bool CanBuildBasic { get; set; }
    public bool CanBuildSpecial { get; set; }
    public List<BasicFacilityType> AvailableBasicFacilities { get; set; }
    public List<SpecialFacilityType> AvailableSpecialFacilities { get; set; }
}