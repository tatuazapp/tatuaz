using MassTransit;
using Tatuaz.History.Infrastructure.Abstractions.Services;
using Tatuaz.History.Queue.Contracts;
using Tatuaz.Shared.Domain.Entities.Hist.Common;

namespace Tatuaz.History.Queue.Consumers;

public class HistExistsByIdConsumer<TEntity, TId> : IConsumer<HistExistsByIdOrder<TEntity, TId>>
    where TEntity : HistEntity<TId>
    where TId : notnull
{
    private readonly IHistorySearcherService<TEntity, TId> _historyRepository;

    public HistExistsByIdConsumer(IHistorySearcherService<TEntity, TId> historyRepository)
    {
        _historyRepository = historyRepository;
    }

    public async Task Consume(ConsumeContext<HistExistsByIdOrder<TEntity, TId>> context)
    {
        await context
            .RespondAsync(new HistExistsByIdResult<TEntity, TId>(await _historyRepository
                .ExistsByIdAsync(context.Message.Id, context.Message.AsOf)
                .ConfigureAwait(false)))
            .ConfigureAwait(false);
    }
}
