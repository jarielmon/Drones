using Drones.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drones.Domain.Services;

public interface IMedicationService
{
    Task<MedicationDto> AddMedication(MedicationDto medicationDto);
    Task UpdateMedication(MedicationDto medicationDto);
    Task DeleteMedication(int medicationId);
    Task<MedicationDto> GetMedication(int medicationId);
    Task<IEnumerable<MedicationDto>> GetMedications();    
}

