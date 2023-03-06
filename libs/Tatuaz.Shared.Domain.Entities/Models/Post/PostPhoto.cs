using System;
using NodaTime;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Post;
using Tatuaz.Shared.Domain.Entities.Models.Common;

namespace Tatuaz.Shared.Domain.Entities.Models.Post;

public class PostPhoto : Entity<HistPostPhoto, Guid>, IEntity
{
    public Guid PostId { get; set; }
    public Post Post { get; set; } = default!;
    public Guid PhotoId { get; set; }
    public Photo.Photo Photo { get; set; } = default!;

    public override HistEntity ToHistEntity(IClock clock, HistState state)
    {
        var histEntity = (HistPostPhoto)base.ToHistEntity(clock, state);
        histEntity.PostId = PostId;
        histEntity.PhotoId = PhotoId;
        return histEntity;
    }
}
