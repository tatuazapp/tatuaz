using System;
using System.Collections.Generic;
using NodaTime;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Post;
using Tatuaz.Shared.Domain.Entities.Models.Common;
using Tatuaz.Shared.Domain.Entities.Models.Identity;

namespace Tatuaz.Shared.Domain.Entities.Models.Post;

public class Post : AuditableEntity<HistPost, Guid>, IEntity
{
    public string AuthorId { get; set; } = default!;
    public TatuazUser Author { get; set; } = default!;
    public string Description { get; set; } = default!;
    public ICollection<PostPhoto> Photos { get; set; } = default!;
    public ICollection<PostLike> Likes { get; set; } = default!;
    public ICollection<Comment> Comments { get; set; } = default!;

    public override HistEntity<Guid> ToHistEntity(IClock clock, HistState state)
    {
        var histEntity = (HistPost)base.ToHistEntity(clock, state);
        histEntity.AuthorId = AuthorId;
        histEntity.Description = Description;
        return histEntity;
    }
}
