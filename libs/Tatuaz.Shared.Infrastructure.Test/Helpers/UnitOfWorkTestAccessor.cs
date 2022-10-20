using System.Reflection;
using MassTransit;
using Tatuaz.Shared.Infrastructure.DataAccess;

namespace Tatuaz.Shared.Infrastructure.Test.Helpers;

public class UnitOfWorkTestAccessor
{
    public UnitOfWorkTestAccessor(UnitOfWork unitOfWork)
    {
        UnitOfWork = unitOfWork;
    }

    public UnitOfWork UnitOfWork { get; set; }

    public ISendEndpointProvider SendEndpointProvider
    {
        get
        {
            var field = typeof(UnitOfWork).GetField(
                "_sendEndpointProvider",
                BindingFlags.NonPublic | BindingFlags.Instance
            );
            return field?.GetValue(UnitOfWork) as ISendEndpointProvider
                ?? throw new InvalidOperationException();
        }
        set
        {
            var field = typeof(UnitOfWork).GetField(
                "_sendEndpointProvider",
                BindingFlags.NonPublic | BindingFlags.Instance
            );
            field?.SetValue(UnitOfWork, value);
        }
    }
}
