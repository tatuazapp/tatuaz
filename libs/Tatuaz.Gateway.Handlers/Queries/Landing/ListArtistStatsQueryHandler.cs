using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Tatuaz.Gateway.Queue.Contracts.Landing.ListArtistStats;
using Tatuaz.Gateway.Queue.Producers.Landing.ListArtistStats;
using Tatuaz.Gateway.Requests.Queries.Landing;
using Tatuaz.Shared.Domain.Dtos.Dtos.Landing.ListArtistStats;
using Tatuaz.Shared.Domain.Dtos.Dtos.Landing.ListSummaryStats;
using Tatuaz.Shared.Domain.Dtos.Validators.Landing.ListArtistStats;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;
using Tatuaz.Shared.Pipeline.Factories.Results;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Gateway.Handlers.Queries.Landing;

public class
    ListArtistStatsQueryHandler : IRequestHandler<ListArtistStatsQuery, TatuazResult<IEnumerable<ArtistStatDto>>>
{
    private readonly ILogger<ListArtistStatsProducer> _logger;
    private readonly IRequestClient<ListArtistStatsOrder> _requestClient;
    private readonly IUserAccessor _userAccessor;

    public ListArtistStatsQueryHandler(
        ILogger<ListArtistStatsProducer> logger,
        IRequestClient<ListArtistStatsOrder> requestClient,
        IUserAccessor userAccessor)
    {
        _logger = logger;
        _requestClient = requestClient;
        _userAccessor = userAccessor;
    }

    public async Task<TatuazResult<IEnumerable<ArtistStatDto>>> Handle(ListArtistStatsQuery request,
        CancellationToken cancellationToken)
    {
        var validator = new ListArtistStatsDtoValidator();
        var validationResult =
            await validator.ValidateAsync(request.ListArtistStatDto, cancellationToken).ConfigureAwait(false);
        if (!validationResult.IsValid)
        {
            return CommonResultFactory.ValidationError<IEnumerable<ArtistStatDto>>(validationResult);
        }

        var producer = new ListArtistStatsProducer(_requestClient, _userAccessor, _logger);
        var result = await producer
            .Send(new ListArtistStatsOrder(request.ListArtistStatDto.Count), cancellationToken)
            .ConfigureAwait(false);

        if (result == null)
        {
            throw new NullReferenceException();
        }

        return result;
    }
}
