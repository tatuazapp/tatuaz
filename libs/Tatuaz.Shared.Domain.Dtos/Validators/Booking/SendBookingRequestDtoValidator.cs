using System;
using FluentValidation;
using NodaTime;
using Tatuaz.Shared.Domain.Dtos.Dtos.Booking;
using Tatuaz.Shared.Pipeline.Factories.ErrorCodes.Booking;

namespace Tatuaz.Shared.Domain.Dtos.Validators.Booking;

public class SendBookingRequestDtoValidator : AbstractValidator<SendBookingRequestDto>
{
    public SendBookingRequestDtoValidator()
    {
        RuleFor(x => x.ArtistName)
            .NotNull()
            .WithErrorCode(SendBookingRequestErrorCodes.ArtistNameIsNull)
            .WithMessage("ArtistName cannot be null");

        RuleFor(x => x.Start)
            .NotNull()
            .WithErrorCode(SendBookingRequestErrorCodes.StartIsNull)
            .WithMessage("Start cannot be null");

        RuleFor(x => x.Start)
            .Must((dto, start) => start < dto.End)
            .When(x => x.Start.HasValue && x.End.HasValue)
            .WithErrorCode(SendBookingRequestErrorCodes.StartIsGreaterThanEnd)
            .WithMessage("Start must be less than End");

        RuleFor(x => x.Start)
            .Must(start => start.Value.Kind == DateTimeKind.Utc)
            .When(x => x.Start.HasValue)
            .WithErrorCode(SendBookingRequestErrorCodes.StartIsNotUtc)
            .WithMessage("Start must be UTC");

        RuleFor(x => x.End)
            .Must(end => end.Value.Kind == DateTimeKind.Utc)
            .When(x => x.End.HasValue)
            .WithErrorCode(SendBookingRequestErrorCodes.EndIsNotUtc)
            .WithMessage("End must be UTC");

        RuleFor(x => x.End)
            .NotNull()
            .WithErrorCode(SendBookingRequestErrorCodes.EndIsNull)
            .WithMessage("End cannot be null");

        RuleFor(x => x.Comment)
            .MaximumLength(1024)
            .WithErrorCode(SendBookingRequestErrorCodes.CommentIsTooLong)
            .WithMessage("Comment cannot be longer than 1024 characters");
    }
}
