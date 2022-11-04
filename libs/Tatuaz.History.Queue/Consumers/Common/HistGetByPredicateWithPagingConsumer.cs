using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Tatuaz.History.DataAccess.Services;
using Tatuaz.History.Queue.Contracts;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;

namespace Tatuaz.History.Queue.Consumers.Common;

public class HistGetByPredicateWithPagingConsumer<TEntity, TId>
    : IConsumer<HistGetByPredicateWithPagingOrder<TEntity, TId>>
    where TEntity : HistEntity<TId>
    where TId : notnull
{
    private readonly IHistorySearcherService<TEntity, TId> _historySearcherService;
    private readonly ILogger<HistGetByPredicateWithPagingConsumer<TEntity, TId>> _logger;

    public HistGetByPredicateWithPagingConsumer(
        IHistorySearcherService<TEntity, TId> historySearcherService,
        ILogger<HistGetByPredicateWithPagingConsumer<TEntity, TId>> logger
    )
    {
        _historySearcherService = historySearcherService;
        _logger = logger;
    }

    public async Task Consume(
        ConsumeContext<HistGetByPredicateWithPagingOrder<TEntity, TId>> context
    )
    {
        _logger.LogInformation("HistGetByPredicateWithPagingConsumer: {Message}", context.Message);
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