using MassTransit;
using Microsoft.Extensions.Logging;
using Tatuaz.History.DataAccess.Services;
using Tatuaz.History.Queue.Contracts;
using Tatuaz.Shared.Domain.Entities.Hist.Common;

namespace Tatuaz.History.Queue.Consumers;

public class HistExistsByIdConsumer<TEntity, TId> : IConsumer<HistExistsByIdOrder<TEntity, TId>>
    where TEntity : HistEntity<TId>
    where TId : notnull
{
    private readonly IHistorySearcherService<TEntity, TId> _historyRepository;
    private readonly ILogger<HistExistsByIdConsumer<TEntity, TId>> _logger;

    public HistExistsByIdConsumer(
        IHistorySearcherService<TEntity, TId> historyRepository,
        ILogger<HistExistsByIdConsumer<TEntity, TId>> logger
    )
    {
        _historyRepository = historyRepository;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<HistExistsByIdOrder<TEntity, TId>> context)
    {
        _logger.LogInformation("HistExistsByIdConsumer: {Message}", context.Message.Id);
        await context
            .RespondAsync(
                new HistExistsByIdResult<TEntity, TId>(
                    await _historyRepository
                        .ExistsByIdAsync(context.Message.Id, context.Message.AsOf)
                        .ConfigureAwait(false)
                )
            )
            .ConfigureAwait(false);
    }
}
