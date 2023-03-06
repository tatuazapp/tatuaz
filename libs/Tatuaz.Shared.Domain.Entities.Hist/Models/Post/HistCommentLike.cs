using System;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;

namespace Tatuaz.Shared.Domain.Entities.Hist.Models.Post;

public class HistCommentLike : HistEntity<Guid>, IHistEntity
{
    public Guid CommentId { get; set; }
    public string UserId { get; set; } = default!;
}
