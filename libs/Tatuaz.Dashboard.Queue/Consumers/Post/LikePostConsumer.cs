using System;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Tatuaz.Dashboard.Queue.Contracts.Post;
using Tatuaz.Shared.Domain.Dtos.Dtos.Common;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Post;
using Tatuaz.Shared.Domain.Entities.Models.Post;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;
using Tatuaz.Shared.Infrastructure.Specification;
using Tatuaz.Shared.Pipeline.Factories.Results;
using Tatuaz.Shared.Pipeline.Factories.Results.Post;
using Tatuaz.Shared.Pipeline.Messages;
using Tatuaz.Shared.Pipeline.Queues;
using Tatuaz.Shared.Pipeline.UserContext;

namespace Tatuaz.Dashboard.Queue.Consumers.Post;

public class LikePostConsumer : TatuazConsumerBase<LikePost, EmptyDto>
{
    private readonly IGenericRepository<
        Shared.Domain.Entities.Models.Post.Post,
        HistPost,
        Guid
    > _postRepository;
    private readonly IGenericRepository<PostLike, HistPostLike, Guid> _postLikeRepository;
    private readonly IUserContext _userContext;
    private readonly IUnitOfWork _unitOfWork;

    public LikePostConsumer(
        ILogger<LikePostConsumer> logger,
        IGenericRepository<Shared.Domain.Entities.Models.Post.Post, HistPost, Guid> postRepository,
        IGenericRepository<PostLike, HistPostLike, Guid> postLikeRepository,
        IUserContext userContext,
        IUnitOfWork unitOfWork
    )
        : base(logger)
    {
        _postRepository = postRepository;
        _postLikeRepository = postLikeRepository;
        _userContext = userContext;
        _unitOfWork = unitOfWork;
    }

    protected override async Task<TatuazResult<EmptyDto>> ConsumeMessage(
        ConsumeContext<LikePost> context
    )
    {
        var postExists = await _postRepository.ExistsByIdAsync(context.Message.PostId);
        if (!postExists)
        {
            return LikePostResultFactory.PostNotFound<EmptyDto>();
        }

        if (context.Message.Like)
        {
            var likeExists = await _postLikeRepository.ExistsByPredicateAsync(
                x =>
                    x.PostId == context.Message.PostId
                    && x.UserId == _userContext.RequiredCurrentUserEmail()
            );
            if (likeExists)
            {
                return LikePostResultFactory.PostAlreadyLiked<EmptyDto>();
            }

            var postLike = new PostLike
            {
                PostId = context.Message.PostId,
                UserId = _userContext.RequiredCurrentUserEmail()
            };

            _postLikeRepository.Create(postLike);

            await _unitOfWork.SaveChangesAsync();
        }
        else
        {
            var spec = new FullSpecification<PostLike>();
            spec.AddFilter(
                x =>
                    x.PostId == context.Message.PostId
                    && x.UserId == _userContext.RequiredCurrentUserEmail()
            );

            var postLike = (
                await _postLikeRepository.GetBySpecificationAsync(spec).ConfigureAwait(false)
            ).FirstOrDefault();

            if (postLike == null)
            {
                return LikePostResultFactory.PostAlreadyUnliked<EmptyDto>();
            }

            _postLikeRepository.Delete(postLike);

            await _unitOfWork.SaveChangesAsync();
        }

        return CommonResultFactory.Ok(new EmptyDto());
    }
}
