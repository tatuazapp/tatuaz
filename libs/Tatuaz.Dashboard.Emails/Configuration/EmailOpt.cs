namespace Tatuaz.Dashboard.Emails.Configuration;

public class EmailOpt
{
    public const string SectionName = "Email";

    public string FromEmail { get; set; } = default!;
    public string FromName { get; set; } = default!;
    public string SmtpHost { get; set; } = default!;
    public int SmtpPort { get; set; }
    public string SmtpUsername { get; set; } = default!;
    public string SmtpPassword { get; set; } = default!;
}
