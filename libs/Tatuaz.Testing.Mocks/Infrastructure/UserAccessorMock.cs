using Moq;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;

namespace Tatuaz.Testing.Mocks.Infrastructure;

public class UserAccessorMock : Mock<IUserAccessor>
{
    public UserAccessorMock()
    {
        Setup(x => x.CurrentUserId).Returns("1");
    }

    public UserAccessorMock ReturnUserId(string? userId)
    {
        Setup(x => x.CurrentUserId).Returns(userId);
        return this;
    }
}