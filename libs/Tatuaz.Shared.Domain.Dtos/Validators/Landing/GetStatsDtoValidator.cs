using FluentValidation;
using Tatuaz.Shared.Domain.Dtos.Dtos.Landing;
using Tatuaz.Shared.Pipeline.Factories.ErrorCodes.Landing;

namespace Tatuaz.Shared.Domain.Dtos.Validators.Landing;

public class GetStatsDtoValidator : AbstractValidator<ListStatsDto>
{
    public GetStatsDtoValidator()
    {
        RuleFor(x => x.TimePeriod)
            .NotEmpty()
            .WithErrorCode(GetStatsErrorCodes.TimePeriodEmpty)
            .WithMessage("TimePeriod must be not empty")
            .IsInEnum()
            .WithErrorCode(GetStatsErrorCodes.TimePeriodInvalid)
            .WithErrorCode("TimePeriod must be valid enum")
            ;

        RuleFor(x => x.Count)
            .NotEmpty()
            .WithErrorCode(GetStatsErrorCodes.CountEmpty)
            .WithMessage("Count cannot be empty")
            .GreaterThan(0)
            .WithErrorCode(GetStatsErrorCodes.CountTooLow)
            .WithMessage("Count must be greater than 0")
            .LessThanOrEqualTo(10)
            .WithErrorCode(GetStatsErrorCodes.CountTooHigh)
            .WithMessage("Count must be less than or equal to 10");
    }
}