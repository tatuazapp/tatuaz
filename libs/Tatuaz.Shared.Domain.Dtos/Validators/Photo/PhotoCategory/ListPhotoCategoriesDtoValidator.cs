using FluentValidation;
using Tatuaz.Shared.Domain.Dtos.Dtos.Photo.PhotoCategory;
using Tatuaz.Shared.Pipeline.Factories.ErrorCodes.Photo;

namespace Tatuaz.Shared.Domain.Dtos.Validators.Photo.PhotoCategory;

public class ListPhotoCategoriesDtoValidator : AbstractValidator<ListPhotoCategoriesDto>
{
    public ListPhotoCategoriesDtoValidator()
    {
        RuleFor(x => x.PageSize)
            .NotNull()
            .WithErrorCode(ListPhotoCategoriesErrorCodes.PageSizeIsNull)
            .WithMessage("PageSize cannot be null")
            .GreaterThanOrEqualTo(1)
            .WithErrorCode(ListPhotoCategoriesErrorCodes.PageSizeIsLessThan1)
            .WithMessage("PageSize must be greater than or equal to 1")
            .LessThanOrEqualTo(1000)
            .WithErrorCode(ListPhotoCategoriesErrorCodes.PageSizeIsGreaterThan1000)
            .WithMessage("PageSize must be less than or equal to 1000");

        RuleFor(x => x.PageNumber)
            .NotNull()
            .WithErrorCode(ListPhotoCategoriesErrorCodes.PageNumberIsNull)
            .WithMessage("PageNumber cannot be null")
            .GreaterThanOrEqualTo(1)
            .WithErrorCode(ListPhotoCategoriesErrorCodes.PageNumberIsLessThan1)
            .WithMessage("PageNumber must be greater than or equal to 1");
    }
}
