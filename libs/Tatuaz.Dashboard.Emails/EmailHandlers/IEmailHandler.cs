using System;
using System.Threading.Tasks;

namespace Tatuaz.Dashboard.Emails.EmailHandlers;

public interface IEmailHandler
{
    Task SendEmailAsync(Guid emailId, string recipientEmail, Guid objectId);
    string GetEmailType();
}
