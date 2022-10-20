using AutoMapper;
using Drones.Domain.DTOs;
using Drones.Domain.Entities;

namespace Drones.Infrastructure.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<DroneDto, Drone>().ReverseMap();
        CreateMap<MedicationDto, Medication>().ReverseMap();        
        CreateMap<DroneMedicationDto, DroneMedication>().ReverseMap();
    }
}
