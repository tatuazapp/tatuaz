using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using NodaTime.Extensions;
using Tatuaz.Dashboard.Queue.Contracts.Booking;
using Tatuaz.Dashboard.Queue.Producers.Booking;
using Tatuaz.Gateway.Requests.Commands.Booking;
using Tatuaz.Shared.Domain.Dtos.Dtos.Booking;
using Tatuaz.Shared.Domain.Dtos.Dtos.Common;
using Tatuaz.Shared.Pipeline.Factories.Results;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Gateway.Handlers.Commands.Booking;

public class SendBookingRequestCommandHandler
    : IRequestHandler<SendBookingRequestCommand, TatuazResult<EmptyDto>>
{
    private readonly IValidator<SendBookingRequestDto> _validator;
    private readonly SendBookingRequestProducer _producer;

    public SendBookingRequestCommandHandler(
        IValidator<SendBookingRequestDto> validator,
        SendBookingRequestProducer producer
    )
    {
        _validator = validator;
        _producer = producer;
    }

    public async Task<TatuazResult<EmptyDto>> Handle(
        SendBookingRequestCommand request,
        CancellationToken cancellationToken
    )
    {
        var validationResult = await _validator
            .ValidateAsync(request.SendBookingRequestDto, cancellationToken)
            .ConfigureAwait(false);

        if (!validationResult.IsValid)
        {
            return CommonResultFactory.ValidationError<EmptyDto>(validationResult);
        }

        return await _producer
            .Send(
                new SendBookingRequest(
                    request.SendBookingRequestDto.ArtistName!,
                    request.SendBookingRequestDto.Start!.Value.ToInstant(),
                    request.SendBookingRequestDto.End!.Value.ToInstant(),
                    request.SendBookingRequestDto.Comment
                ),
                cancellationToken
            )
            .ConfigureAwait(false);
    }
}
