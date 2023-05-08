using MassTransit;
using Microsoft.Extensions.Logging;
using Tatuaz.Dashboard.Queue.Contracts.Identity;
using Tatuaz.Shared.Domain.Dtos.Dtos.Common;
using Tatuaz.Shared.Pipeline.Queues;

namespace Tatuaz.Dashboard.Queue.Producers.Identity;

public class SetBioProducer : TatuazProducerBase<SetBio, EmptyDto>
{
    public SetBioProducer(IRequestClient<SetBio> requestClient, ILogger<SetBioProducer> logger)
        : base(requestClient, logger) { }
}
