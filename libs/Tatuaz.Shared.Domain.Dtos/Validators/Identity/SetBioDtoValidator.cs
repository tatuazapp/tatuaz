using FluentValidation;
using Tatuaz.Shared.Domain.Dtos.Dtos.Identity;
using Tatuaz.Shared.Pipeline.Factories.ErrorCodes.Identity;

namespace Tatuaz.Shared.Domain.Dtos.Validators.Identity;

public class SetBioDtoValidator : AbstractValidator<SetBioDto>
{
    public SetBioDtoValidator()
    {
        RuleFor(x => x.Bio)
            .MaximumLength(4096)
            .WithErrorCode(AddBioErrorCodes.BioTooLong)
            .WithMessage("Bio must not be longer than 4096 characters");

        RuleFor(x => x.City)
            .MaximumLength(64)
            .WithErrorCode(AddBioErrorCodes.CityTooLong)
            .WithMessage("City must not be longer than 64 characters");
    }
}
