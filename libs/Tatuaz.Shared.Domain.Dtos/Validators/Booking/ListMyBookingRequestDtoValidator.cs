using FluentValidation;
using Tatuaz.Shared.Domain.Dtos.Dtos.Booking;
using Tatuaz.Shared.Pipeline.Factories.ErrorCodes.Booking;

namespace Tatuaz.Shared.Domain.Dtos.Validators.Booking;

public class ListMyBookingRequestDtoValidator : AbstractValidator<ListMyBookingRequestsDto>
{
    public ListMyBookingRequestDtoValidator()
    {
        RuleFor(x => x.Status).NotNull().WithErrorCode(ListMyBookingRequestErrorCodes.StatusIsNull);

        RuleFor(x => x.PageSize)
            .NotNull()
            .WithErrorCode(ListMyBookingRequestErrorCodes.PageSizeIsNull)
            .WithMessage("PageSize cannot be null");
        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1)
            .WithErrorCode(ListMyBookingRequestErrorCodes.PageSizeIsLessThan1)
            .WithMessage("PageSize must be greater than or equal to 1");
        RuleFor(x => x.PageSize)
            .LessThanOrEqualTo(1000)
            .WithErrorCode(ListMyBookingRequestErrorCodes.PageSizeIsGreaterThan1000)
            .WithMessage("PageSize must be less than or equal to 1000");

        RuleFor(x => x.PageNumber)
            .NotNull()
            .WithErrorCode(ListMyBookingRequestErrorCodes.PageNumberIsNull)
            .WithMessage("PageNumber cannot be null");
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1)
            .WithErrorCode(ListMyBookingRequestErrorCodes.PageNumberIsLessThan1)
            .WithMessage("PageNumber must be greater than or equal to 1");
    }
}
