using MassTransit;
using Microsoft.Extensions.Logging;
using Tatuaz.Dashboard.Queue.Contracts.Identity;
using Tatuaz.Shared.Domain.Dtos.Dtos.Identity.User;
using Tatuaz.Shared.Pipeline.Queues;

namespace Tatuaz.Dashboard.Queue.Producers.Identity;

public class GetUserProducer : TatuazProducerBase<GetUser, UserDto>
{
    public GetUserProducer(IRequestClient<GetUser> requestClient, ILogger<GetUserProducer> logger)
        : base(requestClient, logger) { }
}
