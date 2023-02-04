using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;
using Tatuaz.Shared.Pipeline.UserContext;

namespace Tatuaz.History.DataAccess;

public class HistUserContext : IUserContext
{
    public string? CurrentUserEmail { get; set; } = "HistMicroservice@tatuaz.app";
    public string? CurrentUserAuth0Id { get; set; } = "HistMicroservice";
}
