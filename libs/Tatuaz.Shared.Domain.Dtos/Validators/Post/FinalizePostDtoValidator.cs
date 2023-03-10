using FluentValidation;
using Tatuaz.Shared.Domain.Dtos.Dtos.Post;
using Tatuaz.Shared.Pipeline.Factories.ErrorCodes.Post;

namespace Tatuaz.Shared.Domain.Dtos.Validators.Post;

public class FinalizePostDtoValidator : AbstractValidator<FinalizePostDto>
{
    public FinalizePostDtoValidator()
    {
        RuleFor(x => x.Description)
            .NotNull()
            .WithErrorCode(FinalizePostErrorCodes.DescriptionIsNull)
            .WithMessage("Description cannot be null");

        RuleFor(x => x.Description)
            .MaximumLength(4096)
            .WithErrorCode(FinalizePostErrorCodes.DescriptionIsTooLong)
            .WithMessage("Description cannot be longer than 4096 characters");
    }
}
