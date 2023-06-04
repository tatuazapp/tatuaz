using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tatuaz.Gateway.Authorization;
using Tatuaz.Gateway.HttpResponses;
using Tatuaz.Gateway.Requests.Commands.Booking;
using Tatuaz.Gateway.Requests.Queries.Booking;
using Tatuaz.Shared.Domain.Dtos.Dtos.Booking;
using Tatuaz.Shared.Infrastructure.Abstractions.Paging;

namespace Tatuaz.Gateway.Controllers;

public class BookingController : TatuazControllerBase
{
    public BookingController(IMediator mediator)
        : base(mediator) { }

    /// <summary>
    /// Send booking request. Limit 1024 znaki na komentarz jak coś B-)
    /// </summary>
    /// <param name="sendBookingRequestDto"></param>
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
    public async Task<IActionResult> SendBookingRequest(SendBookingRequestDto sendBookingRequestDto)
    {
        return ResultToActionResult(
            await Mediator
                .Send(new SendBookingRequestCommand(sendBookingRequestDto))
                .ConfigureAwait(false)
        );
    }

    /// <summary>
    /// List my booking requests
    /// </summary>
    /// <param name="listMyBookingRequestsDto"></param>
    /// <returns></returns>
    [HttpPost("[action]")]
    [AuthorizeActiveUser]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(OkResponse<PagedData<BookingRequestDto>>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(EmptyResponse), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(EmptyResponse), (int)HttpStatusCode.Forbidden)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> ListMyBookingRequests(
        ListMyBookingRequestsDto listMyBookingRequestsDto
    )
    {
        return ResultToActionResult(
            await Mediator
                .Send(new ListMyBookingRequestsQuery(listMyBookingRequestsDto))
                .ConfigureAwait(false)
        );
    }

    /// <summary>
    /// List incoming booking requests
    /// </summary>
    /// <param name="listIncomingBookingRequestsDto"></param>
    /// <returns></returns>
    [HttpPost("[action]")]
    [AuthorizeActiveUser]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(OkResponse<PagedData<BookingRequestDto>>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(EmptyResponse), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(EmptyResponse), (int)HttpStatusCode.Forbidden)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> ListIncomingBookingRequests(
        ListIncomingBookingRequestsDto listIncomingBookingRequestsDto
    )
    {
        return ResultToActionResult(
            await Mediator
                .Send(new ListIncomingBookingRequestsQuery(listIncomingBookingRequestsDto))
                .ConfigureAwait(false)
        );
    }

    /// <summary>
    /// Respond to booking request
    /// </summary>
    /// <param name="respondToBookingRequestDto"></param>
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
    public async Task<IActionResult> RespondToBookingRequest(
        RespondToBookingRequestDto respondToBookingRequestDto
    )
    {
        return ResultToActionResult(
            await Mediator
                .Send(new RespondToBookingRequestCommand(respondToBookingRequestDto))
                .ConfigureAwait(false)
        );
    }
}
