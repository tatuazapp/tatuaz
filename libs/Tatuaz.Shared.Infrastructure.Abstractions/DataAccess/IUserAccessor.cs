namespace Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;

public interface IUserAccessor
{
    string? CurrentUserId { get; }
}
