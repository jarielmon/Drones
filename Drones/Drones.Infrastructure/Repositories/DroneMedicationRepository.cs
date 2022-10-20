using Drones.Domain.Entities;
using Drones.Domain.Repositories;
using Drones.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Drones.Infrastructure.Repositories;

public class DroneMedicationRepository : Repository<DroneMedication>, IDroneMedicationRepository
{
    public DroneMedicationRepository(DbSet<DroneMedication> droneMedications) : base(droneMedications)
    {
    }
}
