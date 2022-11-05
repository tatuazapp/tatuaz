using NodaTime;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;

namespace Tatuaz.Shared.Domain.Entities.Models.Common;

public interface IHistDumpableEntity
{
    HistEntity ToHistEntity(IClock clock, HistState state);
}
