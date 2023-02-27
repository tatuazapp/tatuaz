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

public class GetRegisteredUserCountQueryHandler : IRequestHandler<GetRegisteredUserCountQuery,
    TatuazResult<RegisteredUserCountDto>>
{
    private readonly GetRegisteredUserCountProducer _getRegisteredUserCountProducer;

    public GetRegisteredUserCountQueryHandler(GetRegisteredUserCountProducer getRegisteredUserCountProducer)
    {
        _getRegisteredUserCountProducer = getRegisteredUserCountProducer;
    }

    public async Task<TatuazResult<RegisteredUserCountDto>> Handle(
        GetRegisteredUserCountQuery request,
        CancellationToken cancellationToken)
    {
        return await _getRegisteredUserCountProducer
            .Send(
                new GetRegisteredUserCountOrder(),
                cancellationToken
            )
            .ConfigureAwait(false);
    }
}
