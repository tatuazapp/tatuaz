using MassTransit;
using Microsoft.Extensions.Logging;
using Tatuaz.Dashboard.Queue.Contracts.Identity;
using Tatuaz.Shared.Domain.Dtos.Dtos.Common;
using Tatuaz.Shared.Pipeline.Queues;

namespace Tatuaz.Dashboard.Queue.Producers.Identity;

public class SetAccountTypeProducer : TatuazProducerBase<SetAccountType, EmptyDto>
{
    public SetAccountTypeProducer(
        IRequestClient<SetAccountType> requestClient,
        ILogger<SetAccountTypeProducer> logger
    )
        : base(requestClient, logger) { }
}
