using NodaTime;
using Tatuaz.Shared.Domain.Entities.Models.Attributes;

namespace Tatuaz.Shared.Domain.Entities.Models.Common;

[BaseEntity, TestIgnoreEntity]
public interface IAuditableEntity : IEntity
{
    void UpdateCreationData(string userId, IClock clock);
    void UpdateModificationData(string userId, IClock clock);
}
