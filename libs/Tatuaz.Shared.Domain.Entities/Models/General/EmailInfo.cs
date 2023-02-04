using System;
using NodaTime;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;
using Tatuaz.Shared.Domain.Entities.Hist.Models.General;
using Tatuaz.Shared.Domain.Entities.Models.Common;

namespace Tatuaz.Shared.Domain.Entities.Models.General;

public class EmailInfo : Entity<HistEmailInfo, Guid>
{
    public string RecipientEmail { get; set; } = default!;
    public string EmailType { get; set; } = default!;
    public Guid ObjectId { get; set; }
    public Instant OrderedAt { get; set; }
    public Instant? SentAt { get; set; }
    public int RetryCount { get; set; }
    public string? Error { get; set; }

    public override HistEmailInfo ToHistEntity(IClock clock, HistState state)
    {
        var histEntity = (HistEmailInfo)base.ToHistEntity(clock, state);
        histEntity.RecipientEmail = RecipientEmail;
        histEntity.EmailType = EmailType;
        histEntity.ObjectId = ObjectId;
        histEntity.OrderedAt = OrderedAt;
        histEntity.SentAt = SentAt;
        histEntity.RetryCount = RetryCount;
        histEntity.Error = Error;
        return histEntity;
    }
}
