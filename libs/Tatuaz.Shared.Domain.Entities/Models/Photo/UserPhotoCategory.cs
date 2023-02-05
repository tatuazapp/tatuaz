using System;
using NodaTime;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Photo;
using Tatuaz.Shared.Domain.Entities.Models.Common;
using Tatuaz.Shared.Domain.Entities.Models.Identity;

namespace Tatuaz.Shared.Domain.Entities.Models.Photo;

public class UserPhotoCategory : Entity<HistUserPhotoCategory, Guid>
{
    public string UserId { get; set; } = default!;
    public virtual TatuazUser User { get; set; } = default!;
    public int PhotoCategoryId { get; set; }
    public virtual PhotoCategory PhotoCategory { get; set; } = default!;

    public override HistEntity ToHistEntity(IClock clock, HistState state)
    {
        var histEntity = (HistUserPhotoCategory)base.ToHistEntity(clock, state);
        histEntity.UserId = UserId;
        histEntity.PhotoCategoryId = PhotoCategoryId;
        return histEntity;
    }
}
