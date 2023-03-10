using System;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;

namespace Tatuaz.Shared.Domain.Entities.Hist.Models.Post;

public class HistInitialPostPhoto : HistEntity<Guid>
{
    public Guid InitialPostId { get; set; }
    public Guid PhotoId { get; set; } = default!;
}
