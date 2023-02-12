using System;

namespace Tatuaz.Dashboard.Emails.Exceptions;

public class SendEmailException : Exception
{
    public SendEmailException() : base("Failed to send email") { }
}
