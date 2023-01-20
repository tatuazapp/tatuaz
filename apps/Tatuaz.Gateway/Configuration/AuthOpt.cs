namespace Tatuaz.Gateway.Configuration;

/// <summary>
/// Represents auth0 configuration.
/// </summary>
public class AuthOpt
{
    /// <summary>
    ///     Name of section this corresponds to in appsettings.json
    /// </summary>
    public const string SectionName = "Auth";

    /// <summary>
    ///     Auth0 authority
    /// </summary>
    public string Authority { get; set; } = default!;

    /// <summary>
    ///     Auth0 audience
    /// </summary>
    public string Audience { get; set; } = default!;

    /// <summary>
    ///     Auth0 domain
    /// </summary>
    public string Domain { get; set; } = default!;
}
