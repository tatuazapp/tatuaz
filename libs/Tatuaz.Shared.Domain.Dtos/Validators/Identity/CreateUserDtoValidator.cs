using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Tatuaz.Shared.Domain.Dtos.Dtos.Identity;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Identity;
using Tatuaz.Shared.Domain.Entities.Models.Identity;
using Tatuaz.Shared.Helpers;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;
using Tatuaz.Shared.Pipeline.Factories.ErrorCodes.Identity;

namespace Tatuaz.Shared.Domain.Dtos.Validators.Identity;

public class CreateUserDtoValidator<TDbContext> : AbstractValidator<CreateUserDto>
    where TDbContext : DbContext
{
    public CreateUserDtoValidator(IGenericRepository<TDbContext, TatuazUser, HistTatuazUser, string> userRepository)
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithErrorCode(CreateUserErrorCodes.EmailEmpty)
            .EmailAddress().WithErrorCode(CreateUserErrorCodes.EmailInvalid)
            .MustAsync(async (email, ct) =>
            {
                return !await userRepository.ExistsByPredicateAsync(x => x.Email == email, ct)
                    .ConfigureAwait(false);
            })
            .WithErrorCode(CreateUserErrorCodes.EmailAlreadyExists)
            .WithMessage("Email already exists");

        RuleFor(x => x.Username)
            .NotEmpty().WithErrorCode(CreateUserErrorCodes.UsernameEmpty)
            .MinimumLength(4).WithErrorCode(CreateUserErrorCodes.UsernameTooShort)
            .MaximumLength(32).WithErrorCode(CreateUserErrorCodes.UsernameTooLong)
            .MustAsync(async (username, ct) =>
            {
                return !await userRepository.ExistsByPredicateAsync(x => x.Username == username, ct)
                    .ConfigureAwait(false);
            })
            .WithErrorCode(CreateUserErrorCodes.UsernameAlreadyExists)
            .WithMessage("Username already exists");

        RuleFor(x => x.PhoneNumber)
            .Must(phoneNumber =>
                string.IsNullOrEmpty(phoneNumber) || RegexUtils.PhoneNumberRegex.IsMatch(phoneNumber))
            .WithErrorCode(CreateUserErrorCodes.PhoneNumberInvalid)
            .WithMessage("Phone number is invalid");
    }
}
