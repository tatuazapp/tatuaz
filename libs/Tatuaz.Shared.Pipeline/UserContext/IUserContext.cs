namespace Tatuaz.Shared.Pipeline.UserContext;

public interface IUserContext
{
    string? CurrentUserEmail { get; set; }
    string? CurrentUserAuth0Id { get; set; }
}
