using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Tatuaz.Dashboard.Queue.Contracts.Booking;
using Tatuaz.Dashboard.Queue.Producers.Booking;
using Tatuaz.Gateway.Requests.Commands.Booking;
using Tatuaz.Shared.Domain.Dtos.Dtos.Booking;
using Tatuaz.Shared.Domain.Dtos.Dtos.Common;
using Tatuaz.Shared.Pipeline.Factories.Results;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Gateway.Handlers.Commands.Booking;

public class RespondToBookingRequestCommandHandler
    : IRequestHandler<RespondToBookingRequestCommand, TatuazResult<EmptyDto>>
{
    private readonly IValidator<RespondToBookingRequestDto> _validator;
    private readonly RespondToBookingRequestProducer _producer;

    public RespondToBookingRequestCommandHandler(
        IValidator<RespondToBookingRequestDto> validator,
        RespondToBookingRequestProducer producer
    )
    {
        _validator = validator;
        _producer = producer;
    }

    public async Task<TatuazResult<EmptyDto>> Handle(
        RespondToBookingRequestCommand request,
        CancellationToken cancellationToken
    )
    {
        var validationResult = await _validator
            .ValidateAsync(request.RespondToBookingRequestDto, cancellationToken)
            .ConfigureAwait(false);

        if (!validationResult.IsValid)
        {
            return CommonResultFactory.ValidationError<EmptyDto>(validationResult);
        }

        return await _producer
            .Send(
                new RespondToBookingRequest(
                    request.RespondToBookingRequestDto.BookingRequestId!.Value,
                    request.RespondToBookingRequestDto.Accept!.Value
                ),
                cancellationToken
            )
            .ConfigureAwait(false);
    }
}
