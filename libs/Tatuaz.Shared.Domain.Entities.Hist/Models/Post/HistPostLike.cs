using System;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;

namespace Tatuaz.Shared.Domain.Entities.Hist.Models.Post;

public class HistPostLike : HistEntity<Guid>, IHistEntity
{
    public Guid PostId { get; set; }
    public string UserId { get; set; } = default!;
}
