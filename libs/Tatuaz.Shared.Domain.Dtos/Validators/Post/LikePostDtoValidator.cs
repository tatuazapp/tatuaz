using FluentValidation;
using Tatuaz.Shared.Domain.Dtos.Dtos.Post;
using Tatuaz.Shared.Pipeline.Factories.ErrorCodes.Post;

namespace Tatuaz.Shared.Domain.Dtos.Validators.Post;

public class LikePostDtoValidator : AbstractValidator<LikePostDto>
{
    public LikePostDtoValidator()
    {
        RuleFor(x => x.Like)
            .NotNull()
            .WithErrorCode(LikePostErrorCodes.LikeIsNull)
            .WithMessage("Like cannot be null");

        RuleFor(x => x.PostId)
            .NotNull()
            .WithErrorCode(LikePostErrorCodes.PostIdIsNull)
            .WithMessage("PostId cannot be null");
    }
}
