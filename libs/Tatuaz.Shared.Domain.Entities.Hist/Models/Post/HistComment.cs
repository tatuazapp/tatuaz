using System;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;

namespace Tatuaz.Shared.Domain.Entities.Hist.Models.Post;

public class HistComment : HistAuditableEntity<Guid>, IHistEntity
{
    public Guid? ParentCommentId { get; set; }
    public Guid PostId { get; set; }
    public string UserId { get; set; } = default!;
    public string Content { get; set; } = default!;
}
