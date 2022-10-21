using Tatuaz.Shared.Domain.Entities.Hist.Common;

namespace Tatuaz.History.DataAccess.Services;

public interface IDumpHistoryService<in THistEntity, TId>
    where THistEntity : HistEntity<TId>
    where TId : notnull
{
    Task<Guid> DumpAsync(THistEntity entity, CancellationToken cancellationToken = default);
}
