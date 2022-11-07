using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tatuaz.Gateway.Authorization;
using Tatuaz.Gateway.HttpResponses;
using Tatuaz.Gateway.Requests.Commands.Users;
using Tatuaz.Gateway.Requests.Queries.Users;
using Tatuaz.Shared.Domain.Dtos.Dtos.Identity;

namespace Tatuaz.Gateway.Controllers;

public class UsersController : TatuazControllerBase
{
    public UsersController(IMediator mediator) : base(mediator) { }

    /// <summary>
    ///     Check what user is logged in
    /// </summary>
    /// <returns>UserDto</returns>
    [HttpPost("[action]")]
    [AuthorizeActiveUser]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(OkResponse<UserDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(EmptyResponse), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(EmptyResponse), (int)HttpStatusCode.Forbidden)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> WhoAmI()
    {
        return ResultToActionResult(await Mediator.Send(new WhoAmIQuery()).ConfigureAwait(false));
    }

    /// <summary>
    ///     Register user
    /// </summary>
    /// <returns>UserDto</returns>
    [HttpPost("[action]")]
    [Authorize]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(OkResponse<UserDto>), (int)HttpStatusCode.Created)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(EmptyResponse), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(EmptyResponse), (int)HttpStatusCode.Forbidden)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> SignUp([FromBody] CreateUserDto createUserDto)
    {
        return ResultToActionResult(
            await Mediator.Send(new SignUpCommand(createUserDto)).ConfigureAwait(false)
        );
    }
}
