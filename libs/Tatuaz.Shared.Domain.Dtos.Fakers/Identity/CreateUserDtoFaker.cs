﻿using Bogus;
using Tatuaz.Shared.Domain.Dtos.Dtos.Identity;

namespace Tatuaz.Shared.Domain.Dtos.Fakers.Identity;

public sealed class CreateUserDtoFaker : Faker<CreateUserDto>
{
    public CreateUserDtoFaker()
    {
        StrictMode(true);
        RuleFor(x => x.Email, f => f.Internet.Email());
        RuleFor(x => x.Username, f => f.Internet.UserName());
        RuleFor(x => x.PhoneNumber, f => f.Phone.PhoneNumber());
    }
}
