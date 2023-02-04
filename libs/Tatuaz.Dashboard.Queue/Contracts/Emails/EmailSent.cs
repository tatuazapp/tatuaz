using System;
using NodaTime;

namespace Tatuaz.Dashboard.Queue.Contracts.Emails;

public record EmailSent(Guid EmailId, Instant SentAt);
