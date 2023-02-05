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
        IGenericRepository<PhotoCategory, HistPhotoCategory, int> photoCategoryRepository
    )
    {
        RuleFor(x => x.Username)
            .NotNull()
            .WithErrorCode(CreateUserErrorCodes.UsernameNull)
            .WithMessage("Username cannot be null")
            .MinimumLength(4)
            .WithErrorCode(CreateUserErrorCodes.UsernameTooShort)
            .WithMessage("Username must be at least 4 characters long")
            .MaximumLength(32)
            .WithErrorCode(CreateUserErrorCodes.UsernameTooLong)
            .WithMessage("Username must not be longer than 32 characters")
            .MustAsync(
                async (username, ct) =>
                {
                    return !await userRepository
                        .ExistsByPredicateAsync(x => x.Username == username, ct)
                        .ConfigureAwait(false);
                }
            )
            .WithErrorCode(CreateUserErrorCodes.UsernameAlreadyInUse)
            .WithMessage("Username already in use")
            .Matches("^[a-zA-Z0-9_]*$")
            .WithErrorCode(CreateUserErrorCodes.UsernameInvalidCharacters)
            .WithMessage("Username can only contain letters, numbers and underscores");

        RuleFor(x => x.PhotoCategoryIds)
            .Must(x => x.Length >= 3)
            .WithErrorCode(CreateUserErrorCodes.PhotoCategoryIdsTooFew)
            .WithMessage("At least 3 photo categories must be selected")
            .Must(x => x.Length <= 20)
            .WithErrorCode(CreateUserErrorCodes.PhotoCategoryIdsTooMany)
            .WithMessage("At most 20 photo categories can be selected")
            .MustAsync(
                async (photoCategoryIds, ct) =>
                {
                    var valid = true;
                    foreach (var photoCategoryId in photoCategoryIds)
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
            .WithErrorCode(CreateUserErrorCodes.PhotoCategoryIdsInvalid)
            .WithMessage("One or more photo categories are invalid")
            .Must(x => x.Length == x.Distinct().Count())
            .WithErrorCode(CreateUserErrorCodes.PhotoCategoryIdsDuplicate)
            .WithMessage("Photo categories must not contain duplicates");
    }
}
