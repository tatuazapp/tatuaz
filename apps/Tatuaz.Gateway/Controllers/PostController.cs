using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tatuaz.Gateway.Authorization;
using Tatuaz.Gateway.HttpResponses;
using Tatuaz.Gateway.Requests.Commands.Post;
using Tatuaz.Shared.Domain.Dtos.Dtos.Post;

namespace Tatuaz.Gateway.Controllers;

/// <summary>
/// Controller for posts
/// </summary>
public class PostController : TatuazControllerBase
{
    /// <inheritdoc />
    public PostController(IMediator mediator) : base(mediator)
    {
    }

    /// <summary>
    /// Upload post photos
    /// </summary>
    /// <param name="photos"></param>
    /// <returns></returns>
    [HttpPost("[action]")]
    [AuthorizeActiveUser]
    [Produces("application/json")]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(OkResponse<UploadedPhotosDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(EmptyResponse), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(EmptyResponse), (int)HttpStatusCode.Forbidden)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> UploadPostPhotos([FromForm] UploadPostPhotosDto photos)
    {
        return ResultToActionResult(
            await Mediator.Send(new UploadPostPhotosCommand(photos)).ConfigureAwait(false)
        );
    }

    /// <summary>
    /// Finalize post
    /// </summary>
    /// <param name="finalizePostDto"></param>
    /// <returns></returns>
    [HttpPost("[action]")]
    [AuthorizeActiveUser]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(OkResponse<EmptyResponse>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(EmptyResponse), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(EmptyResponse), (int)HttpStatusCode.Forbidden)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> FinalizePost(FinalizePostDto finalizePostDto)
    {
        return ResultToActionResult(
            await Mediator.Send(new FinalizePostCommand(finalizePostDto)).ConfigureAwait(false)
        );
    }


}
