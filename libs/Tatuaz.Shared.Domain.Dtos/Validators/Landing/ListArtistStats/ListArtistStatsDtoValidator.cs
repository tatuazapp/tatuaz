using FluentValidation;
using Tatuaz.Shared.Domain.Dtos.Dtos.Landing.ListArtistStats;
using Tatuaz.Shared.Pipeline.Factories.ErrorCodes.Landing;

namespace Tatuaz.Shared.Domain.Dtos.Validators.Landing.ListArtistStats;

public class ListArtistStatsDtoValidator : AbstractValidator<ListArtistStatsDto>
{
    public ListArtistStatsDtoValidator()
    {
        RuleFor(x => x.Count)
            .NotEmpty()
            .WithErrorCode(ListSummaryStatsErrorCodes.CountEmpty)
            .WithMessage("Count cannot be empty")
            .GreaterThan(0)
            .WithErrorCode(ListSummaryStatsErrorCodes.CountTooLow)
            .WithMessage("Count must be greater than 0")
            .LessThanOrEqualTo(10)
            .WithErrorCode(ListSummaryStatsErrorCodes.CountTooHigh)
            .WithMessage("Count must be less than or equal to 10");
    }
}
