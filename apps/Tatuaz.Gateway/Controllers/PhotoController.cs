using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tatuaz.Gateway.HttpResponses;
using Tatuaz.Gateway.Requests.Queries.Photo;
using Tatuaz.Shared.Domain.Dtos.Dtos.Photo.Category;
using Tatuaz.Shared.Infrastructure.Abstractions.Paging;

namespace Tatuaz.Gateway.Controllers;

/// <summary>
/// Controller for photos
/// </summary>
public class PhotoController : TatuazControllerBase
{
    /// <inheritdoc />
    public PhotoController(IMediator mediator)
        : base(mediator)
    {
    }

    [HttpPost("[action]")]
    [Authorize]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(OkResponse<PagedData<CategoryDto>>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(EmptyResponse), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(EmptyResponse), (int)HttpStatusCode.Forbidden)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> ListCategories([FromBody] ListCategoriesDto listCategoriesDto)
    {
        return ResultToActionResult(
            await Mediator.Send(new ListCategoriesQuery(listCategoriesDto)).ConfigureAwait(false)
        );
    }
}
