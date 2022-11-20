using Moq;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;

namespace Tatuaz.Testing.Mocks.Infrastructure;

public class UserContextMock : Mock<IUserContext>
{
    public UserContextMock()
    {
        Setup(x => x.CurrentUserId).Returns("1");
    }

    public UserContextMock ReturnUserId(string? userId)
    {
        Setup(x => x.CurrentUserId).Returns(userId);
        return this;
    }
}
