using System;

namespace Drones.Domain.Entities;

public class DroneMedication : BaseEntity
{
    public long DroneId { get; set; }
    public Drone Drone { get; set; }
    public long MedicationId { get; set; }
    public Medication Medication { get; set; }
    public double Count { get; set; }
}
