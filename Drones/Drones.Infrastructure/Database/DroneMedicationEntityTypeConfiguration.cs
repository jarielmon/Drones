using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Drones.Domain.Entities;

namespace Drones.Infrastructure.Database;

public class DroneMedicationEntityTypeConfiguration : IEntityTypeConfiguration<DroneMedication>
{
    public void Configure(EntityTypeBuilder<DroneMedication> builder)
    {
        builder
            .HasKey(bc => new { bc.DroneId, bc.MedicationId });

        builder
            .HasOne(bc => bc.Drone)
            .WithMany(b => b.DroneMedications)
            .HasForeignKey(bc => bc.DroneId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(bc => bc.Medication)
            .WithMany(c => c.DroneMedications)
            .HasForeignKey(bc => bc.MedicationId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
