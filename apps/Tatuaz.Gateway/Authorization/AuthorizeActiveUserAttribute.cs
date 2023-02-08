using Microsoft.AspNetCore.Authorization;

namespace Tatuaz.Gateway.Authorization;

/// <summary>
/// Attribute to mark a controller as requiring user that went through onboarding.
/// </summary>
public class AuthorizeActiveUserAttribute : AuthorizeAttribute
{
    /// <summary>
    /// Default constructor.
    /// </summary>
    public AuthorizeActiveUserAttribute()
        : base(ActiveUserRequirement.Name) { }
}
