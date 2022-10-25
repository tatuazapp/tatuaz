using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;

namespace Tatuaz.Shared.Infrastructure.DataAccess;

public class UserAccessor : IUserAccessor
{
    public Guid CurrentUserId => Guid.Empty;
}
