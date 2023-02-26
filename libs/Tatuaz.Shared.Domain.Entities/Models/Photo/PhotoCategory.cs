using System;
using NodaTime;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Photo;
using Tatuaz.Shared.Domain.Entities.Models.Common;

namespace Tatuaz.Shared.Domain.Entities.Models.Photo;

public class PhotoCategory : Entity<HistPhotoCategory, int>
{
    public Guid PhotoId { get; set; }
    public virtual Photo Photo { get; set; } = default!;
    public int CategoryId { get; set; }
    public virtual Category Category { get; set; } = default!;

    public override HistEntity ToHistEntity(IClock clock, HistState state)
    {
        var histEntity = (HistPhotoCategory)base.ToHistEntity(clock, state);
        histEntity.PhotoId = PhotoId;
        histEntity.CategoryId = CategoryId;
        return histEntity;
    }
}
