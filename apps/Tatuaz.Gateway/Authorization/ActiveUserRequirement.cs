using Microsoft.AspNetCore.Authorization;

namespace Tatuaz.Gateway.Authorization;

public class ActiveUserRequirement : IAuthorizationRequirement
{
    public const string Name = "ActiveUser";
}
