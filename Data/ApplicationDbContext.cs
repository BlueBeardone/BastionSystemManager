namespace Data;

using Core.Models;
using Core.Models.Enums;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Bastion> Bastions { get; set; }
    public DbSet<Facility> Facilities { get; set; }
    public DbSet<BasicFacility> BasicFacilities { get; set; }
    public DbSet<SpecialFacility> SpecialFacilities { get; set; }
    public DbSet<Hireling> Hirelings { get; set; }
    public DbSet<BastionEvents> BastionEvents { get; set; }
    public DbSet<Rewards> Rewards { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        // Configure TPH for Facility inheritance
        builder.Entity<Facility>()
            .HasDiscriminator<FacilityType>("FacilityType")
            .HasValue<BasicFacility>(FacilityType.Basic)
            .HasValue<SpecialFacility>(FacilityType.Special);
            
        // Configure relationships
        builder.Entity<Bastion>()
            .HasMany(b => b.Facilities)
            .WithOne(f => f.Bastion)
            .HasForeignKey(f => f.BastionId);
            
        builder.Entity<Facility>()
            .HasMany(f => f.Hirelings)
            .WithOne(h => h.Facility)
            .HasForeignKey(h => h.FacilityId);
    }
}