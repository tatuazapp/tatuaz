using FluentValidation;
using Tatuaz.Shared.Domain.Dtos.Dtos.Booking;
using Tatuaz.Shared.Pipeline.Factories.ErrorCodes.Booking;

namespace Tatuaz.Shared.Domain.Dtos.Validators.Booking;

public class RespondToBookingRequestDtoValidator : AbstractValidator<RespondToBookingRequestDto>
{
    public RespondToBookingRequestDtoValidator()
    {
        RuleFor(x => x.BookingRequestId)
            .NotNull()
            .WithErrorCode(RespondToBookingRequestDtoValidatorErrorCodes.BookingRequestIdIsNull)
            .WithMessage("BookingRequestId cannot be null");

        RuleFor(x => x.Accept)
            .NotNull()
            .WithErrorCode(RespondToBookingRequestDtoValidatorErrorCodes.AcceptIsNull)
            .WithMessage("Accept cannot be null");
    }
}
