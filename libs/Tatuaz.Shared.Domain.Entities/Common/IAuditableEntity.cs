using NodaTime;

namespace Tatuaz.Shared.Domain.Entities.Common;

public interface IAuditableEntity
{
    void UpdateCreationData(Guid userId, IClock clock);
    void UpdateModificationData(Guid userId, IClock clock);
}
