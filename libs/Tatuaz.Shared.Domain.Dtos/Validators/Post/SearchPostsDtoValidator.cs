using FluentValidation;
using Tatuaz.Shared.Domain.Dtos.Dtos.Post;
using Tatuaz.Shared.Pipeline.Factories.ErrorCodes.Post;

namespace Tatuaz.Shared.Domain.Dtos.Validators.Post;

public class SearchPostsDtoValidator : AbstractValidator<SearchPostsDto>
{
    public SearchPostsDtoValidator()
    {
        RuleFor(x => x.Query)
            .NotNull()
            .WithErrorCode(SearchPostsErrorCodes.QueryNull)
            .WithMessage("Query cannot be null");

        RuleFor(x => x.Query)
            .MaximumLength(128)
            .WithErrorCode(SearchPostsErrorCodes.QueryTooLong)
            .WithMessage("Query cannot be longer than 128 characters");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1)
            .WithErrorCode(SearchPostsErrorCodes.PageSizeIsLessThan1)
            .WithMessage("PageSize must be greater than or equal to 1");

        RuleFor(x => x.PageSize)
            .LessThanOrEqualTo(1000)
            .WithErrorCode(SearchPostsErrorCodes.PageSizeIsGreaterThan1000)
            .WithMessage("PageSize must be less than or equal to 1000");

        RuleFor(x => x.PageNumber)
            .NotNull()
            .WithErrorCode(SearchPostsErrorCodes.PageNumberIsNull)
            .WithMessage("PageNumber cannot be null");

        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1)
            .WithErrorCode(SearchPostsErrorCodes.PageNumberIsLessThan1)
            .WithMessage("PageNumber must be greater than or equal to 1");

        RuleFor(x => x.SearchPostsFlag)
            .NotNull()
            .WithErrorCode(SearchPostsErrorCodes.SearchPostsFlagIsNull)
            .WithMessage("SearchPostsFlag cannot be null");
    }
}
