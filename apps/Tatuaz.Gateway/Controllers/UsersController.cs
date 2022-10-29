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

    [HttpGet("[action]")]
    [AuthorizeActiveUser]
    public async Task<ActionResult<UserDto>> WhoAmI()
    {
        return ResultToActionResult(await Mediator.Send(new WhoAmIQuery()).ConfigureAwait(false));
    }

    [HttpPost("[action]")]
    [Authorize]
    public async Task<ActionResult<UserDto>> SignUp([FromBody] CreateUserDto createUserDto)
    {
        return ResultToActionResult(await Mediator.Send(new SignUpCommand(createUserDto)).ConfigureAwait(false));
    }
}
