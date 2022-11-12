using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;

namespace Tatuaz.Gateway.Infrastructure;

public class GatewayUserContext : IUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GatewayUserContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? CurrentUserId =>
        _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
}
