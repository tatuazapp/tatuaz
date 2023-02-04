using System;
using NodaTime;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;

namespace Tatuaz.Shared.Domain.Entities.Hist.Models.General;

public class HistEmailInfo : HistEntity<Guid>
{
    public string RecipientEmail { get; set; } = default!;
    public string EmailType { get; set; } = default!;
    public Guid ObjectId { get; set; }
    public Instant OrderedAt { get; set; }
    public Instant? SentAt { get; set; }
    public int RetryCount { get; set; }
    public string? Error { get; set; }
}
