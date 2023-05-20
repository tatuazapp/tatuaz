using FluentValidation;
using Tatuaz.Shared.Domain.Dtos.Dtos.Identity;
using Tatuaz.Shared.Pipeline.Factories.ErrorCodes.Identity;

namespace Tatuaz.Shared.Domain.Dtos.Validators.Identity;

public class SetAccountTypeDtoValidator : AbstractValidator<SetAccountTypeDto>
{
    public SetAccountTypeDtoValidator()
    {
        RuleFor(x => x.Artist)
            .NotNull()
            .WithErrorCode(SetAccountTypeErrorCodes.ArtistNull)
            .WithMessage("Artist must not be null");
    }
}
