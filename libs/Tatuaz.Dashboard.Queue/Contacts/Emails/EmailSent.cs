using System;
using NodaTime;

namespace Tatuaz.Dashboard.Queue.Contacts.Emails;

public record EmailSent(Guid EmailId, Instant SentAt);
