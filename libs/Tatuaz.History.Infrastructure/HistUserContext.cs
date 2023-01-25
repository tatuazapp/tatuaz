using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;

namespace Tatuaz.History.DataAccess;

public class HistUserContext : IUserContext
{
    public string? CurrentUserEmail { get; set; } = "HistMicroservice@tatuaz.app";
}
