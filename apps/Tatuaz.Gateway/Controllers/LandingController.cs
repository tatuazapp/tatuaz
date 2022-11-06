using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tatuaz.Gateway.HttpResponses;
using Tatuaz.Gateway.Requests.Queries.Landing;
using Tatuaz.Shared.Domain.Dtos.Dtos.Landing;

namespace Tatuaz.Gateway.Controllers;

public class LandingController : TatuazControllerBase
{
    public LandingController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost("[action]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(OkResponse<IEnumerable<StatDto>>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> ListStats([FromBody] ListStatsDto listStatsDto)
    {
        return ResultToActionResult(await Mediator.Send(new ListStatsQuery(listStatsDto)).ConfigureAwait(false));
    }
}
