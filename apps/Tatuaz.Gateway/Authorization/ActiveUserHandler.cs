using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Tatuaz.Gateway.Requests.Queries.Users;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;

namespace Tatuaz.Gateway.Authorization;

public class ActiveUserHandler : AuthorizationHandler<ActiveUserRequirement>, IUserContextEnjoyer
{
    private readonly IMediator _mediator;
    private IUserContext _userContext;

    public ActiveUserHandler(IMediator mediator, IUserContext userContext)
    {
        _mediator = mediator;
        _userContext = userContext;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        ActiveUserRequirement requirement
    )
    {
        if (_userContext.CurrentUserId == null)
        {
            context.Fail();
            return;
        }

        var userExists = await _mediator
            .Send(new UserExistsQuery(_userContext.CurrentUserId))
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

    public void SetUserContext(IUserContext userContext)
    {
        _userContext = userContext;
    }
}
