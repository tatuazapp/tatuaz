using MassTransit;
using Tatuaz.History.Infrastructure.Abstractions.Services;
using Tatuaz.History.Queue.Contracts;
using Tatuaz.Shared.Domain.Entities.Hist.Common;

namespace Tatuaz.History.Queue.Consumers;

public class HistGetBySpecificationConsumer<TEntity, TId> : IConsumer<HistGetBySpecificationOrder<TEntity, TId>>
    where TEntity : HistEntity<TId>
    where TId : notnull
{
    private readonly IHistorySearcherService<TEntity, TId> _historySearcherService;

    public HistGetBySpecificationConsumer(IHistorySearcherService<TEntity, TId> historySearcherService)
    {
        _historySearcherService = historySearcherService;
    }

    public async Task Consume(ConsumeContext<HistGetBySpecificationOrder<TEntity, TId>> context)
    {
        await context.RespondAsync(new HistGetBySpecificationResult<TEntity, TId>(
            await _historySearcherService.GetBySpecificationAsync(context.Message.Specification,
                context.Message.AsOf).ConfigureAwait(false))).ConfigureAwait(false);
    }
}
