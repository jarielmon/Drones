using Drones.Domain.DTOs;
using Drones.Domain.Exceptions;
using Drones.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Drones.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DroneMedicationController : ControllerBase
    {
        private readonly IDroneMedicationService _droneMedicationService;

        public DroneMedicationController(IDroneMedicationService droneMedicationService)
        {
            _droneMedicationService = droneMedicationService;
        }

        [HttpGet]       
        public async Task<ActionResult<IEnumerable<DroneMedicationDto>>> Get()
        {
            var result = await _droneMedicationService.GetDroneMedications();
            return Ok(result);
        }

        [HttpGet("{droneId}/{medicationId}")]
        public async Task<ActionResult<DroneMedicationDto>> Get(int droneId, int medicationId)
        {
            var result = await _droneMedicationService.GetDroneMedication(droneId, medicationId);
            return Ok(result);
        }

        [HttpPost]       
        public async Task<ActionResult<DroneMedicationDto>> Post([FromBody] DroneMedicationDto droneMedicationDto)
        {
            try
            {
                var result = await _droneMedicationService.AddDroneMedication(droneMedicationDto);
                return Ok(result);
            }
            catch (NotFoundException e)
            {
                return Ok(new
                {
                    error = e.Message
                });
            }
            catch (ConflictException e)
            {
                return Ok(new
                {
                    error = e.Message
                });
            }            
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] DroneMedicationDto droneMedicationDto)
        {
            try
            {
                await _droneMedicationService.UpdateDroneMedication(droneMedicationDto);
                return Ok();
            }
            catch (NotFoundException e)
            {
                return Ok(new
                {
                    error = e.Message
                });
            }
            catch (ConflictException e)
            {
                return Ok(new
                {
                    error = e.Message
                });
            }
        }

        [HttpDelete("{droneId}/{medicationId}")]
        public async Task<IActionResult> Delete(int droneId, int medicationId)
        {
            try
            {
                await _droneMedicationService.DeleteDroneMedication(droneId, medicationId);
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

        [HttpGet("{droneId}/CheckLoadedMedicationItems")]
        public async Task<ActionResult<IEnumerable<MedicationDto>>> GetLoadedMedicationItems(int droneId)
        {
            try
            {
                var result = await _droneMedicationService.GetLoadedMedicationItems(droneId);
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

