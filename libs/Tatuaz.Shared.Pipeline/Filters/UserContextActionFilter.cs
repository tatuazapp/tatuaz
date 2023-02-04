using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Filters;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;
using Tatuaz.Shared.Pipeline.UserContext;

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
        if (context.HttpContext.User.Identity is not { IsAuthenticated: true })
        {
            return;
        }

        _userContext.CurrentUserEmail = context.HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;

        _userContext.CurrentUserAuth0Id = context.HttpContext.User
            .FindFirst(ClaimTypes.NameIdentifier)
            ?.Value;
    }

    public void OnActionExecuted(ActionExecutedContext context) { }
}
