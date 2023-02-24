using System;
using Tatuaz.Dashboard.Emails;

namespace Tatuaz.Dashboard.Queue.Contracts.Emails;

public record SendEmail(
    string RecipientEmail,
    Guid EmailId,
    EmailType EmailType,
    Guid ObjectId
);
