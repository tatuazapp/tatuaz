using FluentValidation;
using Tatuaz.Shared.Domain.Dtos.Dtos.Post;
using Tatuaz.Shared.Pipeline.Factories.ErrorCodes.Post;

namespace Tatuaz.Shared.Domain.Dtos.Validators.Post;

public class GetPostFeedDtoValidator : AbstractValidator<GetPostFeedDto>
{
    public GetPostFeedDtoValidator()
    {
        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1)
            .WithErrorCode(GetPostFeedErrorCodes.PageSizeIsLessThan1)
            .WithMessage("PageSize must be greater than or equal to 1");

        RuleFor(x => x.PageSize)
            .LessThanOrEqualTo(1000)
            .WithErrorCode(GetPostFeedErrorCodes.PageSizeIsGreaterThan1000)
            .WithMessage("PageSize must be less than or equal to 1000");

        RuleFor(x => x.PageNumber)
            .NotNull()
            .WithErrorCode(GetPostFeedErrorCodes.PageNumberIsNull)
            .WithMessage("PageNumber cannot be null");

        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1)
            .WithErrorCode(GetPostFeedErrorCodes.PageNumberIsLessThan1)
            .WithMessage("PageNumber must be greater than or equal to 1");

        RuleFor(x => x.SearchPostsFlag)
            .NotNull()
            .WithErrorCode(GetPostFeedErrorCodes.SearchPostsFlagIsNull)
            .WithMessage("SearchPostsFlag cannot be null");
    }
}
