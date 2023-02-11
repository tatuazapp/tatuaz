using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Tatuaz.Dashboard.Queue.Contracts.Photo;
using Tatuaz.Shared.Domain.Dtos.Dtos.Photo.PhotoCategory;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Photo;
using Tatuaz.Shared.Domain.Entities.Models.Photo;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;
using Tatuaz.Shared.Infrastructure.Abstractions.Paging;
using Tatuaz.Shared.Infrastructure.Specification;
using Tatuaz.Shared.Pipeline.Factories.Results;
using Tatuaz.Shared.Pipeline.Messages;
using Tatuaz.Shared.Pipeline.Queues;

namespace Tatuaz.Dashboard.Queue.Consumers.Photo;

public class ListPhotoCategoriesConsumer
    : TatuazConsumerBase<ListPhotoCategoriesOrder, PagedData<PhotoCategoryDto>>
{
    private readonly IGenericRepository<
        PhotoCategory,
        HistPhotoCategory,
        int
    > _photoCategoryRepository;

    public ListPhotoCategoriesConsumer(
        ILogger<ListPhotoCategoriesConsumer> logger,
        IGenericRepository<PhotoCategory, HistPhotoCategory, int> photoCategoryRepository
    )
        : base(logger)
    {
        _photoCategoryRepository = photoCategoryRepository;
    }

    protected override async Task<TatuazResult<PagedData<PhotoCategoryDto>>> ConsumeMessage(
        ConsumeContext<ListPhotoCategoriesOrder> context
    )
    {
        var spec = new FullSpecification<PhotoCategory>();
        spec.AddOrder(x => x.Popularity, OrderDirection.Descending);
        spec.AddOrder(x => x.Id);

        return CommonResultFactory.Ok(
            await _photoCategoryRepository
                .GetBySpecificationWithPagingAsync<PhotoCategoryDto>(
                    spec,
                    new PagedParams(context.Message.PageNumber, context.Message.PageSize),
                    context.CancellationToken
                )
                .ConfigureAwait(false)
        );
    }
}
