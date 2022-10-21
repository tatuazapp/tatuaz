using MassTransit;
using Microsoft.Extensions.Logging;
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
    private readonly ILogger<HistCountByPredicateConsumer<TEntity, TId>> _logger;

    public HistCountByPredicateConsumer(
        IHistorySearcherService<TEntity, TId> historySearcherService,
        ILogger<HistCountByPredicateConsumer<TEntity, TId>> logger
    )
    {
        _historySearcherService = historySearcherService;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<HistCountByPredicateOrder<TEntity, TId>> context)
    {
        _logger.LogInformation("HistCountByPredicateConsumer: {0}", context.Message);

        await context
            .RespondAsync(
                await _historySearcherService
                    .CountByPredicateAsync(context.Message.Predicate, context.Message.AsOf)
                    .ConfigureAwait(false)
            )
            .ConfigureAwait(false);
    }
}
