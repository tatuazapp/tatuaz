using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Tatuaz.History.DataAccess.Exceptions;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;

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

    public async Task<Guid> DumpAsync(
        THistEntity entity,
        CancellationToken cancellationToken = default
    )
    {
        var emptyHistId = entity.HistId == Guid.Empty;

        _histDbContext.Add(entity);
        await _histDbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        if (emptyHistId)
        {
            _logger.LogWarning(
                "HistId for entity with Id: {Id} was empty. Generated new one: {HistId}",
                entity.Id.ToString(),
                entity.HistId.ToString()
            );
        }

        return entity.HistId;
    }
}
