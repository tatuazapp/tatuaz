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
    private readonly IUserContext _userContext;

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="mediator">From DI</param>
    /// <param name="userContext">From DI</param>
    public ActiveUserHandler(IMediator mediator, IUserContext userContext)
    {
        _mediator = mediator;
        _userContext = userContext;
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
        if (_userContext.CurrentUserEmail == null)
        {
            context.Fail();
            return;
        }

        var userExists = await _mediator
            .Send(new UserExistsQuery(_userContext.CurrentUserEmail))
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
