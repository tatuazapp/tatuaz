using FluentValidation;
using Tatuaz.Shared.Domain.Dtos.Dtos.Booking;
using Tatuaz.Shared.Pipeline.Factories.ErrorCodes.Booking;

namespace Tatuaz.Shared.Domain.Dtos.Validators.Booking;

public class ListIncomingBookingRequestsDtoValidator
    : AbstractValidator<ListIncomingBookingRequestsDto>
{
    public ListIncomingBookingRequestsDtoValidator()
    {
        RuleFor(x => x.Status)
            .NotNull()
            .WithErrorCode(ListIncomingBookingRequestsErrorCodes.StatusIsNull);

        RuleFor(x => x.PageSize)
            .NotNull()
            .WithErrorCode(ListIncomingBookingRequestsErrorCodes.PageSizeIsNull)
            .WithMessage("PageSize cannot be null");
        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1)
            .WithErrorCode(ListIncomingBookingRequestsErrorCodes.PageSizeIsLessThan1)
            .WithMessage("PageSize must be greater than or equal to 1");
        RuleFor(x => x.PageSize)
            .LessThanOrEqualTo(1000)
            .WithErrorCode(ListIncomingBookingRequestsErrorCodes.PageSizeIsGreaterThan1000)
            .WithMessage("PageSize must be less than or equal to 1000");

        RuleFor(x => x.PageNumber)
            .NotNull()
            .WithErrorCode(ListIncomingBookingRequestsErrorCodes.PageNumberIsNull)
            .WithMessage("PageNumber cannot be null");
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1)
            .WithErrorCode(ListIncomingBookingRequestsErrorCodes.PageNumberIsLessThan1)
            .WithMessage("PageNumber must be greater than or equal to 1");
    }
}
