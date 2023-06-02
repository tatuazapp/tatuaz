using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Tatuaz.Dashboard.Queue.Contracts.Booking;
using Tatuaz.Dashboard.Queue.Producers.Booking;
using Tatuaz.Gateway.Requests.Commands.Booking;
using Tatuaz.Gateway.Requests.Queries.Booking;
using Tatuaz.Shared.Domain.Dtos.Dtos.Booking;
using Tatuaz.Shared.Infrastructure.Abstractions.Paging;
using Tatuaz.Shared.Pipeline.Factories.Results;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Gateway.Handlers.Queries.Booking;

public class ListMyBookingRequestsQueryHandler
    : IRequestHandler<ListMyBookingRequestsQuery, TatuazResult<PagedData<BookingRequestDto>>>
{
    private readonly IValidator<ListMyBookingRequestsDto> _validator;
    private readonly ListMyBookingRequestsProducer _producer;

    public ListMyBookingRequestsQueryHandler(
        IValidator<ListMyBookingRequestsDto> validator,
        ListMyBookingRequestsProducer producer
    )
    {
        _validator = validator;
        _producer = producer;
    }

    public async Task<TatuazResult<PagedData<BookingRequestDto>>> Handle(
        ListMyBookingRequestsQuery request,
        CancellationToken cancellationToken
    )
    {
        var validationResult = await _validator
            .ValidateAsync(request.ListMyBookingRequestsDto, cancellationToken)
            .ConfigureAwait(false);

        if (!validationResult.IsValid)
        {
            return CommonResultFactory.ValidationError<PagedData<BookingRequestDto>>(
                validationResult
            );
        }

        return await _producer
            .Send(
                new ListMyBookingRequests(
                    request.ListMyBookingRequestsDto.PageSize!.Value,
                    request.ListMyBookingRequestsDto.PageNumber!.Value,
                    request.ListMyBookingRequestsDto.Status!.Value
                ),
                cancellationToken
            )
            .ConfigureAwait(false);
    }
}
