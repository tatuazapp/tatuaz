using FluentValidation;
using Tatuaz.Shared.Domain.Dtos.Dtos.Identity;
using Tatuaz.Shared.Pipeline.Factories.ErrorCodes.Identity;

namespace Tatuaz.Shared.Domain.Dtos.Validators.Identity;

public class SetForegroundPhotoDtoValidator : AbstractValidator<SetForegroundPhotoDto>
{
    public SetForegroundPhotoDtoValidator()
    {
        RuleFor(x => x.Photo)
            .NotNull()
            .WithErrorCode(SetForegroundPhotoErrorCodes.FileNull)
            .WithMessage("File cannot be null");
        RuleFor(x => x.Photo)
            .Must(file =>
            {
                var length = file?.Length ?? 0;
                return length <= 2 * 1024 * 1024;
            })
            .WithErrorCode(SetForegroundPhotoErrorCodes.FileTooLarge)
            .WithMessage("File must not be larger than 2MB");
    }
}
