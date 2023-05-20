using System;
using FluentValidation;
using Tatuaz.Shared.Domain.Dtos.Dtos.Comment;
using Tatuaz.Shared.Domain.Dtos.Dtos.Post;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Post;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;
using Tatuaz.Shared.Pipeline.Factories.ErrorCodes.Comment;

namespace Tatuaz.Shared.Domain.Dtos.Validators.Comment;

public class SubmitCommentValidator : AbstractValidator<SubmitCommentDto>
{
    public SubmitCommentValidator(
        IGenericRepository<Entities.Models.Post.Post, HistPost, Guid> postRepository,
        IGenericRepository<Entities.Models.Post.Comment, HistComment, Guid> commentRepository
    )
    {
        RuleFor(x => x.Content)
            .NotNull()
            .WithErrorCode(SubmitCommentErrorCodes.ContentIsNull)
            .WithMessage("Content cannot be null.");

        RuleFor(x => x.Content)
            .MaximumLength(4096)
            .WithErrorCode(SubmitCommentErrorCodes.ContentIsTooLong)
            .WithMessage("Content cannot be longer than 4096 characters.");

        RuleFor(x => x.PostId)
            .NotNull()
            .WithErrorCode(SubmitCommentErrorCodes.PostIsNull)
            .WithMessage("PostId cannot be null.");
        RuleFor(x => x.PostId)
            .MustAsync(
                async (postId, ct) =>
                    await postRepository.ExistsByIdAsync(postId, ct).ConfigureAwait(false)
            )
            .WithErrorCode(SubmitCommentErrorCodes.PostNotFound)
            .WithMessage("Post with given id not found.");

        RuleFor(x => x.ParentCommentId)
            .MustAsync(
                async (parentCommentId, ct) =>
                    await commentRepository
                        .ExistsByIdAsync(parentCommentId.Value, ct)
                        .ConfigureAwait(false)
            )
            .When(x => x.ParentCommentId.HasValue)
            .WithErrorCode(SubmitCommentErrorCodes.ParentCommentNotFound)
            .WithMessage("Parent comment with given id not found.");

        RuleFor(x => x.PostId)
            .MustAsync(
                async (dto, postId, ct) =>
                {
                    return await commentRepository
                        .ExistsByPredicateAsync(
                            x => x.Id == dto.ParentCommentId && x.PostId == postId,
                            ct
                        )
                        .ConfigureAwait(false);
                }
            )
            .When(x => x.ParentCommentId.HasValue)
            .WithErrorCode(SubmitCommentErrorCodes.ParentCommentNotFound)
            .WithMessage("Parent comment is not under parent post.");
    }
}
