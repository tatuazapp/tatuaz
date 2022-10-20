using MassTransit;
using Tatuaz.History.DataAccess.Services;
using Tatuaz.History.Queue.Contracts;
using Tatuaz.Shared.Domain.Entities.Hist.Common;

namespace Tatuaz.History.Queue.Consumers;

public class HistCountByPredicateConsumer<TEntity, TId>
    : IConsumer<HistCountByPredicateOrder<TEntity, TId>>
    where TEntity : HistEntity<TId>
    where TId : notnull
{
    private readonly IHistorySearcherService<TEntity, TId> _historySearcherService;

    public HistCountByPredicateConsumer(
        IHistorySearcherService<TEntity, TId> historySearcherService
    )
    {
        _historySearcherService = historySearcherService;
    }

    public async Task Consume(ConsumeContext<HistCountByPredicateOrder<TEntity, TId>> context)
    {
        await context
            .RespondAsync(
                await _historySearcherService
                    .CountByPredicateAsync(context.Message.Predicate, context.Message.AsOf)
                    .ConfigureAwait(false)
            )
            .ConfigureAwait(false);
    }
}
