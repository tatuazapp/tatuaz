using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Tatuaz.Gateway.Requests.Queries.Users;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;

namespace Tatuaz.Gateway.Authorization;

/// <summary>
/// Authorization handler for checking if user exists in database meaning he got through onboarding
/// </summary>
public class ActiveUserHandler : AuthorizationHandler<ActiveUserRequirement>
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="mediator">From DI</param>
    public ActiveUserHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Check if user exists in database
    /// </summary>
    /// <param name="context"></param>
    /// <param name="requirement"></param>
    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        ActiveUserRequirement requirement
    )
    {
        var userEmail = context.User.FindFirst(ClaimTypes.Email)?.Value;
        var userAuth0Id = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userEmail == null || userAuth0Id == null)
        {
            context.Fail();
            return;
        }

        var userExists = await _mediator
            .Send(new UserExistsQuery(userEmail, userAuth0Id))
            .ConfigureAwait(false);

        if (userExists)
        {
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
        }
    }
}
