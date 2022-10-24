using NodaTime;

namespace Tatuaz.Shared.Domain.Entities.Models.Common;

public interface IAuditableEntity
{
    void UpdateCreationData(Guid userId, IClock clock);
    void UpdateModificationData(Guid userId, IClock clock);
}
