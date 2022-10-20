using Drones.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drones.Domain.Services;

public interface IDroneMedicationService
{
    Task<DroneMedicationDto> AddDroneMedication(DroneMedicationDto droneMedicationDto);
    Task UpdateDroneMedication(DroneMedicationDto droneMedicationDto);
    Task DeleteDroneMedication(int droneId, int medicationId);
    Task<DroneMedicationDto> GetDroneMedication(int droneId, int medicationId);
    Task<IEnumerable<DroneMedicationDto>> GetDroneMedications();
    Task<IEnumerable<DroneMedicationDto>> GetLoadedMedicationItems(int droneId);
}

