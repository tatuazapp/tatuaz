using MassTransit;
using Tatuaz.History.Infrastructure.Abstractions.Services;
using Tatuaz.History.Queue.Contracts;
using Tatuaz.Shared.Domain.Entities.Hist.Common;

namespace Tatuaz.History.Queue.Consumers;

public class HistGetByIdConsumer<TEntity, TId> : IConsumer<HistGetByIdOrder<TEntity, TId>>
    where TEntity : HistEntity<TId>
    where TId : notnull
{
    private readonly IHistorySearcherService<TEntity, TId> _historySearcherService;

    public HistGetByIdConsumer(IHistorySearcherService<TEntity, TId> historySearcherService)
    {
        _historySearcherService = historySearcherService;
    }

    public async Task Consume(ConsumeContext<HistGetByIdOrder<TEntity, TId>> context)
    {
        await context.RespondAsync(new HistGetByIdResult<TEntity, TId>(await _historySearcherService
            .GetByIdAsync(context.Message.Id, context.Message.AsOf)
            .ConfigureAwait(false))).ConfigureAwait(false);
    }
}