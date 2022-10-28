namespace Tatuaz.Gateway.Configuration.Options;

public class SwaggerOptions
{
    /// <summary>
    /// Name of section this corresponds to in appsettings.json
    /// </summary>
    public const string SectionName = "Swagger";

    /// <summary>
    /// Is swagger enabled
    /// </summary>
    public bool Enabled { get; set; }
}
