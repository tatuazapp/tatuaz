using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;

namespace Tatuaz.History.DataAccess;

public class HistUserAccessor : IUserAccessor
{
    public string? CurrentUserId => "Hist-microservice";
}
