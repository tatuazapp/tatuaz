namespace Tatuaz.Gateway.Configuration;

public class SwaggerOpt
{
    /// <summary>
    ///     Name of section this corresponds to in appsettings.json
    /// </summary>
    public const string SectionName = "Swagger";

    /// <summary>
    ///     Is swagger enabled
    /// </summary>
    public bool Enabled { get; set; }

    public string Route { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Title { get; set; } = default!;

    public string? Theme { get; set; }
}
