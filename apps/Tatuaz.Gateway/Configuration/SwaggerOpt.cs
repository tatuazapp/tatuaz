namespace Tatuaz.Gateway.Configuration;

/// <summary>
/// Represents gateway swagger configuration.
/// </summary>
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

    /// <summary>
    /// Route prefix for swagger
    /// </summary>
    public string Route { get; set; } = default!;

    /// <summary>
    ///  Name of the API
    /// </summary>
    public string Name { get; set; } = default!;

    /// <summary>
    /// Swagger title
    /// </summary>
    public string Title { get; set; } = default!;

    /// <summary>
    /// Optional theme added via custom css
    /// </summary>
    public string? Theme { get; set; }
}
