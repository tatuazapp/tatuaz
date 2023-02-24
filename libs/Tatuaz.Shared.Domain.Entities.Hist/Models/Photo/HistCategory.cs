using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;

namespace Tatuaz.Shared.Domain.Entities.Hist.Models.Photo;

public class HistCategory : HistEntity<int>
{
    public string Title { get; set; } = default!;
    public HistCategoryType Type { get; set; }
    public string ImageUri { get; set; } = default!;
    public int Popularity { get; set; }
}
