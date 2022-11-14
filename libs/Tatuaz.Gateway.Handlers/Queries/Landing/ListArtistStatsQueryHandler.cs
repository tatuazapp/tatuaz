using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Tatuaz.Gateway.Queue.Contracts.Landing.ListArtistStats;
using Tatuaz.Gateway.Queue.Producers.Landing.ListArtistStats;
using Tatuaz.Gateway.Requests.Queries.Landing;
using Tatuaz.Shared.Domain.Dtos.Dtos.Landing.ListArtistStats;
using Tatuaz.Shared.Pipeline.Factories.Results;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Gateway.Handlers.Queries.Landing;

public class ListArtistStatsQueryHandler
    : IRequestHandler<ListArtistStatsQuery, TatuazResult<IEnumerable<ArtistStatDto>>>
{
    private readonly ListArtistStatsProducer _listArtistStatsProducer;
    private readonly IValidator<ListArtistStatsDto> _validator;

    public ListArtistStatsQueryHandler(
        ListArtistStatsProducer listArtistStatsProducer,
        IValidator<ListArtistStatsDto> validator
    )
    {
        _listArtistStatsProducer = listArtistStatsProducer;
        _validator = validator;
    }

    public async Task<TatuazResult<IEnumerable<ArtistStatDto>>> Handle(
        ListArtistStatsQuery request,
        CancellationToken cancellationToken
    )
    {
        var validationResult = await _validator
            .ValidateAsync(request.ListArtistStatDto, cancellationToken)
            .ConfigureAwait(false);
        if (!validationResult.IsValid)
        {
            return CommonResultFactory.ValidationError<IEnumerable<ArtistStatDto>>(
                validationResult
            );
        }

        var result = await _listArtistStatsProducer
            .Send(
                new ListArtistStatsOrder(request.ListArtistStatDto.Count.Value),
                cancellationToken
            )
            .ConfigureAwait(false);

        if (result == null)
        {
            throw new NullReferenceException();
        }

        return result;
    }
}
