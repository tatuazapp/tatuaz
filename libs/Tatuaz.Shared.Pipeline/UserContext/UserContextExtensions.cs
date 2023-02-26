using Tatuaz.Shared.Pipeline.Exceptions;

namespace Tatuaz.Shared.Pipeline.UserContext;

public static class UserContextExtensions
{
    public static string RequiredCurrentUserEmail(this IUserContext userContext)
    {
        return userContext.CurrentUserEmail ?? throw new UserContextMissingException();
    }

    public static string RequiredCurrentUserAuth0Id(this IUserContext userContext)
    {
        return userContext.CurrentUserAuth0Id ?? throw new UserContextMissingException();
    }
}
