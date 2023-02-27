using System;
using FluentValidation;
using Tatuaz.Shared.Domain.Dtos.Dtos.Identity.User;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Identity;
using Tatuaz.Shared.Domain.Entities.Models.Identity;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;
using Tatuaz.Shared.Pipeline.Factories.ErrorCodes.Identity;

namespace Tatuaz.Shared.Domain.Dtos.Validators.Identity.User;

public class GetUserDtoValidator : AbstractValidator<GetUserDto>
{
    public GetUserDtoValidator(
        IGenericRepository<TatuazUser, HistTatuazUser, string> userRepository
    )
    {
        RuleFor(x => x.Username)
            .NotNull()
            .WithErrorCode(GetUserErrorCodes.UsernameNull)
            .WithMessage("Username cannot be null");

        RuleFor(x => x.Username)
            .MaximumLength(32)
            .WithErrorCode(GetUserErrorCodes.UsernameTooLong)
            .WithMessage("Username must not be longer than 32 characters");
    }
}
