using Drones.Domain.Entities;
using Drones.Domain.Repositories;
using Drones.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Drones.Infrastructure.Repositories;

public class DroneRepository : Repository<Drone>, IDroneRepository
{
    public DroneRepository(DbSet<Drone> drones) : base(drones)
    {
    }
}
