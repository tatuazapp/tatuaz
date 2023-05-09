using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tatuaz.Dashboard.Queue.Consumers.Identity;
using Tatuaz.Gateway.Authorization;
using Tatuaz.Gateway.HttpResponses;
using Tatuaz.Gateway.Requests.Commands.Identity;
using Tatuaz.Gateway.Requests.Queries.Identity;
using Tatuaz.Shared.Domain.Dtos.Dtos.Identity;
using Tatuaz.Shared.Infrastructure.Abstractions.Paging;

namespace Tatuaz.Gateway.Controllers;

/// <summary>
/// Controller for user related operations
/// </summary>
public class IdentityController : TatuazControllerBase
{
    /// <inheritdoc />
    public IdentityController(IMediator mediator)
        : base(mediator) { }

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

    /// <summary>
    /// Set foreground photo
    /// </summary>
    /// <param name="setForegroundPhotoDto"></param>
    /// <returns></returns>
    [HttpPost("[action]")]
    [AuthorizeActiveUser]
    [ProducesResponseType(typeof(EmptyResponse), (int)HttpStatusCode.Created)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(EmptyResponse), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(EmptyResponse), (int)HttpStatusCode.Forbidden)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> SetForegroundPhoto(IFormFile photo)
    {
        return ResultToActionResult(
            await Mediator
                .Send(new SetForegroundPhotoCommand(new SetForegroundPhotoDto(photo)))
                .ConfigureAwait(false)
        );
    }

    /// <summary>
    /// Set background photo
    /// </summary>
    /// <param name="setBackgroundPhotoDto"></param>
    /// <returns></returns>
    [HttpPost("[action]")]
    [AuthorizeActiveUser]
    [ProducesResponseType(typeof(EmptyResponse), (int)HttpStatusCode.Created)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(EmptyResponse), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(EmptyResponse), (int)HttpStatusCode.Forbidden)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> SetBackgroundPhoto(IFormFile photo)
    {
        return ResultToActionResult(
            await Mediator
                .Send(new SetBackgroundPhotoCommand(new SetBackgroundPhotoDto(photo)))
                .ConfigureAwait(false)
        );
    }

    /// <summary>
    /// Delete foreground photo
    /// </summary>
    /// <param name="deleteForegroundPhotoDto"></param>
    /// <returns></returns>
    [HttpPost("[action]")]
    [AuthorizeActiveUser]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(EmptyResponse), (int)HttpStatusCode.Created)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(EmptyResponse), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(EmptyResponse), (int)HttpStatusCode.Forbidden)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> DeleteForegroundPhoto(
        [FromBody] DeleteForegroundPhotoDto deleteForegroundPhotoDto
    )
    {
        return ResultToActionResult(
            await Mediator
                .Send(new DeleteForegroundPhotoCommand(deleteForegroundPhotoDto))
                .ConfigureAwait(false)
        );
    }

    /// <summary>
    /// Delete background photo
    /// </summary>
    /// <param name="deleteBackgroundPhotoDto"></param>
    /// <returns></returns>
    [HttpPost("[action]")]
    [AuthorizeActiveUser]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(EmptyResponse), (int)HttpStatusCode.Created)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(EmptyResponse), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(EmptyResponse), (int)HttpStatusCode.Forbidden)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> DeleteBackgroundPhoto(
        [FromBody] DeleteBackgroundPhotoDto deleteBackgroundPhotoDto
    )
    {
        return ResultToActionResult(
            await Mediator
                .Send(new DeleteBackgroundPhotoCommand(deleteBackgroundPhotoDto))
                .ConfigureAwait(false)
        );
    }

    /// <summary>
    /// Get user with username
    /// </summary>
    /// <param name="getUserDto"></param>
    /// <returns></returns>
    [HttpPost("[action]")]
    [AuthorizeActiveUser]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(OkResponse<UserDto>), (int)HttpStatusCode.Created)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(EmptyResponse), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(EmptyResponse), (int)HttpStatusCode.Forbidden)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetUser([FromBody] GetUserDto getUserDto)
    {
        return ResultToActionResult(
            await Mediator.Send(new GetUserQuery(getUserDto)).ConfigureAwait(false)
        );
    }

    /// <summary>
    /// Set bio for current user
    /// </summary>
    /// <param name="bio"></param>
    /// <returns></returns>
    [HttpPost("[action]")]
    [AuthorizeActiveUser]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(EmptyResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(EmptyResponse), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(EmptyResponse), (int)HttpStatusCode.Forbidden)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> SetBio([FromBody] SetBioDto setBioDto)
    {
        return ResultToActionResult(
            await Mediator.Send(new SetBioCommand(setBioDto)).ConfigureAwait(false)
        );
    }

    /// <summary>
    /// Set account type for current user
    /// </summary>
    /// <param name="setAccountTypeDto"></param>
    /// <returns></returns>
    [HttpPost("[action]")]
    [AuthorizeActiveUser]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(EmptyResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(EmptyResponse), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(EmptyResponse), (int)HttpStatusCode.Forbidden)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> SetAccountType([FromBody] SetAccountTypeDto setAccountTypeDto)
    {
        return ResultToActionResult(
            await Mediator.Send(new SetAccountTypeCommand(setAccountTypeDto)).ConfigureAwait(false)
        );
    }

    /// <summary>
    /// Set account type for current user
    /// </summary>
    /// <param name="setAccountTypeDto"></param>
    /// <returns></returns>
    [HttpPost("[action]")]
    [AuthorizeActiveUser]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(OkResponse<PagedData<BriefArtistDto>>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(EmptyResponse), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(EmptyResponse), (int)HttpStatusCode.Forbidden)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetTopArtists([FromBody] GetTopArtistsDto getTopArtistsDto)
    {
        return ResultToActionResult(
            await Mediator.Send(new GetTopArtistsQuery(getTopArtistsDto)).ConfigureAwait(false)
        );
    }
}
