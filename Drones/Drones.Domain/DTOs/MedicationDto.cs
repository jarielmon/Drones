using Drones.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drones.Domain.DTOs
{
    public class MedicationDto
    {
        public int? Id { get; set; }        
        public string Name { get; set; }       
        public double Weight { get; set; }
        public string Code { get; set; }        
        public string Image { get; set; }                
    }
}
