using System;
using System.Linq;
using FluentValidation;
using Tatuaz.Shared.Domain.Dtos.Dtos.Identity.User;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Identity;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Photo;
using Tatuaz.Shared.Domain.Entities.Models.Identity;
using Tatuaz.Shared.Domain.Entities.Models.Photo;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;
using Tatuaz.Shared.Pipeline.Factories.ErrorCodes.Identity;

namespace Tatuaz.Shared.Domain.Dtos.Validators.Identity.User;

public class SignUpDtoValidator : AbstractValidator<SignUpDto>
{
    public SignUpDtoValidator(
        IGenericRepository<TatuazUser, HistTatuazUser, string> userRepository,
        IGenericRepository<Category, HistCategory, int> categoryRepository
    )
    {
        RuleFor(x => x.Username)
            .NotNull()
            .WithErrorCode(SignUpErrorCodes.UsernameNull)
            .WithMessage("Username cannot be null");
        RuleFor(x => x.Username)
            .MinimumLength(4)
            .WithErrorCode(SignUpErrorCodes.UsernameTooShort)
            .WithMessage("Username must be at least 4 characters long");
        RuleFor(x => x.Username)
            .MaximumLength(32)
            .WithErrorCode(SignUpErrorCodes.UsernameTooLong)
            .WithMessage("Username must not be longer than 32 characters");
        RuleFor(x => x.Username)
            .MustAsync(
                async (username, ct) =>
                {
                    return !await userRepository
                        .ExistsByPredicateAsync(x => x.Username.ToLower() == username.ToLower(), ct)
                        .ConfigureAwait(false);
                }
            )
            .WithErrorCode(SignUpErrorCodes.UsernameAlreadyInUse)
            .WithMessage("Username already in use");
        RuleFor(x => x.Username)
            .Matches("^[a-zA-Z0-9_]*$")
            .WithErrorCode(SignUpErrorCodes.UsernameInvalidCharacters)
            .WithMessage("Username can only contain letters, numbers and underscores");

        RuleFor(x => x.CategoryIds)
            .NotNull()
            .WithErrorCode(SignUpErrorCodes.CategoryIdsNull)
            .WithMessage("Photo categories cannot be null");
        RuleFor(x => x.CategoryIds)
            .Must(x => x?.Length >= 3)
            .WithErrorCode(SignUpErrorCodes.CategoryIdsTooFew)
            .WithMessage("At least 3 categories must be selected")
            .When(x => x.CategoryIds != null);
        RuleFor(x => x.CategoryIds)
            .MustAsync(
                async (categoryIds, ct) =>
                {
                    var valid = true;
                    foreach (var categoryId in categoryIds!)
                    {
                        if (
                            await categoryRepository
                                .ExistsByIdAsync(categoryId, ct)
                                .ConfigureAwait(false)
                        )
                        {
                            continue;
                        }

                        valid = false;
                        break;
                    }

                    return valid;
                }
            )
            .WithErrorCode(SignUpErrorCodes.CategoryIdsInvalid)
            .WithMessage("One or more categories are invalid")
            .When(x => x.CategoryIds != null);
        RuleFor(x => x.CategoryIds)
            .Must(x => x?.Length == x?.Distinct().Count())
            .WithErrorCode(SignUpErrorCodes.CategoryIdsDuplicate)
            .WithMessage("Categories must not contain duplicates")
            .When(x => x.CategoryIds != null);
    }
}
