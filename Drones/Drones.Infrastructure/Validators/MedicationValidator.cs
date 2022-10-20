using FluentValidation;
using Drones.Domain.DTOs;

namespace Drones.Infrastructure.Validators;

public class MedicationValidator : AbstractValidator<MedicationDto>
{
    public MedicationValidator()
    {
        RuleFor(x => x.Name).NotEmpty().Matches(@"^[A-Za-z0-9_-]*$").WithMessage("The field allow only letters, numbers, ‘-‘, ‘_’");
        RuleFor(x => x.Weight).NotNull();
        RuleFor(x => x.Code).NotNull().Matches(@"^[A-Z0-9_]*$").WithMessage("The field allow only upper case letters, underscore and numbers");
    }
}
