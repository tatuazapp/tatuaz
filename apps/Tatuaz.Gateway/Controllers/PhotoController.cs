using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tatuaz.Gateway.HttpResponses;
using Tatuaz.Gateway.Requests.Queries.Photo;
using Tatuaz.Shared.Domain.Dtos.Dtos.Identity;
using Tatuaz.Shared.Domain.Dtos.Dtos.Photo.PhotoCategory;
using Tatuaz.Shared.Infrastructure.Abstractions.Paging;

namespace Tatuaz.Gateway.Controllers;

/// <summary>
/// Controller for photo categories
/// </summary>
public class PhotoController : TatuazControllerBase
{
    /// <summary>
    /// Constructor receiving the mediator from DI
    /// </summary>
    /// <param name="mediator"></param>
    public PhotoController(IMediator mediator)
        : base(mediator) { }

    [HttpPost("[action]")]
    [Authorize]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(
        typeof(OkResponse<PagedData<PhotoCategoryDto>>),
        (int)HttpStatusCode.Created
    )]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(EmptyResponse), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> ListPhotoCategories(
        [FromBody] ListPhotoCategoriesDto listPhotoCategoriesDto
    )
    {
        return ResultToActionResult(
            await Mediator
                .Send(new ListPhotoCategoriesQuery(listPhotoCategoriesDto))
                .ConfigureAwait(false)
        );
    }
}
