using FluentValidation;
using Tatuaz.Shared.Domain.Dtos.Dtos.Identity;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Identity;
using Tatuaz.Shared.Domain.Entities.Models.Identity;
using Tatuaz.Shared.Helpers;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;
using Tatuaz.Shared.Pipeline.Factories.ErrorCodes.Identity;

namespace Tatuaz.Shared.Domain.Dtos.Validators.Identity;

public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
{
    public CreateUserDtoValidator(
        IGenericRepository<TatuazUser, HistTatuazUser, string> userRepository
    )
    {
        RuleFor(x => x.Email)
            .NotNull()
            .WithErrorCode(CreateUserErrorCodes.EmailNull)
            .WithMessage("Email cannot be null")
            .MinimumLength(1)
            .WithErrorCode(CreateUserErrorCodes.EmailEmpty)
            .WithMessage("Email must not be empty")
            .EmailAddress()
            .WithErrorCode(CreateUserErrorCodes.EmailInvalid)
            .WithMessage("Email is invalid")
            .MustAsync(
                async (email, ct) =>
                {
                    return !await userRepository
                        .ExistsByPredicateAsync(x => x.Email == email, ct)
                        .ConfigureAwait(false);
                }
            )
            .WithErrorCode(CreateUserErrorCodes.EmailAlreadyExists)
            .WithMessage("Email already exists")
            .MaximumLength(256)
            .WithErrorCode(CreateUserErrorCodes.EmailTooLong)
            .WithMessage("Email must not be longer than 256 characters");

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
            .WithErrorCode(CreateUserErrorCodes.UsernameAlreadyExists)
            .WithMessage("Username already exists");

        RuleFor(x => x.PhoneNumber)
            .Matches(RegexUtils.PhoneNumberRegex)
            .WithErrorCode(CreateUserErrorCodes.PhoneNumberInvalid)
            .WithMessage("Phone number is invalid");
    }
}
