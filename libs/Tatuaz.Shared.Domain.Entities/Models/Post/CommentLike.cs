using System;
using System.Collections.Generic;
using NodaTime;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Post;
using Tatuaz.Shared.Domain.Entities.Models.Common;
using Tatuaz.Shared.Domain.Entities.Models.Identity;

namespace Tatuaz.Shared.Domain.Entities.Models.Post;

public class CommentLike : Entity<HistCommentLike, Guid>, IEntity
{
    public Guid CommentId { get; set; }
    public Comment Comment { get; set; } = default!;
    public string UserId { get; set; } = default!;
    public TatuazUser User { get; set; } = default!;

    public override HistEntity<Guid> ToHistEntity(IClock clock, HistState state)
    {
        var histEntity = (HistCommentLike)base.ToHistEntity(clock, state);
        histEntity.CommentId = CommentId;
        histEntity.UserId = UserId;
        return histEntity;
    }
}
