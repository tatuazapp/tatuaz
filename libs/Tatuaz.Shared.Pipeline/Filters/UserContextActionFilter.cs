using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Filters;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;

namespace Tatuaz.Shared.Pipeline.Filters;

public class UserContextActionFilter : IActionFilter
{
    private readonly IUserContext _userContext;

    public UserContextActionFilter(IUserContext userContext)
    {
        _userContext = userContext;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.HttpContext.User.Identity is { IsAuthenticated: true })
        {
            _userContext.CurrentUserEmail = context.HttpContext.User
                .FindFirst(ClaimTypes.Email)
                ?.Value;
        }
    }

    public void OnActionExecuted(ActionExecutedContext context) { }
}
