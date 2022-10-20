using Drones.Domain.DTOs;
using Drones.Domain.Exceptions;
using Drones.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Drones.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DroneController : ControllerBase
    {
        private readonly IDroneService _droneService;

        public DroneController(IDroneService droneService)
        {
            _droneService = droneService;
        }

        [HttpGet]       
        public async Task<ActionResult<IEnumerable<DroneDto>>> Get()
        {
            var result = await _droneService.GetDrones();
            return Ok(result);
        }

        [HttpGet("{droneId}")]
        public async Task<ActionResult<DroneDto>> Get(int droneId)
        {
            try
            {
                var result = await _droneService.GetDrone(droneId);
                return Ok(result);
            }
            catch (NotFoundException e)
            {
                return Ok(new
                {
                    error = e.Message
                });
            }            
        }

        [HttpPost]       
        public async Task<ActionResult<DroneDto>> Post([FromBody] DroneDto droneDto)
        {
            var result = await _droneService.AddDrone(droneDto);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] DroneDto droneDto)
        {
            try
            {
                await _droneService.UpdateDrone(droneDto);
                return Ok();
            }
            catch (NotFoundException e)
            {
                return Ok(new
                {
                    error = e.Message
                });
            }            
        }

        [HttpDelete("{droneId}")]
        public async Task<IActionResult> Delete(int droneId)
        {
            try
            {
                await _droneService.DeleteDrone(droneId);
                return Ok();
            }
            catch (NotFoundException e)
            {
                return Ok(new
                {
                    error = e.Message
                });
            }            
        }        

        [HttpGet("CheckAvailableForLoading")]
        public async Task<ActionResult<IEnumerable<DroneDto>>> GetAvailableDronesForLoading()
        {
            var result = await _droneService.GetAvailableDronesForLoading();
            return Ok(result);
        }

        [HttpGet("{droneId}/CheckBatteryLevel")]
        public async Task<ActionResult<int>> GetBatteryLevel(int droneId)
        {
            try
            {
                var result = await _droneService.GetBatteryLevel(droneId);
                return Ok(result);
            }
            catch (NotFoundException e)
            {
                return Ok(new
                {
                    error = e.Message
                });
            }            
        }
    }
}

