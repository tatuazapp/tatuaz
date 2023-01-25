using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;

namespace Tatuaz.Gateway.Infrastructure;

public class UserContext : IUserContext
{
    public string? CurrentUserEmail { get; set; }
}
