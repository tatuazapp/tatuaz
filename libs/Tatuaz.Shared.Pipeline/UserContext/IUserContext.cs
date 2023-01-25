namespace Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;

public interface IUserContext
{
    string? CurrentUserEmail { get; set; }
}
