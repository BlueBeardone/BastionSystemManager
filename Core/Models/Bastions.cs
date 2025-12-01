public class Bastion{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Rank Rank { get; set; }
    public int Gold { get; set; }   
    public DateTime CreatedAt { get; set; }

    //Relationships
    public string UserId { get; set; }
    public ApplicationUser User { get; set; }
    public ICollection<Facility> Facilities { get; set; }
    public ICollection<Hirelings> Hirelings { get; set; }
    public ICollection<Events> Events { get; set; }
}