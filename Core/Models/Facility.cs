namespace Core.Models;

public abstract class Facility
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public FacilityType FacilityType { get; set; }
    public Rank Rank { get; set; }
    public FacilitySize Size { get; set; }
    public int ConstructionProgress { get; set; }
    public int ConstructionTimeRequired { get; set; }
    public bool IsUnderConstruction => ConstructionProgress < ConstructionTimeRequired;

    //Foreign keys
    public int BastionId { get; set; }
    public Bastion Bastion { get; set; }
    public ICollection<Hireling> Hirelings { get; set; }
}

public class BasicFacility : Facility
{
    public BasicFacilityType SubType { get; set; }
}   

public class SpecialFacility : Facility
{
    public SpecialFacilityType SubType { get; set; }
}

