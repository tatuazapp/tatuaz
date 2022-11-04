using System.Collections.Generic;
using System.Threading.Tasks;
using Tatuaz.Gateway.Queue.Contracts;
using Tatuaz.Shared.Domain.Dtos.Dtos.Landing;
using Tatuaz.Shared.Pipeline.Messages;
using Tatuaz.Shared.Pipeline.Queues;

namespace Tatuaz.Landing.Queue.Consumers;

public class ListStatsConsumer : TatuazConsumerBase<ListStatsOrder, IEnumerable<StatDto>>
{
    protected override Task<TatuazResult<IEnumerable<StatDto>>> ConsumeMessage(ListStatsOrder message)
    {
        throw new System.NotImplementedException();
    }
}