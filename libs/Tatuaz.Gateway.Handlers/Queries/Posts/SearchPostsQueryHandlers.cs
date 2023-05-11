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

public class SearchPostsQueryHandlers
    : IRequestHandler<SearchPostsQuery, TatuazResult<PagedData<BriefPostDto>>>
{
    private readonly IValidator<SearchPostsDto> _validator;
    private readonly SearchPostsProducer _searchPostsProducer;

    public SearchPostsQueryHandlers(
        IValidator<SearchPostsDto> validator,
        SearchPostsProducer searchPostsProducer
    )
    {
        _validator = validator;
        _searchPostsProducer = searchPostsProducer;
    }

    public async Task<TatuazResult<PagedData<BriefPostDto>>> Handle(
        SearchPostsQuery request,
        CancellationToken cancellationToken
    )
    {
        var validationResult = await _validator
            .ValidateAsync(request.SearchPostsDto, cancellationToken)
            .ConfigureAwait(false);

        if (!validationResult.IsValid)
        {
            return CommonResultFactory.ValidationError<PagedData<BriefPostDto>>(validationResult);
        }

        return await _searchPostsProducer
            .Send(
                new SearchPosts(
                    request.SearchPostsDto.Query!,
                    request.SearchPostsDto.PageNumber!.Value,
                    request.SearchPostsDto.PageSize!.Value,
                    request.SearchPostsDto.SearchPostsFlag
                        is SearchPostsFlag.All
                            or SearchPostsFlag.OnlyPosts,
                    request.SearchPostsDto.SearchPostsFlag
                        is SearchPostsFlag.All
                            or SearchPostsFlag.OnlyPhotos
                ),
                cancellationToken
            )
            .ConfigureAwait(false);
    }
}
