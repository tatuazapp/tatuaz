using FluentValidation;
using Tatuaz.Shared.Domain.Dtos.Dtos.Photo.Category;
using Tatuaz.Shared.Pipeline.Factories.ErrorCodes.Photo;

namespace Tatuaz.Shared.Domain.Dtos.Validators.Photo.Category;

public class ListCategoriesDtoValidator : AbstractValidator<ListCategoriesDto>
{
    public ListCategoriesDtoValidator()
    {
        RuleFor(x => x.PageSize)
            .NotNull()
            .WithErrorCode(ListCategoriesErrorCodes.PageSizeIsNull)
            .WithMessage("PageSize cannot be null");
        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1)
            .WithErrorCode(ListCategoriesErrorCodes.PageSizeIsLessThan1)
            .WithMessage("PageSize must be greater than or equal to 1");
        RuleFor(x => x.PageSize)
            .LessThanOrEqualTo(1000)
            .WithErrorCode(ListCategoriesErrorCodes.PageSizeIsGreaterThan1000)
            .WithMessage("PageSize must be less than or equal to 1000");

        RuleFor(x => x.PageNumber)
            .NotNull()
            .WithErrorCode(ListCategoriesErrorCodes.PageNumberIsNull)
            .WithMessage("PageNumber cannot be null");
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1)
            .WithErrorCode(ListCategoriesErrorCodes.PageNumberIsLessThan1)
            .WithMessage("PageNumber must be greater than or equal to 1");
    }
}
