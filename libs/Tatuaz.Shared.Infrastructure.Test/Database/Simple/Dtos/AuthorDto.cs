using System;

namespace Tatuaz.Shared.Infrastructure.Test.Database.Simple.Dtos;

public class AuthorDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
}
