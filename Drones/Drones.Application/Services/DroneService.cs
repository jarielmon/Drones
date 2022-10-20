using AutoMapper;
using Drones.Domain;
using Drones.Domain.DTOs;
using Drones.Domain.Entities;
using Drones.Domain.Exceptions;
using Drones.Domain.Repositories;
using Drones.Domain.Services;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Drones.Application.Services;

public class DroneService : IDroneService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;    

    public DroneService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;        
    }

    public async Task<DroneDto> AddDrone(DroneDto droneDto)
    {
        var drone = _mapper.Map<Drone>(droneDto);
        
        await _unitOfWork.DroneRepository.AddAsync(drone);
        await _unitOfWork.SaveAsync();

        return _mapper.Map<DroneDto>(drone);
    }

    public async Task UpdateDrone(DroneDto droneDto)
    {
        var exists = await _unitOfWork.DroneRepository.ExistAsync(x => x.Id == droneDto.Id);
        if (!exists)
            throw new NotFoundException("The drone doesn't exist");
        
        var drone = _mapper.Map<Drone>(droneDto);
        await _unitOfWork.DroneRepository.UpdateAsync(drone);
        await _unitOfWork.SaveAsync();
    }

    public async Task DeleteDrone(int droneId)
    {
        var exists = await _unitOfWork.DroneRepository.ExistAsync(x => x.Id == droneId);
        if (!exists)
            throw new NotFoundException("The drone doesn't exist.");

        var drone = await _unitOfWork.DroneRepository.GetByIdAsync(droneId);
        await _unitOfWork.DroneRepository.DeleteAsync(drone);
        await _unitOfWork.SaveAsync();
    }

    public async Task<DroneDto> GetDrone(int droneId)
    {
        var exists = await _unitOfWork.DroneRepository.ExistAsync(x => x.Id == droneId);
        if (!exists)
            throw new NotFoundException("The drone doesn't exist.");

        var drone = await _unitOfWork.DroneRepository.GetByIdAsync(droneId);
        var droneDto = _mapper.Map<DroneDto>(drone);

        return droneDto;
    }

    public async Task<IEnumerable<DroneDto>> GetDrones()
    {        
        var drones = await _unitOfWork.DroneRepository.GetAllAsync();
        var dronesDto = _mapper.Map<IEnumerable<DroneDto>>(drones);

        return dronesDto;
    }

    public async Task<IEnumerable<DroneDto>> GetAvailableDronesForLoading()
    {
        var predicates = new List<Expression<Func<Drone, bool>>>() {
            (x => x.State == State.IDLE),
            (x => x.BatteryCapacity >= 25)
        };
          
        var drones = await _unitOfWork.DroneRepository.GetAllAsync(predicates);
        var dronesDto = _mapper.Map<IEnumerable<DroneDto>>(drones);

        return dronesDto;
    }

    public async Task<string> GetBatteryLevel(int droneId)
    {
        var exists = await _unitOfWork.DroneRepository.ExistAsync(x => x.Id == droneId);
        if (!exists)
            throw new NotFoundException("The drone doesn't exist.");

        var drone = await _unitOfWork.DroneRepository.GetByIdAsync(droneId);        

        return drone.BatteryCapacity.ToString();
    }
}
