namespace Tatuaz.Dashboard.Emails.Data;

public class EmailData
{
    public string RecipientName { get; }
    public string Subject { get; }
    public string TemplateName { get; }
    public object TemplateData { get; }

    public EmailData(string recipientName, string subject, string templateName, object templateData)
    {
        RecipientName = recipientName;
        Subject = subject;
        TemplateName = templateName;
        TemplateData = templateData;
    }
}
