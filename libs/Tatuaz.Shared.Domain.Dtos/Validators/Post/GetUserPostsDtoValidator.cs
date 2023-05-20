using FluentValidation;
using Tatuaz.Shared.Domain.Dtos.Dtos.Post;
using Tatuaz.Shared.Pipeline.Factories.ErrorCodes.Post;

namespace Tatuaz.Shared.Domain.Dtos.Validators.Post;

public class GetUserPostsDtoValidator : AbstractValidator<GetUserPostsDto>
{
    public GetUserPostsDtoValidator()
    {
        RuleFor(x => x.Username)
            .NotNull()
            .WithErrorCode(GetUserPostsErrorCodes.UsernameIsNull)
            .WithMessage("Username cannot be null");

        RuleFor(x => x.PageSize)
            .NotNull()
            .WithErrorCode(GetUserPostsErrorCodes.PageSizeIsNull)
            .WithMessage("PageSize cannot be null");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1)
            .WithErrorCode(GetUserPostsErrorCodes.PageSizeIsLessThan1)
            .WithMessage("PageSize must be greater than or equal to 1");

        RuleFor(x => x.PageSize)
            .LessThanOrEqualTo(1000)
            .WithErrorCode(GetUserPostsErrorCodes.PageSizeIsGreaterThan1000)
            .WithMessage("PageSize must be less than or equal to 1000");

        RuleFor(x => x.PageNumber)
            .NotNull()
            .WithErrorCode(GetUserPostsErrorCodes.PageNumberIsNull)
            .WithMessage("PageNumber cannot be null");

        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1)
            .WithErrorCode(GetUserPostsErrorCodes.PageNumberIsLessThan1)
            .WithMessage("PageNumber must be greater than or equal to 1");
    }
}
