namespace Tatuaz.Shared.Pipeline.Configuration;

public class RabbitMqOpt
{
    public const string SectionName = "RabbitMq";
    public string Host { get; set; } = default!;
    public string VirtualHost { get; set; } = default!;
    public string Username { get; set; } = default!;
    public string Password { get; set; } = default!;
}
