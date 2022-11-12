namespace Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;

/// <summary>
/// Every class that uses IUserContext from DI resolution should implement this interface.
/// </summary>
public interface IUserContextEnjoyer
{
    /// <summary>
    /// Internal tatuaz pipeline method, should not be called explicitly.
    /// </summary>
    /// <param name="userContext"></param>
    void SetUserContext(IUserContext userContext);
}
