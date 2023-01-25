using System;
using Tatuaz.Dashboard.Emails;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Dashboard.Queue.Contacts.Emails;

public record SendEmailOrder(
    string RecipientEmail,
    Guid EmailId,
    EmailType EmailType,
    Guid ObjectId
);
