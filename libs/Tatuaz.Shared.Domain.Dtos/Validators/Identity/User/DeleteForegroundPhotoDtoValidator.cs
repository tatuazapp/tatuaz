using FluentValidation;
using Tatuaz.Shared.Domain.Dtos.Dtos.Identity.User;

namespace Tatuaz.Shared.Domain.Dtos.Validators.Identity.User;

public class DeleteForegroundDtoValidator : AbstractValidator<DeleteForegroundPhotoDto>
{
    public DeleteForegroundDtoValidator() { }
}
