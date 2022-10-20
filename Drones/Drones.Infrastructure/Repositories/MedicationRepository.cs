using Drones.Domain.Entities;
using Drones.Domain.Repositories;
using Drones.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Drones.Infrastructure.Repositories;

public class MedicationRepository : Repository<Medication>, IMedicationRepository
{
    public MedicationRepository(DbSet<Medication> medications) : base(medications)
    {
    }
}
