using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Tatuaz.Dashboard.Queue.Contracts.Post;
using Tatuaz.Dashboard.Queue.Producers.Post;
using Tatuaz.Gateway.Requests.Queries.Posts;
using Tatuaz.Shared.Domain.Dtos.Dtos.Post;
using Tatuaz.Shared.Domain.Dtos.Dtos.Post.GetPostDetails;
using Tatuaz.Shared.Pipeline.Factories.Results;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Gateway.Handlers.Queries.Posts;

public class GetPostDetailsQueryHandler
    : IRequestHandler<GetPostDetailsQuery, TatuazResult<PostDetailsDto>>
{
    private readonly IValidator<GetPostDetailsDto> _validator;
    private readonly GetPostDetailsProducer _producer;

    public GetPostDetailsQueryHandler(
        IValidator<GetPostDetailsDto> validator,
        GetPostDetailsProducer producer
    )
    {
        _validator = validator;
        _producer = producer;
    }

    public async Task<TatuazResult<PostDetailsDto>> Handle(
        GetPostDetailsQuery request,
        CancellationToken cancellationToken
    )
    {
        var validationResult = await _validator
            .ValidateAsync(request.GetPostDetailsDto, cancellationToken)
            .ConfigureAwait(false);

        if (!validationResult.IsValid)
        {
            return CommonResultFactory.ValidationError<PostDetailsDto>(validationResult);
        }

        return await _producer
            .Send(new GetPostDetails(request.GetPostDetailsDto.PostId), cancellationToken)
            .ConfigureAwait(false);
    }
}
