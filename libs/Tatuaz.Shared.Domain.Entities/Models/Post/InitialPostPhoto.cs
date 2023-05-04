using System;
using NodaTime;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Post;
using Tatuaz.Shared.Domain.Entities.Models.Common;

namespace Tatuaz.Shared.Domain.Entities.Models.Post;

public class InitialPostPhoto : Entity<HistInitialPostPhoto, Guid>
{
    public Guid InitialPostId { get; set; }
    public InitialPost InitialPost { get; set; } = default!;
    public Guid PhotoId { get; set; }
    public Photo.Photo Photo { get; set; } = default!;

    public override HistEntity ToHistEntity(IClock clock, HistState state)
    {
        var histEntity = (HistInitialPostPhoto)base.ToHistEntity(clock, state);
        histEntity.InitialPostId = InitialPostId;
        histEntity.PhotoId = PhotoId;
        return histEntity;
    }
}
