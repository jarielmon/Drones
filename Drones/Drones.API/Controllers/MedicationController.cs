using Drones.Domain.DTOs;
using Drones.Domain.Exceptions;
using Drones.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Drones.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicationController : ControllerBase
    {
        private readonly IMedicationService _medicationService;

        public MedicationController(IMedicationService medicationService)
        {
            _medicationService = medicationService;
        }

        [HttpGet]       
        public async Task<ActionResult<IEnumerable<MedicationDto>>> Get()
        {
            var result = await _medicationService.GetMedications();
            return Ok(result);
        }

        [HttpGet("{medicationId}")]
        public async Task<ActionResult<MedicationDto>> Get(int medicationId)
        {
            try
            {
                var result = await _medicationService.GetMedication(medicationId);
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
        public async Task<ActionResult<MedicationDto>> Post([FromBody] MedicationDto medicationDto)
        {
            var result = await _medicationService.AddMedication(medicationDto);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] MedicationDto medicationDto)
        {
            try
            {
                await _medicationService.UpdateMedication(medicationDto);
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

        [HttpDelete("{medicationId}")]
        public async Task<IActionResult> Delete(int medicationId)
        {
            try
            {
                await _medicationService.DeleteMedication(medicationId);
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
    }
}

