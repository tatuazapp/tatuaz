using NodaTime;

namespace Tatuaz.Shared.Domain.Entities.Models.Common;

public interface IAuditableEntity
{
    void UpdateCreationData(string userId, IClock clock);
    void UpdateModificationData(string userId, IClock clock);
}
