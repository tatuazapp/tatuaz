namespace Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;

public interface IUserContext
{
    string? CurrentUserEmail { get; set; }
    string? CurrentUserAuth0Id { get; set; }
}
