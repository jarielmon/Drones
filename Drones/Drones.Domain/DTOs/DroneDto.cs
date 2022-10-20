using Drones.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drones.Domain.DTOs
{
    public class DroneDto
    {
        public int? Id { get; set; }        
        public string SerialNumber { get; set; }       
        public Model Model { get; set; }
        public double WeightLimit { get; set; }        
        public int BatteryCapacity { get; set; }        
        public State State { get; set; }
    }
}
