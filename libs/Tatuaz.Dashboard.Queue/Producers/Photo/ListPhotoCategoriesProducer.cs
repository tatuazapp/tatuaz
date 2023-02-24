using MassTransit;
using Microsoft.Extensions.Logging;
using Tatuaz.Dashboard.Queue.Contracts.Photo;
using Tatuaz.Shared.Domain.Dtos.Dtos.Photo.PhotoCategory;
using Tatuaz.Shared.Infrastructure.Abstractions.Paging;
using Tatuaz.Shared.Pipeline.Queues;

namespace Tatuaz.Dashboard.Queue.Producers.Photo;

public class ListPhotoCategoriesProducer
    : TatuazProducerBase<ListPhotoCategories, PagedData<PhotoCategoryDto>>
{
    public ListPhotoCategoriesProducer(
        IRequestClient<ListPhotoCategories> requestClient,
        ILogger<ListPhotoCategoriesProducer> logger
    )
        : base(requestClient, logger) { }
}
