namespace Tatuaz.Shared.Pipeline.UserContext;

public class UserContext : IUserContext
{
    public string? CurrentUserEmail { get; set; }
    public string? CurrentUserAuth0Id { get; set; }
}
