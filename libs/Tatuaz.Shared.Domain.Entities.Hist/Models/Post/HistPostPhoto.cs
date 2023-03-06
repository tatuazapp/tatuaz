using System;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;

namespace Tatuaz.Shared.Domain.Entities.Hist.Models.Post;

public class HistPostPhoto : HistEntity<Guid>, IHistEntity
{
    public Guid PostId { get; set; }
    public Guid PhotoId { get; set; }
}
