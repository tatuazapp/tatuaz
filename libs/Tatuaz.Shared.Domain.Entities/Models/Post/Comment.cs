using System;
using System.Collections.Generic;
using NodaTime;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Post;
using Tatuaz.Shared.Domain.Entities.Models.Common;
using Tatuaz.Shared.Domain.Entities.Models.Identity;

namespace Tatuaz.Shared.Domain.Entities.Models.Post;

public class Comment : AuditableEntity<HistComment, Guid>, IEntity
{
    public Guid? ParentCommentId { get; set; }
    public Comment? ParentComment { get; set; }
    public Guid PostId { get; set; }
    public Post Post { get; set; } = default!;
    public string UserId { get; set; } = default!;
    public TatuazUser User { get; set; } = default!;
    public string Content { get; set; } = default!;
    public ICollection<Comment> ChildComments { get; set; } = default!;

    public override HistEntity<Guid> ToHistEntity(IClock clock, HistState state)
    {
        var histEntity = (HistComment)base.ToHistEntity(clock, state);
        histEntity.ParentCommentId = ParentCommentId;
        histEntity.PostId = PostId;
        histEntity.UserId = UserId;
        histEntity.Content = Content;
        return histEntity;
    }
}
