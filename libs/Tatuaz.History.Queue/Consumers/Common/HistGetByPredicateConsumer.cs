using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Tatuaz.History.DataAccess.Services;
using Tatuaz.History.Queue.Contracts;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;

namespace Tatuaz.History.Queue.Consumers.Common;

public class HistGetByPredicateConsumer<TEntity, TId>
    : IConsumer<HistGetByPredicateOrder<TEntity, TId>>
    where TEntity : HistEntity<TId>
    where TId : notnull
{
    private readonly IHistorySearcherService<TEntity, TId> _historySearcherService;
    private readonly ILogger<HistGetByPredicateConsumer<TEntity, TId>> _logger;

    public HistGetByPredicateConsumer(
        IHistorySearcherService<TEntity, TId> historySearcherService,
        ILogger<HistGetByPredicateConsumer<TEntity, TId>> logger
    )
    {
        _historySearcherService = historySearcherService;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<HistGetByPredicateOrder<TEntity, TId>> context)
    {
        _logger.LogInformation("HistGetByPredicateConsumer: {Message}", context.Message);

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
