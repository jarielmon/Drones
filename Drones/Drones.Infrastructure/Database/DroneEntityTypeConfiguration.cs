using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Drones.Domain.Entities;

namespace Drones.Infrastructure.Database;

public class DroneEntityTypeConfiguration : IEntityTypeConfiguration<Drone>
{
    public void Configure(EntityTypeBuilder<Drone> builder)
    {
        builder
            .HasKey(bar => bar.Id);

        builder.Property(p => p.SerialNumber).IsRequired();
        builder.Property(p => p.Model).IsRequired();
        builder.Property(p => p.WeightLimit).IsRequired();
        builder.Property(p => p.BatteryCapacity).IsRequired();
        builder.Property(p => p.State).IsRequired();        
    }
}
