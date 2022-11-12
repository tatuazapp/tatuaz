using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;

namespace Tatuaz.Shared.Pipeline.Queues;

public class InternalUserContext : IUserContext
{
    public InternalUserContext() { }

    public InternalUserContext(string? currentUserId)
    {
        CurrentUserId = currentUserId;
    }

    public string? CurrentUserId { get; }
}
