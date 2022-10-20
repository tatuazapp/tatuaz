using MassTransit;
using Tatuaz.History.DataAccess.Services;
using Tatuaz.History.Queue.Contracts;
using Tatuaz.Shared.Domain.Entities.Hist.Common;

namespace Tatuaz.History.Queue.Consumers;

public class HistGetByPredicateWithPagingConsumer<TEntity, TId>
    : IConsumer<HistGetByPredicateWithPagingOrder<TEntity, TId>>
    where TEntity : HistEntity<TId>
    where TId : notnull
{
    private readonly IHistorySearcherService<TEntity, TId> _historySearcherService;

    public HistGetByPredicateWithPagingConsumer(
        IHistorySearcherService<TEntity, TId> historySearcherService
    )
    {
        _historySearcherService = historySearcherService;
    }

    public async Task Consume(
        ConsumeContext<HistGetByPredicateWithPagingOrder<TEntity, TId>> context
    )
    {
        await context
            .RespondAsync(
                new HistGetByPredicateWithPagingResult<TEntity, TId>(
                    await _historySearcherService
                        .GetByPredicateWithPagingAsync(
                            context.Message.Predicate,
                            context.Message.OrderBy,
                            context.Message.PagedParams,
                            context.Message.AsOf,
                            context.CancellationToken
                        )
                        .ConfigureAwait(false)
                )
            )
            .ConfigureAwait(false);
    }
}
