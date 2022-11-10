using Microsoft.AspNetCore.Authorization;

namespace Tatuaz.Gateway.Authorization;

public class AuthorizeActiveUserAttribute : AuthorizeAttribute
{
    public AuthorizeActiveUserAttribute() : base(ActiveUserRequirement.Name) { }
}
