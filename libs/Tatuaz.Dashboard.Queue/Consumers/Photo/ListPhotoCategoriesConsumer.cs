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
    : TatuazConsumerBase<ListPhotoCategories, PagedData<PhotoCategoryDto>>
{
    private readonly IGenericRepository<
        Category,
        HistCategory,
        int
    > _photoCategoryRepository;

    public ListPhotoCategoriesConsumer(
        ILogger<ListPhotoCategoriesConsumer> logger,
        IGenericRepository<Category, HistCategory, int> photoCategoryRepository
    )
        : base(logger)
    {
        _photoCategoryRepository = photoCategoryRepository;
    }

    protected override async Task<TatuazResult<PagedData<PhotoCategoryDto>>> ConsumeMessage(
        ConsumeContext<ListPhotoCategories> context
    )
    {
        var spec = new FullSpecification<Category>();
        spec.AddOrder(x => x.UserCategories.Count, OrderDirection.Descending);
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
