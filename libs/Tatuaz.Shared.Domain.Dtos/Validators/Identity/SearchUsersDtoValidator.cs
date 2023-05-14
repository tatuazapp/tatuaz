using FluentValidation;
using Tatuaz.Shared.Domain.Dtos.Dtos.Identity;
using Tatuaz.Shared.Pipeline.Factories.ErrorCodes.Identity;

namespace Tatuaz.Shared.Domain.Dtos.Validators.Identity;

public class SearchUsersDtoValidator : AbstractValidator<SearchUsersDto>
{
    public SearchUsersDtoValidator()
    {
        RuleFor(x => x.Query)
            .NotNull()
            .WithErrorCode(SearchUsersErrorCodes.QueryNull)
            .WithMessage("Query cannot be empty.");

        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1)
            .WithErrorCode(SearchUsersErrorCodes.PageNumberLessThan1)
            .WithMessage("Page number cannot be less than 1.");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1)
            .WithErrorCode(SearchUsersErrorCodes.PageSizeLessThan1)
            .WithMessage("Page size cannot be less than 1.");

        RuleFor(x => x.PageSize)
            .LessThanOrEqualTo(1000)
            .WithErrorCode(SearchUsersErrorCodes.PageSizeGreaterThan1000)
            .WithMessage("Page size cannot be greater than 1000.");

        RuleFor(x => x.OnlyArtists)
            .NotNull()
            .WithErrorCode(SearchUsersErrorCodes.OnlyArtistsNull)
            .WithMessage("Only artists cannot be null.");
    }
}
