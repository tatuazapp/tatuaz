using Moq;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;
using Tatuaz.Shared.Pipeline.UserContext;

namespace Tatuaz.Testing.Mocks.Infrastructure;

public class UserContextMock : Mock<IUserContext>
{
    public UserContextMock()
    {
        Setup(x => x.CurrentUserEmail).Returns("1");
        Setup(x => x.CurrentUserAuth0Id).Returns("1");
    }

    public UserContextMock ReturnUserId(string? userId)
    {
        Setup(x => x.CurrentUserEmail).Returns(userId);
        return this;
    }

    public UserContextMock ReturnUserAuth0Id(string? userAuth0Id)
    {
        Setup(x => x.CurrentUserAuth0Id).Returns(userAuth0Id);
        return this;
    }
}
