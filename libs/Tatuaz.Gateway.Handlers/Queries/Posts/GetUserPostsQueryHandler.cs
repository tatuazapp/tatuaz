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

public class GetUserPostsQueryHandler
    : IRequestHandler<GetUserPostsQuery, TatuazResult<PagedData<BriefPostDto>>>
{
    private readonly IValidator<GetUserPostsDto> _validator;
    private readonly GetUserPostsProducer _producer;

    public GetUserPostsQueryHandler(
        IValidator<GetUserPostsDto> validator,
        GetUserPostsProducer producer
    )
    {
        _validator = validator;
        _producer = producer;
    }

    public async Task<TatuazResult<PagedData<BriefPostDto>>> Handle(
        GetUserPostsQuery request,
        CancellationToken cancellationToken
    )
    {
        var validationResult = await _validator
            .ValidateAsync(request.GetUserPostsDto, cancellationToken)
            .ConfigureAwait(false);

        if (!validationResult.IsValid)
        {
            return CommonResultFactory.ValidationError<PagedData<BriefPostDto>>(validationResult);
        }

        return await _producer
            .Send(
                new GetUserPosts(
                    request.GetUserPostsDto.Username!,
                    request.GetUserPostsDto.PageNumber!.Value,
                    request.GetUserPostsDto.PageSize!.Value
                ),
                cancellationToken
            )
            .ConfigureAwait(false);
    }
}
