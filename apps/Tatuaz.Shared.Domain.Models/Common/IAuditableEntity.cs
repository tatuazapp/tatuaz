namespace Tatuaz.Shared.Domain.Models.Common;

public interface IAuditableEntity
{
    void UpdateCreationData(Guid userId);
    void UpdateModificationData(Guid userId);
}
