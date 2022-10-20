using Drones.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drones.Domain.Services;

public interface IDroneService
{
    Task<DroneDto> AddDrone(DroneDto droneDto);
    Task UpdateDrone(DroneDto droneDto);
    Task DeleteDrone(int droneId);
    Task<DroneDto> GetDrone(int droneId);
    Task<IEnumerable<DroneDto>> GetDrones();    
    Task<IEnumerable<DroneDto>> GetAvailableDronesForLoading();
    Task<string> GetBatteryLevel(int droneId);
}

