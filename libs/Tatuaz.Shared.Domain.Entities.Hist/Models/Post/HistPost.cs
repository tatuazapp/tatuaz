using System;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;

namespace Tatuaz.Shared.Domain.Entities.Hist.Models.Post;

public class HistPost : HistAuditableEntity<Guid>, IHistEntity
{
    public string AuthorId { get; set; } = default!;
    public string Description { get; set; } = default!;
}
