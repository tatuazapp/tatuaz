using System.Collections.Generic;
using NodaTime;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Photo;
using Tatuaz.Shared.Domain.Entities.Models.Common;

namespace Tatuaz.Shared.Domain.Entities.Models.Photo;

public class Category : Entity<HistCategory, int>
{
    public string Title { get; set; } = default!;
    public CategoryType Type { get; set; }
    public string ImageUri { get; set; } = default!;
    public virtual ICollection<UserCategory> UserCategories { get; set; } = default!;
    public virtual ICollection<PhotoCategory> PhotoCategories { get; set; } = default!;

    public override HistEntity ToHistEntity(IClock clock, HistState state)
    {
        var histEntity = (HistCategory)base.ToHistEntity(clock, state);
        histEntity.Title = Title;
        histEntity.Type = (HistCategoryType)Type;
        histEntity.ImageUri = ImageUri;
        return histEntity;
    }
}
