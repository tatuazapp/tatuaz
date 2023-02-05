using System.Collections.Generic;
using NodaTime;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Photo;
using Tatuaz.Shared.Domain.Entities.Models.Common;

namespace Tatuaz.Shared.Domain.Entities.Models.Photo;

public class PhotoCategory : Entity<HistPhotoCategory, int>
{
    public string Title { get; set; } = default!;
    public PhotoCategoryType Type { get; set; }
    public string ImageUrl { get; set; } = default!;
    public int Popularity { get; set; } // popularity starts at 0 and is incremented by 1 for each signup that uses this category
    public virtual ICollection<UserPhotoCategory> UserPhotoCategories { get; set; } = default!;

    public override HistEntity ToHistEntity(IClock clock, HistState state)
    {
        var histEntity = (HistPhotoCategory)base.ToHistEntity(clock, state);
        histEntity.Title = Title;
        histEntity.Type = (HistPhotoCategoryType)Type;
        histEntity.ImageUrl = ImageUrl;
        histEntity.Popularity = Popularity;
        return histEntity;
    }

    public void IncrementPopularity()
    {
        Popularity++;
    }
}
