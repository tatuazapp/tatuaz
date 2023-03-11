using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tatuaz.Dashboard.Queue.Contracts.Photo;
using Tatuaz.Dashboard.Queue.Contracts.Statistics;
using Tatuaz.Gateway.Requests.Queries.Statistics;
using Tatuaz.Shared.Domain.Dtos.Dtos.Statistics;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Gateway.Handlers.Queries.Statistics;

public class GetRegisteredStatsQueryHandler : IRequestHandler<GetRegisteredStatsQuery,
    TatuazResult<RegisteredStatsDto>>
{
    private readonly GetRegisteredStatsProducer _getRegisteredStatsProducer;

    public GetRegisteredStatsQueryHandler(GetRegisteredStatsProducer getRegisteredStatsProducer)
    {
        _getRegisteredStatsProducer = getRegisteredStatsProducer;
    }

    public async Task<TatuazResult<RegisteredStatsDto>> Handle(
        GetRegisteredStatsQuery request,
        CancellationToken cancellationToken)
    {
        return await _getRegisteredStatsProducer
            .Send(
                new GetRegisteredStats(),
                cancellationToken
            )
            .ConfigureAwait(false);
    }
}
