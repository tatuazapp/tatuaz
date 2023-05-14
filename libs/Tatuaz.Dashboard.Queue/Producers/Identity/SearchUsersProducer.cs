using MassTransit;
using Microsoft.Extensions.Logging;
using Tatuaz.Dashboard.Queue.Contracts.Identity;
using Tatuaz.Shared.Domain.Dtos.Dtos.Identity;
using Tatuaz.Shared.Infrastructure.Abstractions.Paging;
using Tatuaz.Shared.Pipeline.Queues;

namespace Tatuaz.Dashboard.Queue.Producers.Identity;

public class SearchUsersProducer : TatuazProducerBase<SearchUsers, PagedData<BriefUserDto>>
{
    public SearchUsersProducer(IRequestClient<SearchUsers> requestClient, ILogger<SearchUsersProducer> logger) : base(requestClient, logger)
    {
    }
}
