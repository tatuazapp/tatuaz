using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tatuaz.Gateway.Authorization;
using Tatuaz.Gateway.HttpResponses;
using Tatuaz.Gateway.Requests.Commands.Comment;
using Tatuaz.Gateway.Requests.Commands.Post;
using Tatuaz.Shared.Domain.Dtos.Dtos.Comment;
using Tatuaz.Shared.Domain.Dtos.Dtos.Common;
using Tatuaz.Shared.Domain.Dtos.Dtos.Post;

namespace Tatuaz.Gateway.Controllers;

/// <summary>
/// Controller for comments under posts
/// </summary>
public class CommentController : TatuazControllerBase
{
    /// <inheritdoc />
    public CommentController(IMediator mediator)
        : base(mediator) { }

    /// <summary>
    /// Submit comments
    /// </summary>
    /// <returns></returns>
    [HttpPost("[action]")]
    [AuthorizeActiveUser]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(OkResponse<SubmittedCommentDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(EmptyResponse), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(EmptyResponse), (int)HttpStatusCode.Forbidden)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> SubmitComment(SubmitCommentDto comment)
    {
        return ResultToActionResult(
            await Mediator.Send(new SubmitCommentCommand(comment)).ConfigureAwait(false)
        );
    }
}
