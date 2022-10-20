using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Drones.Domain.Entities;

namespace Drones.Infrastructure.Database;

public class MedicationEntityTypeConfiguration : IEntityTypeConfiguration<Medication>
{
    public void Configure(EntityTypeBuilder<Medication> builder)
    {
        builder
            .HasKey(bar => bar.Id);

        builder.Property(p => p.Name).IsRequired();
        builder.Property(p => p.Weight).IsRequired();
        builder.Property(p => p.Code).IsRequired();        
    }
}
