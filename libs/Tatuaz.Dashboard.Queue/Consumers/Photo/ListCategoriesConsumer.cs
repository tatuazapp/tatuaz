using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Tatuaz.Dashboard.Queue.Contracts.Photo;
using Tatuaz.Shared.Domain.Dtos.Dtos.Photo.Category;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Photo;
using Tatuaz.Shared.Domain.Entities.Models.Photo;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;
using Tatuaz.Shared.Infrastructure.Abstractions.Paging;
using Tatuaz.Shared.Infrastructure.Specification;
using Tatuaz.Shared.Pipeline.Factories.Results;
using Tatuaz.Shared.Pipeline.Messages;
using Tatuaz.Shared.Pipeline.Queues;

namespace Tatuaz.Dashboard.Queue.Consumers.Photo;

public class ListCategoriesConsumer : TatuazConsumerBase<ListCategories, PagedData<CategoryDto>>
{
    private readonly IGenericRepository<Category, HistCategory, int> _categoryRepository;

    public ListCategoriesConsumer(
        ILogger<ListCategoriesConsumer> logger,
        IGenericRepository<Category, HistCategory, int> categoryRepository
    )
        : base(logger)
    {
        _categoryRepository = categoryRepository;
    }

    protected override async Task<TatuazResult<PagedData<CategoryDto>>> ConsumeMessage(
        ConsumeContext<ListCategories> context
    )
    {
        var spec = new FullSpecification<Category>();
        spec.AddOrder(x => x.UserCategories.Count, OrderDirection.Descending);
        spec.AddOrder(x => x.Id);

        return CommonResultFactory.Ok(
            await _categoryRepository
                .GetBySpecificationWithPagingAsync<CategoryDto>(
                    spec,
                    new PagedParams(context.Message.PageNumber, context.Message.PageSize),
                    context.CancellationToken
                )
                .ConfigureAwait(false)
        );
    }
}
