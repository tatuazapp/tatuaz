using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tatuaz.Gateway.Authorization;
using Tatuaz.Gateway.Requests.Commands.Users;
using Tatuaz.Gateway.Requests.Queries.Users;
using Tatuaz.Shared.Domain.Dtos.Dtos.Identity;

namespace Tatuaz.Gateway.Controllers;

public class UsersController : TatuazControllerBase
{
    public UsersController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet("[action]"), AuthorizeActiveUser]
    public async Task<ActionResult<UserDto>> WhoAmI([FromQuery] WhoAmIQuery query)
    {
        return ResultToActionResult(await Mediator.Send(query).ConfigureAwait(false));
    }

    [HttpPost("[action]"), Authorize]
    public async Task<ActionResult<UserDto>> SignUp([FromBody] SignUpCommand command)
    {
        return ResultToActionResult(await Mediator.Send(command).ConfigureAwait(false));
    }
}
