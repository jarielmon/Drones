using AutoMapper;
using Drones.Domain;
using Drones.Domain.Common;
using Drones.Domain.DTOs;
using Drones.Domain.Entities;
using Drones.Domain.Exceptions;
using Drones.Domain.Services;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Drones.Application.Services;

public class DroneMedicationService : IDroneMedicationService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DroneMedicationService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<DroneMedicationDto> AddDroneMedication(DroneMedicationDto droneMedicationDto)
    {
        var drone = await _unitOfWork.DroneRepository.GetByIdAsync(droneMedicationDto.DroneId);
        var medication = await _unitOfWork.MedicationRepository.GetByIdAsync(droneMedicationDto.MedicationId);

        if (drone == null)
            throw new NotFoundException("The drone doesn't exist");

        if (medication == null)
            throw new NotFoundException("The medication doesn't exist");

        if (drone.State != State.IDLE && drone.State != State.LOADING)
            throw new ConflictException("The drone is not available for loading");

        if (drone.BatteryCapacity < 25)
            throw new ConflictException("The drone battery is below 25%");

        var currentDroneMedication = await GetLoadedMedicationItems(droneMedicationDto.DroneId);
        var newCapacity = droneMedicationDto.Count * medication.Weight;

        foreach(var elem in currentDroneMedication)
        {
            var temp = await _unitOfWork.MedicationRepository.GetByIdAsync(elem.MedicationId);
            newCapacity += temp.Weight * elem.Count;
        }

        if (drone.WeightLimit < newCapacity)
            throw new ConflictException("The weight of the medication is greater than the cargo availability of the drone");

        var droneMedication = _mapper.Map<DroneMedication>(droneMedicationDto);
        
        await _unitOfWork.DroneMedicationRepository.AddAsync(droneMedication);

        drone.State = State.LOADING;
        if(drone.WeightLimit == newCapacity)        
            drone.State = State.LOADED;                    

        await _unitOfWork.DroneRepository.UpdateAsync(drone);
        await _unitOfWork.SaveAsync();

        return _mapper.Map<DroneMedicationDto>(droneMedication);
    }

    public async Task UpdateDroneMedication(DroneMedicationDto droneMedicationDto)
    {
        var exists = await _unitOfWork.DroneMedicationRepository.ExistAsync(x => x.DroneId == droneMedicationDto.DroneId && x.MedicationId == droneMedicationDto.MedicationId);
        if (!exists)
            throw new NotFoundException("The drone medication doesn't exist");

        var drone = await _unitOfWork.DroneRepository.GetByIdAsync(droneMedicationDto.DroneId);
        var medication = await _unitOfWork.MedicationRepository.GetByIdAsync(droneMedicationDto.MedicationId);

        if (drone.State != State.LOADING && drone.State != State.LOADED)
            throw new ConflictException("The drone is not available for laoding");

        if (drone.BatteryCapacity < 25)
            throw new ConflictException("The drone battery is below 25%");

        var currentDroneMedication = await GetLoadedMedicationItems(droneMedicationDto.DroneId);
        var newCapacity = droneMedicationDto.Count * medication.Weight;

        foreach (var elem in currentDroneMedication)
        {
            if(elem.DroneId != droneMedicationDto.DroneId && elem.MedicationId != droneMedicationDto.MedicationId)
            {
                var temp = await _unitOfWork.MedicationRepository.GetByIdAsync(elem.MedicationId);
                newCapacity += temp.Weight * elem.Count;
            }            
        }

        if (drone.WeightLimit < newCapacity)
            throw new ConflictException("The weight of the medication is greater than the cargo availability of the drone");

        var droneMedication = _mapper.Map<DroneMedication>(droneMedicationDto);
        await _unitOfWork.DroneMedicationRepository.UpdateAsync(droneMedication);

        if (drone.WeightLimit == newCapacity)
            drone.State = State.LOADED;

        await _unitOfWork.DroneRepository.UpdateAsync(drone);
        await _unitOfWork.SaveAsync();
    }

    public async Task DeleteDroneMedication(int droneId, int medicationId)
    {        
        var exists = await _unitOfWork.DroneMedicationRepository.ExistAsync(x => x.DroneId == droneId && x.MedicationId == medicationId);
        if (!exists)
            throw new NotFoundException("The drone medication doesn't exist");

        var predicates = new List<Expression<Func<DroneMedication, bool>>>() {
            (x => x.DroneId == droneId),
            (x => x.MedicationId == medicationId)
        };

        var count = await _unitOfWork.DroneMedicationRepository.Count(new List<Expression<Func<DroneMedication, bool>>>() { (x => x.DroneId == droneId) });
        if(count == 1)
        {
            var drone = await _unitOfWork.DroneRepository.GetByIdAsync(droneId);
            drone.State = State.IDLE;
        }

        var droneMedication = await _unitOfWork.DroneMedicationRepository.GetAllAsync(predicates);
        await _unitOfWork.DroneMedicationRepository.DeleteAsync(droneMedication.FirstOrDefault());
        await _unitOfWork.SaveAsync();
    }

    public async Task<DroneMedicationDto> GetDroneMedication(int droneId, int medicationId)
    {
        var exists = await _unitOfWork.DroneMedicationRepository.ExistAsync(x => x.Id == droneId && x.MedicationId == medicationId);
        if (!exists)
            throw new NotFoundException("The drone medication doesn't exist.");

        var predicates = new List<Expression<Func<DroneMedication, bool>>>() {
            (x => x.DroneId == droneId),
            (x => x.MedicationId == medicationId)
        };

        var droneMedication = await _unitOfWork.DroneMedicationRepository.GetAllAsync(predicates);        
        var droneMedicationDto = _mapper.Map<DroneMedicationDto>(droneMedication.FirstOrDefault());

        return droneMedicationDto;
    }

    public async Task<IEnumerable<DroneMedicationDto>> GetDroneMedications()
    {
        var droneMedications = await _unitOfWork.DroneMedicationRepository.GetAllAsync();
        var droneMedicationsDto = _mapper.Map<IEnumerable<DroneMedicationDto>>(droneMedications);

        return droneMedicationsDto;
    }

    public async Task<IEnumerable<DroneMedicationDto>> GetLoadedMedicationItems(int droneId)
    {
        var exists = await _unitOfWork.DroneRepository.ExistAsync(x => x.Id == droneId);
        if (!exists)
            throw new NotFoundException("The drone medication doesn't exist.");

        var predicates = new List<Expression<Func<DroneMedication, bool>>>() {
            (x => x.DroneId == droneId)
        };

        var droneMedications = await _unitOfWork.DroneMedicationRepository.GetAllAsync(predicates);
        var droneMedicationsDto = _mapper.Map<IEnumerable<DroneMedicationDto>>(droneMedications);

        return droneMedicationsDto;
    }
}
