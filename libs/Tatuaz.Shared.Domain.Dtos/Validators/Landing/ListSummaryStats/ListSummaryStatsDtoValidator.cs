using FluentValidation;
using Tatuaz.Shared.Domain.Dtos.Dtos.Landing.ListSummaryStats;
using Tatuaz.Shared.Pipeline.Factories.ErrorCodes.Landing;

namespace Tatuaz.Shared.Domain.Dtos.Validators.Landing.ListSummaryStats;

public class ListSummaryStatsDtoValidator : AbstractValidator<ListSummaryStatsDto>
{
    public ListSummaryStatsDtoValidator()
    {
        RuleFor(x => x.TimePeriod)
            .NotNull()
            .WithErrorCode(ListSummaryStatsErrorCodes.TimePeriodNull)
            .WithMessage("TimePeriod cannot be null")
            .IsInEnum()
            .WithErrorCode(ListSummaryStatsErrorCodes.TimePeriodInvalid)
            .WithErrorCode("TimePeriod must be valid enum");

        RuleFor(x => x.Count)
            .NotNull()
            .WithErrorCode(ListSummaryStatsErrorCodes.CountNull)
            .WithMessage("Count cannot be null")
            .GreaterThan(0)
            .WithErrorCode(ListSummaryStatsErrorCodes.CountTooLow)
            .WithMessage("Count must be greater than 0")
            .LessThanOrEqualTo(10)
            .WithErrorCode(ListSummaryStatsErrorCodes.CountTooHigh)
            .WithMessage("Count must be less than or equal to 10");
    }
}
