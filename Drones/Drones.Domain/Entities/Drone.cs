using System;

namespace Drones.Domain.Entities;

public class Drone : BaseEntity
{
    public string SerialNumber { get; set; }
    public Model Model { get; set; }
    public double WeightLimit { get; set; }
    public int BatteryCapacity { get; set; }
    public State State { get; set; }
    public ICollection<DroneMedication> DroneMedications { get; set; }
}
