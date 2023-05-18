using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Tatuaz.Dashboard.Queue.Contracts.Post;
using Tatuaz.Shared.Domain.Dtos.Dtos.Post;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Identity;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Post;
using Tatuaz.Shared.Domain.Entities.Models.Identity;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;
using Tatuaz.Shared.Infrastructure.Abstractions.Paging;
using Tatuaz.Shared.Infrastructure.Specification;
using Tatuaz.Shared.Pipeline.Factories.Results;
using Tatuaz.Shared.Pipeline.Factories.Results.Post;
using Tatuaz.Shared.Pipeline.Messages;
using Tatuaz.Shared.Pipeline.Queues;

namespace Tatuaz.Dashboard.Queue.Consumers.Post;

public class GetUserPostsConsumer : TatuazConsumerBase<GetUserPosts, PagedData<BriefPostDto>>
{
    private readonly IGenericRepository<
        Shared.Domain.Entities.Models.Post.Post,
        HistPost,
        Guid
    > _postRepository;
    private readonly IGenericRepository<TatuazUser, HistTatuazUser, string> _userRepository;

    public GetUserPostsConsumer(
        ILogger<GetUserPostsConsumer> logger,
        IGenericRepository<Shared.Domain.Entities.Models.Post.Post, HistPost, Guid> postRepository,
        IGenericRepository<TatuazUser, HistTatuazUser, string> userRepository
    )
        : base(logger)
    {
        _postRepository = postRepository;
        _userRepository = userRepository;
    }

    protected override async Task<TatuazResult<PagedData<BriefPostDto>>> ConsumeMessage(
        ConsumeContext<GetUserPosts> context
    )
    {
        var spec = new FullSpecification<Shared.Domain.Entities.Models.Post.Post>();
        spec.AddFilter(post => post.Author.Username == context.Message.Username);
        spec.AddOrder(x => x.CreatedAt, OrderDirection.Descending);

        var userExists = await _userRepository.ExistsByPredicateAsync(
            x => x.Username == context.Message.Username
        );
        if (!userExists)
        {
            return GetUserPostsResultFactory.UserNotFound<PagedData<BriefPostDto>>();
        }

        return CommonResultFactory.Ok(
            await _postRepository
                .GetBySpecificationWithPagingAsync<BriefPostDto>(
                    spec,
                    new PagedParams(context.Message.PageNumber, context.Message.PageSize)
                )
                .ConfigureAwait(false)
        );
    }
}
