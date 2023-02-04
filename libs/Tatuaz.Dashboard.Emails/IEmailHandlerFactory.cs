using Tatuaz.Dashboard.Emails.EmailHandlers;

namespace Tatuaz.Dashboard.Emails;

public interface IEmailHandlerFactory
{
    IEmailHandler GetEmailHandler(EmailType emailType);
}
