using Drones.Domain.Repositories;
using System;
using System.Threading.Tasks;

namespace Drones.Domain;

public interface IUnitOfWork : IDisposable
{
    IDroneRepository DroneRepository { get; set; }
    IMedicationRepository MedicationRepository { get; set; }
    IDroneMedicationRepository DroneMedicationRepository { get; set; }
    Task<int> SaveAsync();
}
