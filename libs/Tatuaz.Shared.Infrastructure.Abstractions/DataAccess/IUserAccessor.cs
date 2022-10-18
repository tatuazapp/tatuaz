namespace Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;

public interface IUserAccessor
{
    Guid CurrentUserId { get; }
}