using System;
using System.Linq;
using FluentValidation;
using Tatuaz.Shared.Domain.Dtos.Dtos.Post;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Photo;
using Tatuaz.Shared.Domain.Entities.Models.Photo;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;
using Tatuaz.Shared.Infrastructure.Specification;
using Tatuaz.Shared.Pipeline.Factories.ErrorCodes.Post;

namespace Tatuaz.Shared.Domain.Dtos.Validators.Post;

public class FinalizePostDtoValidator : AbstractValidator<FinalizePostDto>
{
    public FinalizePostDtoValidator(
        IGenericRepository<Category, HistCategory, int> categoryRepository
    )
    {
        RuleFor(x => x.Description)
            .NotNull()
            .WithErrorCode(FinalizePostErrorCodes.DescriptionIsNull)
            .WithMessage("Description cannot be null");

        RuleFor(x => x.Description)
            .MaximumLength(4096)
            .WithErrorCode(FinalizePostErrorCodes.DescriptionIsTooLong)
            .WithMessage("Description cannot be longer than 4096 characters");

        RuleFor(x => x.PhotoInfoDtos)
            .NotNull()
            .WithErrorCode(FinalizePostErrorCodes.PhotoInfoDtosIsNull)
            .WithMessage("PhotoInfoDtos cannot be null");

        RuleFor(x => x.PhotoInfoDtos)
            .Must(x => x.Length <= 10)
            .When(x => x.PhotoInfoDtos != null)
            .WithErrorCode(FinalizePostErrorCodes.PhotoInfoDtosTooMany)
            .WithMessage("PhotoInfoDtos cannot be longer than 10");

        RuleFor(x => x.PhotoInfoDtos)
            .Must(photoInfoDtos =>
            {
                foreach (var photoInfoDto in photoInfoDtos)
                {
                    if (
                        photoInfoDto.CategoryIds.Length
                        != photoInfoDto.CategoryIds.Distinct().ToArray().Length
                    )
                    {
                        return false;
                    }
                }

                return true;
            })
            .When(x => x.PhotoInfoDtos != null)
            .WithErrorCode(FinalizePostErrorCodes.PhotoInfoDtosHasDuplicateCategoryIds)
            .WithMessage("PhotoInfoDtos cannot have duplicate category ids");

        RuleFor(x => x.PhotoInfoDtos)
            .MustAsync(
                async (photoInfoDto, ct) =>
                {
                    var spec = new FullSpecification<Category>();
                    var categoryIdsInDb = (
                        await categoryRepository
                            .GetBySpecificationAsync(spec, ct)
                            .ConfigureAwait(false)
                    )
                        .Select(x => x.Id)
                        .ToList();

                    foreach (var photoInfo in photoInfoDto)
                    {
                        if (photoInfo.CategoryIds.Any(x => !categoryIdsInDb.Contains(x)))
                        {
                            return false;
                        }
                    }

                    return true;
                }
            )
            .When(x => x.PhotoInfoDtos != null)
            .WithErrorCode(FinalizePostErrorCodes.PhotoInfoDtosHasInvalidCategoryIds)
            .WithMessage("PhotoInfoDtos cannot have invalid category ids");
    }
}
