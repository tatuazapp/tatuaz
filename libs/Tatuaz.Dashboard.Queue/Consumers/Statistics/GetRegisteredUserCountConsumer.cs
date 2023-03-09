using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Tatuaz.Dashboard.Queue.Contracts.Statistics;
using Tatuaz.Shared.Domain.Dtos.Dtos.Statistics;
using Tatuaz.Shared.Pipeline.Factories.Results;
using Tatuaz.Shared.Pipeline.Messages;
using Tatuaz.Shared.Pipeline.Queues;

namespace Tatuaz.Dashboard.Queue.Consumers.Statistics;

public class GetRegisteredStatsConsumer : TatuazConsumerBase<GetRegisteredStatsOrder, RegisteredStatsDto>
{
    public GetRegisteredStatsConsumer(ILogger<GetRegisteredStatsConsumer> logger) : base(logger)
    {
    }

    protected override Task<TatuazResult<RegisteredStatsDto>> ConsumeMessage(
        ConsumeContext<GetRegisteredStatsOrder> context)
    {
        var result = new RegisteredStatsDto(10, 20, 500);

        return Task.FromResult(CommonResultFactory.Ok<RegisteredStatsDto>(result));
    }
}
