using System;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;

namespace Tatuaz.Shared.Domain.Entities.Hist.Models.Photo;

public class HistUserPhotoCategory : HistEntity<Guid>
{
    public string UserId { get; set; } = default!;
    public int PhotoCategoryId { get; set; }
}
