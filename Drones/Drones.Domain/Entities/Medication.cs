using System;

namespace Drones.Domain.Entities;

public class Medication : BaseEntity
{
    public string Name { get; set; }
    public double Weight { get; set; }
    public string Code { get; set; }
    public string Image { get; set; }
    public ICollection<DroneMedication> DroneMedications { get; set; }
}
