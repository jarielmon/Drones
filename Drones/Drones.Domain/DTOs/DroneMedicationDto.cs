using Drones.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drones.Domain.DTOs
{
    public class DroneMedicationDto
    {      
        public int DroneId { get; set; }
        public int MedicationId { get; set; }         
        public double Count { get; set; }                
    }
}
