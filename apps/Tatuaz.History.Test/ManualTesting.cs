using MassTransit;
using Newtonsoft.Json;
using NodaTime;
using NodaTime.Serialization.JsonNet;
using Tatuaz.History.Queue;
using Tatuaz.History.Queue.Contracts;
using Tatuaz.Shared.Domain.Entities.Hist.Common;
using Tatuaz.Shared.Helpers;
using Xunit;

namespace Tatuaz.History.Test;

public class ManualTesting
{
    private readonly ISendEndpointProvider _sendEndpointProvider;

    public ManualTesting(ISendEndpointProvider sendEndpointProvider)
    {
        _sendEndpointProvider = sendEndpointProvider;
    }

    [Fact]
    public async Task Test()
    {
        var sendEndpoint = await _sendEndpointProvider
            .GetSendEndpoint(HistoryQueueConstants.DumpQueueUri)
            .ConfigureAwait(false);
        var histEntity = new HistEntity<Guid>
        {
            HistDumpedAt = Instant.FromUtc(2020, 1, 1, 0, 0),
            HistState = HistState.Added,
            Id = Guid.NewGuid()
        };
        await sendEndpoint
            .Send(
                new DumpHistoryOrder(
                    histEntity.GetType().FullName,
                    JsonConvert.SerializeObject(histEntity, SerializationUtils.GetTatuazSerializerSettings())
                )
            )
            .ConfigureAwait(false);
    }
}
