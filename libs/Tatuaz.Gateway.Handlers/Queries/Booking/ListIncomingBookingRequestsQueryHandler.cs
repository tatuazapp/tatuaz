using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Tatuaz.Dashboard.Queue.Contracts.Booking;
using Tatuaz.Dashboard.Queue.Producers.Booking;
using Tatuaz.Gateway.Requests.Queries.Booking;
using Tatuaz.Shared.Domain.Dtos.Dtos.Booking;
using Tatuaz.Shared.Infrastructure.Abstractions.Paging;
using Tatuaz.Shared.Pipeline.Factories.Results;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Gateway.Handlers.Queries.Booking;

public class ListIncomingBookingRequestsQueryHandler
    : IRequestHandler<ListIncomingBookingRequestsQuery, TatuazResult<PagedData<BookingRequestDto>>>
{
    private readonly IValidator<ListIncomingBookingRequestsDto> _validator;
    private readonly ListIncomingBookingRequestsProducer _producer;

    public ListIncomingBookingRequestsQueryHandler(
        IValidator<ListIncomingBookingRequestsDto> validator,
        ListIncomingBookingRequestsProducer producer
    )
    {
        _validator = validator;
        _producer = producer;
    }

    public async Task<TatuazResult<PagedData<BookingRequestDto>>> Handle(
        ListIncomingBookingRequestsQuery request,
        CancellationToken cancellationToken
    )
    {
        var validationResult = await _validator
            .ValidateAsync(request.ListIncomingBookingRequestsDto, cancellationToken)
            .ConfigureAwait(false);

        if (!validationResult.IsValid)
        {
            return CommonResultFactory.ValidationError<PagedData<BookingRequestDto>>(
                validationResult
            );
        }

        return await _producer
            .Send(
                new ListIncomingBookingRequests(
                    request.ListIncomingBookingRequestsDto.PageSize!.Value,
                    request.ListIncomingBookingRequestsDto.PageNumber!.Value,
                    request.ListIncomingBookingRequestsDto.Status!.Value
                ),
                cancellationToken
            )
            .ConfigureAwait(false);
    }
}
