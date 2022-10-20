using MassTransit;
using Microsoft.Extensions.Logging;
using Tatuaz.History.DataAccess.Services;
using Tatuaz.History.Queue.Contracts;
using Tatuaz.Shared.Domain.Entities.Hist.Common;

namespace Tatuaz.History.Queue.Consumers;

public class HistGetByIdConsumer<TEntity, TId> : IConsumer<HistGetByIdOrder<TEntity, TId>>
    where TEntity : HistEntity<TId>
    where TId : notnull
{
    private readonly IHistorySearcherService<TEntity, TId> _historySearcherService;
    private readonly ILogger<HistGetByIdConsumer<TEntity, TId>> _logger;

    public HistGetByIdConsumer(
        IHistorySearcherService<TEntity, TId> historySearcherService,
        ILogger<HistGetByIdConsumer<TEntity, TId>> logger
    )
    {
        _historySearcherService = historySearcherService;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<HistGetByIdOrder<TEntity, TId>> context)
    {
        _logger.LogInformation("HistGetByIdConsumer: {0}", context.Message);

        await context
            .RespondAsync(
                new HistGetByIdResult<TEntity, TId>(
                    await _historySearcherService
                        .GetByIdAsync(context.Message.Id, context.Message.AsOf)
                        .ConfigureAwait(false)
                )
            )
            .ConfigureAwait(false);
    }
}
