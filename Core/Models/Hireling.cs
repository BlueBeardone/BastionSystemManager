namespace Core.Models;

public class Hireling
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Cost { get; set; }
    public bool IsActive { get; set; }

    //Foreign keys
    public int? FacilityId { get; set; }
    public Facility Facility { get; set; }
    public int BastionId { get; set; }
    public Bastion Bastion { get; set; }
}