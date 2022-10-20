using FluentValidation;
using Drones.Domain.DTOs;

namespace Drones.Infrastructure.Validators;

public class DroneValidator : AbstractValidator<DroneDto>
{
    public DroneValidator()
    {
        RuleFor(x => x.SerialNumber).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Model).NotNull();
        RuleFor(x => x.WeightLimit).NotNull().LessThanOrEqualTo(500);
        RuleFor(x => x.BatteryCapacity).NotNull().LessThanOrEqualTo(100);
        RuleFor(x => x.State).NotNull();
    }
}
