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
            .WithMessage("Username already in use");
    }
}
