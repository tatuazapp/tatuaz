using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;

namespace Tatuaz.Shared.Pipeline.Queues;

public class InternalUserAccessor : IUserAccessor
{
    public InternalUserAccessor() : this("SYSTEM")
    {
        
    }
    public InternalUserAccessor(string? currentUserId)
    {
        CurrentUserId = currentUserId;
    }
    public string? CurrentUserId { get; }
}