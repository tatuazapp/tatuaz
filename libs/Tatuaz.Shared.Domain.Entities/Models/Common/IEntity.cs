using Tatuaz.Shared.Domain.Entities.Models.Attributes;

namespace Tatuaz.Shared.Domain.Entities.Models.Common;

/// <summary>
/// Marker interface for all entities.
/// </summary>
[BaseEntity, TestIgnoreEntity]
public interface IEntity { }
