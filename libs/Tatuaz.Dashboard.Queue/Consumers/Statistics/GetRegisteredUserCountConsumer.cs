using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Tatuaz.Dashboard.Queue.Contracts.Statistics;
using Tatuaz.Shared.Domain.Dtos.Dtos.Statistics;
using Tatuaz.Shared.Pipeline.Factories.Results;
using Tatuaz.Shared.Pipeline.Messages;
using Tatuaz.Shared.Pipeline.Queues;

namespace Tatuaz.Dashboard.Queue.Consumers.Statistics;

public class GetRegisteredUserCountConsumer : TatuazConsumerBase<GetRegisteredUserCountOrder, RegisteredUserCountDto>
{
    public GetRegisteredUserCountConsumer(ILogger<GetRegisteredUserCountConsumer> logger) : base(logger)
    {
    }

    protected override Task<TatuazResult<RegisteredUserCountDto>> ConsumeMessage(
        ConsumeContext<GetRegisteredUserCountOrder> context)
    {
        var result = new RegisteredUserCountDto(10, 20, 500);

        return Task.FromResult(CommonResultFactory.Ok<RegisteredUserCountDto>(result));
    }
}
