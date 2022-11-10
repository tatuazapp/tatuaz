using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tatuaz.Gateway.HttpResponses;
using Tatuaz.Gateway.Requests.Queries.Landing;
using Tatuaz.Shared.Domain.Dtos.Dtos.Landing.ListArtistStats;
using Tatuaz.Shared.Domain.Dtos.Dtos.Landing.ListSummaryStats;

namespace Tatuaz.Gateway.Controllers;

public class LandingController : TatuazControllerBase
{
    public LandingController(IMediator mediator) : base(mediator) { }

    [HttpPost("[action]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(OkResponse<IEnumerable<SummaryStatDto>>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> ListSummaryStats(
        [FromBody] ListSummaryStatsDto listSummaryStatsDto
    )
    {
        return ResultToActionResult(
            await Mediator
                .Send(new ListSummaryStatsQuery(listSummaryStatsDto))
                .ConfigureAwait(false)
        );
    }

    [HttpPost("[action]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(OkResponse<IEnumerable<SummaryStatDto>>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> ListArtistStats(
        [FromBody] ListArtistStatsDto listArtistStatsDto
    )
    {
        return ResultToActionResult(
            await Mediator.Send(new ListArtistStatsQuery(listArtistStatsDto)).ConfigureAwait(false)
        );
    }
}
