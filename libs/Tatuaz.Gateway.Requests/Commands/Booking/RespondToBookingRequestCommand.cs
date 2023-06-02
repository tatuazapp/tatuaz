using MediatR;
using Tatuaz.Shared.Domain.Dtos.Dtos.Booking;
using Tatuaz.Shared.Domain.Dtos.Dtos.Common;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Gateway.Requests.Commands.Booking;

public record RespondToBookingRequestCommand(RespondToBookingRequestDto RespondToBookingRequestDto)
    : IRequest<TatuazResult<EmptyDto>>;
