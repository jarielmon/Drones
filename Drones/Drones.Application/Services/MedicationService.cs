using AutoMapper;
using Drones.Domain;
using Drones.Domain.DTOs;
using Drones.Domain.Entities;
using Drones.Domain.Exceptions;
using Drones.Domain.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Drones.Application.Services;

public class MedicationService : IMedicationService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public MedicationService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<MedicationDto> AddMedication(MedicationDto medicationDto)
    {
        var medication = _mapper.Map<Medication>(medicationDto);
        
        await _unitOfWork.MedicationRepository.AddAsync(medication);
        await _unitOfWork.SaveAsync();

        return _mapper.Map<MedicationDto>(medication);
    }

    public async Task UpdateMedication(MedicationDto medicationDto)
    {
        var exists = await _unitOfWork.MedicationRepository.ExistAsync(x => x.Id == medicationDto.Id);
        if (!exists)
            throw new NotFoundException("The medication doesn't exist");
        
        var medication = _mapper.Map<Medication>(medicationDto);
        await _unitOfWork.MedicationRepository.UpdateAsync(medication);
        await _unitOfWork.SaveAsync();
    }

    public async Task DeleteMedication(int medicationId)
    {
        var exists = await _unitOfWork.MedicationRepository.ExistAsync(x => x.Id == medicationId);
        if (!exists)
            throw new NotFoundException("The medication doesn't exist.");

        var medication = await _unitOfWork.MedicationRepository.GetByIdAsync(medicationId);
        await _unitOfWork.MedicationRepository.DeleteAsync(medication);
        await _unitOfWork.SaveAsync();
    }

    public async Task<MedicationDto> GetMedication(int medicationId)
    {
        var exists = await _unitOfWork.MedicationRepository.ExistAsync(x => x.Id == medicationId);
        if (!exists)
            throw new NotFoundException("The medication doesn't exist.");

        var medication = await _unitOfWork.MedicationRepository.GetByIdAsync(medicationId);
        var medicationDto = _mapper.Map<MedicationDto>(medication);

        return medicationDto;
    }

    public async Task<IEnumerable<MedicationDto>> GetMedications()
    {
        var medications = await _unitOfWork.MedicationRepository.GetAllAsync();
        var medicationsDto = _mapper.Map<IEnumerable<MedicationDto>>(medications);

        return medicationsDto;
    }
}
