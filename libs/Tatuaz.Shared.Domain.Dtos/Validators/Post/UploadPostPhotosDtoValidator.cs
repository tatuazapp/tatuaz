using System.Linq;
using FluentValidation;
using Tatuaz.Shared.Domain.Dtos.Dtos.Post;
using Tatuaz.Shared.Pipeline.Factories.ErrorCodes.Post;

namespace Tatuaz.Shared.Domain.Dtos.Validators.Post;

public class UploadPostPhotosDtoValidator : AbstractValidator<UploadPostPhotosDto>
{
    public UploadPostPhotosDtoValidator()
    {
        RuleFor(x => x.Photos)
            .NotNull()
            .WithErrorCode(UploadPostPhotosErrorCodes.PhotosNull)
            .WithMessage("Photos cannot be null");

        RuleFor(x => x.Photos)
            .Must(x => x.Length <= 5)
            // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
            .When(x => x.Photos != null)
            .WithErrorCode(UploadPostPhotosErrorCodes.TooManyPhotos)
            .WithMessage("Too many photos");

        RuleForEach(x => x.Photos)
            .NotNull()
            .WithErrorCode(UploadPostPhotosErrorCodes.PhotoNull)
            .WithMessage("Photo cannot be null");

        RuleForEach(x => x.Photos)
            .Must(file =>
            {
                var length = file?.Length ?? 0;
                return length <= 5 * 1024 * 1024;
            })
            .WithErrorCode(UploadPostPhotosErrorCodes.PhotoTooLarge)
            .WithMessage(
                (dto, file) => $"Photo {dto.Photos.ToList().IndexOf(file) + 1} is too large"
            );
    }
}
