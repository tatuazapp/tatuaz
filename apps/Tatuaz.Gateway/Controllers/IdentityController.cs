using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tatuaz.Gateway.Authorization;
using Tatuaz.Gateway.HttpResponses;
using Tatuaz.Gateway.Requests.Commands.Identity;
using Tatuaz.Gateway.Requests.Queries.Identity;
using Tatuaz.Shared.Domain.Dtos.Dtos.Identity;
using Tatuaz.Shared.Domain.Dtos.Dtos.Identity.User;

namespace Tatuaz.Gateway.Controllers;

/// <summary>
/// Controller for user related operations
/// </summary>
public class IdentityController : TatuazControllerBase
{
    /// <inheritdoc />
    public IdentityController(IMediator mediator) : base(mediator) { }

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
    public async Task<IActionResult> Me()
    {
        return ResultToActionResult(await Mediator.Send(new MeQuery()).ConfigureAwait(false));
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
    public async Task<IActionResult> SignUp([FromBody] SignUpDto signUpDto)
    {
        return ResultToActionResult(
            await Mediator.Send(new SignUpCommand(signUpDto)).ConfigureAwait(false)
        );
    }
}
