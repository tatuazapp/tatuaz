namespace Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;

/// <summary>
/// When using this via DI, class should derive from <see cref="IUserContextEnjoyer"/>.
/// </summary>
public interface IUserContext
{
    string? CurrentUserId { get; }
}
