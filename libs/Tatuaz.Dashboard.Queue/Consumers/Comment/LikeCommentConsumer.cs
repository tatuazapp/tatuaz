using System;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Tatuaz.Dashboard.Queue.Contracts.Comment;
using Tatuaz.Shared.Domain.Dtos.Dtos.Common;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Post;
using Tatuaz.Shared.Domain.Entities.Models.Post;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;
using Tatuaz.Shared.Infrastructure.Specification;
using Tatuaz.Shared.Pipeline.Factories.Results;
using Tatuaz.Shared.Pipeline.Factories.Results.Comment;
using Tatuaz.Shared.Pipeline.Messages;
using Tatuaz.Shared.Pipeline.Queues;
using Tatuaz.Shared.Pipeline.UserContext;

namespace Tatuaz.Dashboard.Queue.Consumers.Comment;

public class LikeCommentConsumer : TatuazConsumerBase<LikeComment, EmptyDto>
{
    private readonly IGenericRepository<
        Shared.Domain.Entities.Models.Post.Comment,
        HistComment,
        Guid
    > _commentRepository;

    private readonly IGenericRepository<CommentLike, HistCommentLike, Guid> _commentLikeRepository;
    private readonly IUserContext _userContext;
    private readonly IUnitOfWork _unitOfWork;

    public LikeCommentConsumer(
        ILogger<LikeCommentConsumer> logger,
        IUserContext userContext,
        IUnitOfWork unitOfWork,
        IGenericRepository<CommentLike, HistCommentLike, Guid> commentLikeRepository,
        IGenericRepository<
            Shared.Domain.Entities.Models.Post.Comment,
            HistComment,
            Guid
        > commentRepository
    )
        : base(logger)
    {
        _userContext = userContext;
        _unitOfWork = unitOfWork;
        _commentLikeRepository = commentLikeRepository;
        _commentRepository = commentRepository;
    }

    protected override async Task<TatuazResult<EmptyDto>> ConsumeMessage(
        ConsumeContext<LikeComment> context
    )
    {
        var commentExists = await _commentRepository
            .ExistsByIdAsync(context.Message.CommentId)
            .ConfigureAwait(false);
        if (!commentExists)
        {
            return LikeCommentResultFactory.CommentNotFound<EmptyDto>();
        }

        if (context.Message.Like)
        {
            var likeExists = await _commentLikeRepository
                .ExistsByPredicateAsync(
                    x =>
                        x.CommentId == context.Message.CommentId
                        && x.UserId == _userContext.RequiredCurrentUserEmail()
                )
                .ConfigureAwait(false);
            if (likeExists)
            {
                return LikeCommentResultFactory.CommentAlreadyLiked<EmptyDto>();
            }

            var commentLike = new CommentLike
            {
                CommentId = context.Message.CommentId,
                UserId = _userContext.RequiredCurrentUserEmail()
            };

            _commentLikeRepository.Create(commentLike);

            await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);
        }
        else
        {
            var spec = new FullSpecification<CommentLike>();
            spec.AddFilter(
                x =>
                    x.CommentId == context.Message.CommentId
                    && x.UserId == _userContext.RequiredCurrentUserEmail()
            );

            var commentLike = (
                await _commentLikeRepository.GetBySpecificationAsync(spec).ConfigureAwait(false)
            ).FirstOrDefault();

            if (commentLike == null)
            {
                return LikeCommentResultFactory.CommentAlreadyUnliked<EmptyDto>();
            }

            _commentLikeRepository.Delete(commentLike);

            await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);
        }

        return CommonResultFactory.Ok(new EmptyDto());
    }
}
