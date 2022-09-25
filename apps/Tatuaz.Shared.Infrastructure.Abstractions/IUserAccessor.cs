namespace Tatuaz.Shared.Infrastructure.Abstractions;

public interface IUserAccessor
{
    Guid CurrentUserId { get; }
}
