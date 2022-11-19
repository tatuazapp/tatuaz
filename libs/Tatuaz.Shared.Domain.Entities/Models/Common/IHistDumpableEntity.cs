using NodaTime;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;
using Tatuaz.Shared.Domain.Entities.Models.Attributes;

namespace Tatuaz.Shared.Domain.Entities.Models.Common;

[BaseEntity, TestIgnoreEntity]
public interface IHistDumpableEntity : IEntity
{
    HistEntity ToHistEntity(IClock clock, HistState state);
}
