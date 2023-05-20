using FluentValidation;
using Tatuaz.Shared.Domain.Dtos.Dtos.Identity;
using Tatuaz.Shared.Pipeline.Factories.ErrorCodes.Identity;

namespace Tatuaz.Shared.Domain.Dtos.Validators.Identity;

public class GetTopArtistsDtoValidator : AbstractValidator<GetTopArtistsDto>
{
    public GetTopArtistsDtoValidator()
    {
        RuleFor(x => x.PageSize)
            .NotNull()
            .WithErrorCode(GetTopArtistsErrorCodes.PageSizeIsNull)
            .WithMessage("PageSize cannot be null");
        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1)
            .WithErrorCode(GetTopArtistsErrorCodes.PageSizeIsLessThan1)
            .WithMessage("PageSize must be greater than or equal to 1");
        RuleFor(x => x.PageSize)
            .LessThanOrEqualTo(1000)
            .WithErrorCode(GetTopArtistsErrorCodes.PageSizeIsGreaterThan1000)
            .WithMessage("PageSize must be less than or equal to 1000");

        RuleFor(x => x.PageNumber)
            .NotNull()
            .WithErrorCode(GetTopArtistsErrorCodes.PageNumberIsNull)
            .WithMessage("PageNumber cannot be null");
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1)
            .WithErrorCode(GetTopArtistsErrorCodes.PageNumberIsLessThan1)
            .WithMessage("PageNumber must be greater than or equal to 1");
    }
}
