using MassTransit;
using Microsoft.Extensions.Logging;
using Tatuaz.Dashboard.Queue.Contracts.Photo;
using Tatuaz.Shared.Domain.Dtos.Dtos.Photo.Category;
using Tatuaz.Shared.Infrastructure.Abstractions.Paging;
using Tatuaz.Shared.Pipeline.Queues;

namespace Tatuaz.Dashboard.Queue.Producers.Photo;

public class ListCategoriesProducer : TatuazProducerBase<ListCategories, PagedData<CategoryDto>>
{
    public ListCategoriesProducer(
        IRequestClient<ListCategories> requestClient,
        ILogger<ListCategoriesProducer> logger
    )
        : base(requestClient, logger) { }
}
