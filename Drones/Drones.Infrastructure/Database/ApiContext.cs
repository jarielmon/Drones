using Microsoft.EntityFrameworkCore;
using Drones.Domain.Entities;
using Drones.Infrastructure.Database;

namespace Drones.Infrastructure.Database;

public class ApiContext : DbContext
{
    public ApiContext(DbContextOptions<ApiContext> options) : base(options)
    {
    }

    public DbSet<Drone> Drones { get; set; }
    public DbSet<Medication> Medications { get; set; }
    public DbSet<DroneMedication> DroneMedications { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new DroneEntityTypeConfiguration());
        modelBuilder.Entity<Drone>().Property(f => f.Id).ValueGeneratedOnAdd();

        modelBuilder.ApplyConfiguration(new MedicationEntityTypeConfiguration());
        modelBuilder.Entity<Medication>().Property(f => f.Id).ValueGeneratedOnAdd();

        modelBuilder.ApplyConfiguration(new DroneMedicationEntityTypeConfiguration());
        modelBuilder.Entity<DroneMedication>().Property(f => f.Id).ValueGeneratedOnAdd();
    }
}