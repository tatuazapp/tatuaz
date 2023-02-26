using System;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;

namespace Tatuaz.Shared.Domain.Entities.Hist.Models.Photo;

public class HistPhotoCategory : HistEntity<int>
{
    public Guid PhotoId { get; set; }
    public int CategoryId { get; set; }
}
