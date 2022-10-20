using Drones.Domain;
using Drones.Domain.Repositories;
using Drones.Infrastructure.Database;
using System.Threading.Tasks;

namespace Drones.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    public IDroneRepository DroneRepository { get; set; }
    public IMedicationRepository MedicationRepository { get; set; }
    public IDroneMedicationRepository DroneMedicationRepository { get; set; }

    private readonly ApiContext _apiContext;

    public UnitOfWork(ApiContext apiContext, IDroneRepository droneRepository, IMedicationRepository medicationRepository, IDroneMedicationRepository droneMedicationRepository)
    {
        _apiContext = apiContext;

        DroneRepository = droneRepository;
        MedicationRepository = medicationRepository;
        DroneMedicationRepository = droneMedicationRepository;
    }

    public async Task<int> SaveAsync() => await _apiContext.SaveChangesAsync();

    public void Dispose() => _apiContext.Dispose();
}
