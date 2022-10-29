namespace Tatuaz.Shared.Domain.Dtos.Dtos.Identity;

public record CreateUserDto
(
    string Username,
    string Email,
    string PhoneNumber
);
