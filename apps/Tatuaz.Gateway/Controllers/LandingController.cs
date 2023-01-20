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

/// <summary>
/// Controlling receiving requests for landing page
/// </summary>
public class LandingController : TatuazControllerBase
{
    /// <inheritdoc />
    public LandingController(IMediator mediator) : base(mediator) { }

    /// <summary>
    /// Get summary stats for landing page
    /// </summary>
    /// <param name="listSummaryStatsDto"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Get artist stats for landing page
    /// </summary>
    /// <param name="listArtistStatsDto"></param>
    /// <returns></returns>
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
