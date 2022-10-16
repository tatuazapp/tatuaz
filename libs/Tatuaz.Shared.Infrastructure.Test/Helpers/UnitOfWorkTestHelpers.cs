using System.Reflection;

using MassTransit;

using Tatuaz.Shared.Infrastructure.Abstractions;

namespace Tatuaz.Shared.Infrastructure.Test.Helpers;

public static class UnitOfWorkTestHelpers
{
    public static void ReplaceSendEndpointProvider(this UnitOfWork unitOfWork, ISendEndpointProvider sendEndpointProvider)
    {
        var field = typeof(UnitOfWork).GetField("_sendEndpointProvider", BindingFlags.NonPublic | BindingFlags.Instance);
        field.SetValue(unitOfWork, sendEndpointProvider);
    }
}
