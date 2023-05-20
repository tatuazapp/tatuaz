using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Tatuaz.Dashboard.Queue.Contracts.Identity;
using Tatuaz.Dashboard.Queue.Contracts.Photo;
using Tatuaz.Dashboard.Queue.Producers.Identity;
using Tatuaz.Gateway.Requests.Queries.Identity;
using Tatuaz.Shared.Domain.Dtos.Dtos.Identity;
using Tatuaz.Shared.Domain.Dtos.Dtos.Photo.Category;
using Tatuaz.Shared.Infrastructure.Abstractions.Paging;
using Tatuaz.Shared.Pipeline.Factories.Results;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Gateway.Handlers.Queries.Identity;

public class GetTopArtistsQueryHandler
    : IRequestHandler<GetTopArtistsQuery, TatuazResult<PagedData<BriefUserDto>>>
{
    private readonly IValidator<GetTopArtistsDto> _validator;
    private readonly GetTopArtistsProducer _producer;

    public GetTopArtistsQueryHandler(
        IValidator<GetTopArtistsDto> validator,
        GetTopArtistsProducer producer
    )
    {
        _validator = validator;
        _producer = producer;
    }

    public async Task<TatuazResult<PagedData<BriefUserDto>>> Handle(
        GetTopArtistsQuery request,
        CancellationToken cancellationToken
    )
    {
        var validationResult = await _validator
            .ValidateAsync(request.GetTopArtistsDto, cancellationToken)
            .ConfigureAwait(false);

        if (!validationResult.IsValid)
        {
            return CommonResultFactory.ValidationError<PagedData<BriefUserDto>>(validationResult);
        }

        return await _producer
            .Send(
                new GetTopArtists(
                    request.GetTopArtistsDto.PageNumber!.Value,
                    request.GetTopArtistsDto.PageSize!.Value
                ),
                cancellationToken
            )
            .ConfigureAwait(false);
    }
}
