using NodaTime;

using Tatuaz.Shared.Domain.Entities.Hist.Common;

namespace Tatuaz.Shared.Domain.Entities.Common;

public interface IHistDumpableEntity
{
    HistEntity ToHistEntity(IClock clock);
}
