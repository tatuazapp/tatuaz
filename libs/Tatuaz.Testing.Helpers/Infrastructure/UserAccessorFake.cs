using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;
using Tatuaz.Testing.Fakes.Common;

namespace Tatuaz.Testing.Fakes.Infrastructure;

public class UserAccessorFake : IUserAccessor
{
    private readonly IPrimitiveValuesGenerator _primitiveTestFactory;
    private int _currentIndex;

    public UserAccessorFake(IPrimitiveValuesGenerator primitiveTestFactory)
    {
        _primitiveTestFactory = primitiveTestFactory;
        _currentIndex = 0;
    }

    public Guid CurrentUserId => _primitiveTestFactory.Guids(_currentIndex);

    public void SetCurrentIndex(int index)
    {
        _currentIndex = index;
    }
}