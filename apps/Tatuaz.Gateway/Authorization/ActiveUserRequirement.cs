using Microsoft.AspNetCore.Authorization;

namespace Tatuaz.Gateway.Authorization;

/// <summary>
/// Represents a requirement that user went through onboarding.
/// </summary>
public class ActiveUserRequirement : IAuthorizationRequirement
{
    /// <summary>
    /// Name of the requirement.
    /// </summary>
    public const string Name = "ActiveUser";
}
