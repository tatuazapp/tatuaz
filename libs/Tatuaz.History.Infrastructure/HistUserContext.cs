using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;

namespace Tatuaz.History.DataAccess;

public class HistUserContext : IUserContext
{
    public string? CurrentUserId => "Hist-microservice";
}
