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
        IGenericRepository<Category, HistCategory, int> photoCategoryRepository
    )
    {
        RuleFor(x => x.Username)
            .NotNull()
            .WithErrorCode(SignUpErrorCodes.UsernameNull)
            .WithMessage("Username cannot be null")
            .MinimumLength(4)
            .WithErrorCode(SignUpErrorCodes.UsernameTooShort)
            .WithMessage("Username must be at least 4 characters long")
            .MaximumLength(32)
            .WithErrorCode(SignUpErrorCodes.UsernameTooLong)
            .WithMessage("Username must not be longer than 32 characters")
            .MustAsync(
                async (username, ct) =>
                {
                    return !await userRepository
                        .ExistsByPredicateAsync(x => x.Username == username, ct)
                        .ConfigureAwait(false);
                }
            )
            .WithErrorCode(SignUpErrorCodes.UsernameAlreadyInUse)
            .WithMessage("Username already in use")
            .Matches("^[a-zA-Z0-9_]*$")
            .WithErrorCode(SignUpErrorCodes.UsernameInvalidCharacters)
            .WithMessage("Username can only contain letters, numbers and underscores");

        RuleFor(x => x.PhotoCategoryIds)
            .NotNull()
            .WithErrorCode(SignUpErrorCodes.PhotoCategoryIdsNull)
            .WithMessage("Photo categories cannot be null")
            .Must(x => x?.Length >= 3)
            .When(x => x.PhotoCategoryIds != null)
            .WithErrorCode(SignUpErrorCodes.PhotoCategoryIdsTooFew)
            .WithMessage("At least 3 photo categories must be selected")
            .Must(x => x?.Length <= 20)
            .When(x => x.PhotoCategoryIds != null)
            .WithErrorCode(SignUpErrorCodes.PhotoCategoryIdsTooMany)
            .WithMessage("At most 20 photo categories can be selected")
            .MustAsync(
                async (photoCategoryIds, ct) =>
                {
                    var valid = true;
                    foreach (var photoCategoryId in photoCategoryIds!)
                    {
                        if (
                            await photoCategoryRepository
                                .ExistsByIdAsync(photoCategoryId, ct)
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
            .When(x => x.PhotoCategoryIds != null)
            .WithErrorCode(SignUpErrorCodes.PhotoCategoryIdsInvalid)
            .WithMessage("One or more photo categories are invalid")
            .Must(x => x?.Length == x?.Distinct().Count())
            .When(x => x.PhotoCategoryIds != null)
            .WithErrorCode(SignUpErrorCodes.PhotoCategoryIdsDuplicate)
            .WithMessage("Photo categories must not contain duplicates");
    }
}
