using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Tatuaz.Dashboard.Queue.Contracts.Post;
using Tatuaz.Dashboard.Queue.Producers.Post;
using Tatuaz.Gateway.Requests.Queries.Posts;
using Tatuaz.Shared.Domain.Dtos.Dtos.Post;
using Tatuaz.Shared.Infrastructure.Abstractions.Paging;
using Tatuaz.Shared.Pipeline.Factories.Results;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Gateway.Handlers.Queries.Posts;

public class GetPostFeedQueryHandler
    : IRequestHandler<GetPostFeedQuery, TatuazResult<PagedData<BriefPostDto>>>
{
    private readonly IValidator<GetPostFeedDto> _validator;
    private readonly GetPostFeedProducer _producer;

    public GetPostFeedQueryHandler(
        IValidator<GetPostFeedDto> validator,
        GetPostFeedProducer producer
    )
    {
        _validator = validator;
        _producer = producer;
    }

    public async Task<TatuazResult<PagedData<BriefPostDto>>> Handle(
        GetPostFeedQuery request,
        CancellationToken cancellationToken
    )
    {
        var validationResult = await _validator
            .ValidateAsync(request.GetPostFeedDto, cancellationToken)
            .ConfigureAwait(false);

        if (!validationResult.IsValid)
        {
            return CommonResultFactory.ValidationError<PagedData<BriefPostDto>>(validationResult);
        }

        return await _producer
            .Send(
                new GetPostFeed(
                    request.GetPostFeedDto.PageNumber!.Value,
                    request.GetPostFeedDto.PageSize!.Value,
                    request.GetPostFeedDto.SearchPostsFlag
                        is SearchPostsFlag.All
                            or SearchPostsFlag.OnlyPosts,
                    request.GetPostFeedDto.SearchPostsFlag
                        is SearchPostsFlag.All
                            or SearchPostsFlag.OnlyPhotos
                ),
                cancellationToken
            )
            .ConfigureAwait(false);
    }
}
