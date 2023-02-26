using FluentValidation;
using Tatuaz.Shared.Domain.Dtos.Dtos.Identity.User;
using Tatuaz.Shared.Pipeline.Factories.ErrorCodes.Identity;

namespace Tatuaz.Shared.Domain.Dtos.Validators.Identity.User;

public class SetBackgroundPhotoDtoValidator : AbstractValidator<SetBackgroundPhotoDto>
{
    public SetBackgroundPhotoDtoValidator()
    {
        RuleFor(x => x.Photo)
            .NotNull()
            .WithErrorCode(SetBackgroundPhotoErrorCodes.FileNull)
            .WithMessage("File cannot be null");
        RuleFor(x => x.Photo)
            .Must(file =>
            {
                var length = file?.Length ?? 0;
                return length <= 5 * 1024 * 1024;
            })
            .WithErrorCode(SetBackgroundPhotoErrorCodes.FileTooLarge)
            .WithMessage("File must not be larger than 5MB");
    }
}
