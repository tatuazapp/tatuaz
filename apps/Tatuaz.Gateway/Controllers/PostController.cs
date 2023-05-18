using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tatuaz.Dashboard.Queue.Contracts.Post;
using Tatuaz.Gateway.Authorization;
using Tatuaz.Gateway.HttpResponses;
using Tatuaz.Gateway.Requests.Commands.Post;
using Tatuaz.Gateway.Requests.Queries.Posts;
using Tatuaz.Shared.Domain.Dtos.Dtos.Post;
using Tatuaz.Shared.Infrastructure.Abstractions.Paging;

namespace Tatuaz.Gateway.Controllers;

/// <summary>
/// Controller for posts
/// </summary>
public class PostController : TatuazControllerBase
{
    /// <inheritdoc />
    public PostController(IMediator mediator)
        : base(mediator) { }

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

    /// <summary>
    /// Search posts
    /// </summary>
    /// <param name="searchPostsDto"></param>
    /// <returns></returns>
    [HttpPost("[action]")]
    [AuthorizeActiveUser]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(OkResponse<PagedData<BriefPostDto>>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(EmptyResponse), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(EmptyResponse), (int)HttpStatusCode.Forbidden)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> SearchPosts(SearchPostsDto searchPostsDto)
    {
        return ResultToActionResult(
            await Mediator.Send(new SearchPostsQuery(searchPostsDto)).ConfigureAwait(false)
        );
    }

    /// <summary>
    /// Like post
    /// </summary>
    /// <param name="likePostDto"></param>
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
    public async Task<IActionResult> LikePost(LikePostDto likePostDto)
    {
        return ResultToActionResult(
            await Mediator.Send(new LikePostCommand(likePostDto)).ConfigureAwait(false)
        );
    }

    /// <summary>
    /// Get user posts
    /// </summary>
    /// <param name="getUserPostsDto"></param>
    /// <returns></returns>
    [HttpPost("[action]")]
    [AuthorizeActiveUser]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(OkResponse<PagedData<BriefPostDto>>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(EmptyResponse), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(EmptyResponse), (int)HttpStatusCode.Forbidden)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetUserPosts(GetUserPostsDto getUserPostsDto)
    {
        return ResultToActionResult(
            await Mediator.Send(new GetUserPostsQuery(getUserPostsDto)).ConfigureAwait(false)
        );
    }
}
