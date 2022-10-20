using MassTransit;
using Tatuaz.History.DataAccess.Services;
using Tatuaz.History.Queue.Contracts;
using Tatuaz.Shared.Domain.Entities.Hist.Common;

namespace Tatuaz.History.Queue.Consumers;

public class HistExistsByPredicateConsumer<TEntity, TId>
    : IConsumer<HistExistsByPredicateOrder<TEntity, TId>>
    where TEntity : HistEntity<TId>
    where TId : notnull
{
    private readonly IHistorySearcherService<TEntity, TId> _historySearcherService;

    public HistExistsByPredicateConsumer(
        IHistorySearcherService<TEntity, TId> historySearcherService
    )
    {
        _historySearcherService = historySearcherService;
    }

    public async Task Consume(ConsumeContext<HistExistsByPredicateOrder<TEntity, TId>> context)
    {
        await context
            .RespondAsync(
                await _historySearcherService
                    .ExistsByPredicateAsync(context.Message.Predicate, context.Message.AsOf)
                    .ConfigureAwait(false)
            )
            .ConfigureAwait(false);
    }
}
