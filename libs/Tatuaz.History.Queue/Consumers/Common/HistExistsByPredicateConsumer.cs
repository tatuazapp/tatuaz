using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Tatuaz.History.DataAccess.Services;
using Tatuaz.History.Queue.Contracts;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;

namespace Tatuaz.History.Queue.Consumers.Common;

public class HistExistsByPredicateConsumer<TEntity, TId>
    : IConsumer<HistExistsByPredicateOrder<TEntity, TId>>
    where TEntity : HistEntity<TId>
    where TId : notnull
{
    private readonly IHistorySearcherService<TEntity, TId> _historySearcherService;
    private readonly ILogger<HistExistsByPredicateConsumer<TEntity, TId>> _logger;

    public HistExistsByPredicateConsumer(
        IHistorySearcherService<TEntity, TId> historySearcherService,
        ILogger<HistExistsByPredicateConsumer<TEntity, TId>> logger
    )
    {
        _historySearcherService = historySearcherService;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<HistExistsByPredicateOrder<TEntity, TId>> context)
    {
        _logger.LogInformation("HistExistsByPredicateConsumer: {Message}", context.Message);
        await context
            .RespondAsync(
                await _historySearcherService
                    .ExistsByPredicateAsync(context.Message.Predicate, context.Message.AsOf)
                    .ConfigureAwait(false)
            )
            .ConfigureAwait(false);
    }
}