using System;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;

namespace Tatuaz.Shared.Domain.Entities.Hist.Models.Photo;

public class HistUserCategory : HistEntity<int>
{
    public string UserId { get; set; } = default!;
    public int CategoryId { get; set; }
}
