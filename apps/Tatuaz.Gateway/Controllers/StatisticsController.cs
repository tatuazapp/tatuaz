using System;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tatuaz.Gateway.HttpResponses;
using Tatuaz.Gateway.Requests.Queries.Statistics;
using Tatuaz.Shared.Domain.Dtos.Dtos.Statistics;

namespace Tatuaz.Gateway.Controllers;

/// <summary>
/// Controller for dashboard
/// </summary>
public class StatisticsController : TatuazControllerBase
{
    /// <summary>
    /// Constructor receiving the mediator from DI
    /// </summary>
    /// <param name="mediator"></param>
    public StatisticsController(IMediator mediator)
        : base(mediator) { }

    /// <summary>
    ///     Get number of registered tattoo artists, clients and users.
    ///     A client is a user who has booked at least 1 appointment
    /// </summary>
    /// <returns>RegisteredStatsDto</returns>
    [HttpPost("[action]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(OkResponse<RegisteredStatsDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetRegisteredStats()
    {
        return ResultToActionResult(
            await Mediator.Send(new GetRegisteredStatsQuery()).ConfigureAwait(false)
        );
    }
}
