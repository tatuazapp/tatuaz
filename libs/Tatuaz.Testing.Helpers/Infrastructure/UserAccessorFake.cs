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

    public string? CurrentUserId => _primitiveTestFactory.Guids(_currentIndex).ToString();

    public void SetCurrentIndex(int index)
    {
        _currentIndex = index;
    }
}
