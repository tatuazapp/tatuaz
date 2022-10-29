using MediatR;
using Microsoft.AspNetCore.Authorization;
using Tatuaz.Gateway.Requests.Queries.Users;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;

namespace Tatuaz.Gateway.Authorization;

public class ActiveUserHandler : AuthorizationHandler<ActiveUserRequirement>
{
    private readonly IMediator _mediator;
    private readonly IUserAccessor _userAccessor;

    public ActiveUserHandler(IMediator mediator, IUserAccessor userAccessor)
    {
        _mediator = mediator;
        _userAccessor = userAccessor;
    }
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, ActiveUserRequirement requirement)
    {
        if(_userAccessor.CurrentUserId == null)
        {
            context.Fail();
            return;
        }
        var userExists = await _mediator.Send(new UserExistsQuery(_userAccessor.CurrentUserId));

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
