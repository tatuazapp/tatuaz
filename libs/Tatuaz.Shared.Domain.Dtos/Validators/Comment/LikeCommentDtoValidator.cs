using FluentValidation;
using Tatuaz.Shared.Domain.Dtos.Dtos.Comment;
using Tatuaz.Shared.Pipeline.Factories.ErrorCodes.Comment;

namespace Tatuaz.Shared.Domain.Dtos.Validators.Comment;

public class LikeCommentDtoValidator : AbstractValidator<LikeCommentDto>
{
    public LikeCommentDtoValidator()
    {
        RuleFor(x => x.Like)
            .NotNull()
            .WithErrorCode(LikeCommentErrorCodes.LikeIsNull)
            .WithMessage("Like cannot be null");

        RuleFor(x => x.CommentId)
            .NotNull()
            .WithErrorCode(LikeCommentErrorCodes.CommentIdIsNull)
            .WithMessage("CommentId cannot be null");
    }
}
