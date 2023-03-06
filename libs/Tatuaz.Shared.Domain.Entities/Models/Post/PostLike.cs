using System;
using NodaTime;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Post;
using Tatuaz.Shared.Domain.Entities.Models.Common;
using Tatuaz.Shared.Domain.Entities.Models.Identity;

namespace Tatuaz.Shared.Domain.Entities.Models.Post;

public class PostLike : Entity<HistPostLike, Guid>, IEntity
{
    public Guid PostId { get; set; }
    public Post Post { get; set; } = default!;
    public string UserId { get; set; } = default!;
    public TatuazUser User { get; set; } = default!;

    public override HistEntity<Guid> ToHistEntity(IClock clock, HistState state)
    {
        var histEntity = (HistPostLike)base.ToHistEntity(clock, state);
        histEntity.PostId = PostId;
        histEntity.UserId = UserId;
        return histEntity;
    }
}
