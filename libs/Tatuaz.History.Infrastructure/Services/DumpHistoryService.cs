using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Tatuaz.History.DataAccess.Exceptions;
using Tatuaz.Shared.Domain.Entities.Hist.Common;

namespace Tatuaz.History.DataAccess.Services;

public class DumpHistoryService<THistEntity, TId> : IDumpHistoryService<THistEntity, TId>
    where THistEntity : HistEntity<TId>
    where TId : notnull
{
    private readonly HistDbContext _histDbContext;
    private readonly ILogger<DumpHistoryService<THistEntity, TId>> _logger;

    public DumpHistoryService(
        HistDbContext histDbContext,
        ILogger<DumpHistoryService<THistEntity, TId>> logger
    )
    {
        _histDbContext = histDbContext;
        _logger = logger;
    }

    public async Task<Guid> DumpAsync(THistEntity entity)
    {
        switch (entity.HistState)
        {
            case HistState.Added:
                await ValidateNotYetDumpedAsync(entity).ConfigureAwait(false);
                break;
            case HistState.Modified
            or HistState.Deleted:
                await ValidateAlreadyDumpedAsync(entity).ConfigureAwait(false);
                break;
            default:
                throw new HistException("Invalid HistState");
        }

        _histDbContext.Add(entity);
        await _histDbContext.SaveChangesAsync().ConfigureAwait(false);

        return entity.HistId;
    }

    private async Task ValidateAlreadyDumpedAsync(THistEntity entity)
    {
        if (
            !await _histDbContext
                .Set<THistEntity>()
                .AnyAsync(x => x.HistState == HistState.Added && entity.Id.Equals(x.Id))
                .ConfigureAwait(false)
        )
        {
            throw new HistException("Entity does not exist in history yet.");
        }
    }

    private async Task ValidateNotYetDumpedAsync(THistEntity entity)
    {
        if (
            await _histDbContext
                .Set<THistEntity>()
                .AnyAsync(x => x.HistState == HistState.Added && entity.Id.Equals(x.Id))
                .ConfigureAwait(false)
        )
        {
            throw new HistException("Entity already exists in history");
        }
    }
}
