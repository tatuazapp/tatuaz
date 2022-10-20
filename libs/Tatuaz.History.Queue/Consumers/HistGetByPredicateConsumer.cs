using MassTransit;
using Tatuaz.History.DataAccess.Services;
using Tatuaz.History.Queue.Contracts;
using Tatuaz.Shared.Domain.Entities.Hist.Common;

namespace Tatuaz.History.Queue.Consumers;

public class HistGetByPredicateConsumer<TEntity, TId>
    : IConsumer<HistGetByPredicateOrder<TEntity, TId>>
    where TEntity : HistEntity<TId>
    where TId : notnull
{
    private readonly IHistorySearcherService<TEntity, TId> _historySearcherService;

    public HistGetByPredicateConsumer(IHistorySearcherService<TEntity, TId> historySearcherService)
    {
        _historySearcherService = historySearcherService;
    }

    public async Task Consume(ConsumeContext<HistGetByPredicateOrder<TEntity, TId>> context)
    {
        await context
            .RespondAsync(
                new HistGetByPredicateResult<TEntity, TId>(
                    await _historySearcherService
                        .GetByPredicateAsync(
                            context.Message.Predicate,
                            context.Message.OrderBy,
                            context.Message.AsOf,
                            context.CancellationToken
                        )
                        .ConfigureAwait(false)
                )
            )
            .ConfigureAwait(false);
    }
}
