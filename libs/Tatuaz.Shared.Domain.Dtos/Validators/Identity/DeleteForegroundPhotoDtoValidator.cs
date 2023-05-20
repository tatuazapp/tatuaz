using FluentValidation;
using Tatuaz.Shared.Domain.Dtos.Dtos.Identity;

namespace Tatuaz.Shared.Domain.Dtos.Validators.Identity;

public class DeleteForegroundDtoValidator : AbstractValidator<DeleteForegroundPhotoDto>
{
    public DeleteForegroundDtoValidator() { }
}
