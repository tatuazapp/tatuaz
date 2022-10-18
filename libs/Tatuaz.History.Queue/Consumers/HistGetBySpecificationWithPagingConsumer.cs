using MassTransit;
using Tatuaz.History.Infrastructure.Abstractions.Services;
using Tatuaz.History.Queue.Contracts;
using Tatuaz.Shared.Domain.Entities.Hist.Common;

namespace Tatuaz.History.Queue.Consumers;

public class
    HistGetBySpecificationWithPagingConsumer<TEntity, TId> : IConsumer<
        HistGetBySpecificationWithPagingOrder<TEntity, TId>>
    where TEntity : HistEntity<TId>
    where TId : notnull
{
    private readonly IHistorySearcherService<TEntity, TId> _historySearcherService;

    public HistGetBySpecificationWithPagingConsumer(IHistorySearcherService<TEntity, TId> historySearcherService)
    {
        _historySearcherService = historySearcherService;
    }

    public async Task Consume(ConsumeContext<HistGetBySpecificationWithPagingOrder<TEntity, TId>> context)
    {
        await context.RespondAsync(new HistGetBySpecificationWithPagingResult<TEntity, TId>(
            await _historySearcherService
                .GetBySpecificationWithPagingAsync(context.Message.Specification, context.Message.PagedParams,
                    context.Message.AsOf).ConfigureAwait(false))).ConfigureAwait(false);
    }
}
