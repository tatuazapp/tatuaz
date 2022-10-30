using System;
using System.Reflection;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Tatuaz.Shared.Infrastructure.DataAccess;

namespace Tatuaz.Shared.Infrastructure.Test.Helpers;

public class UnitOfWorkTestAccessor<TDbContext>
    where TDbContext : DbContext
{
    public UnitOfWorkTestAccessor(UnitOfWork<TDbContext> unitOfWork)
    {
        UnitOfWork = unitOfWork;
    }

    public UnitOfWork<TDbContext> UnitOfWork { get; set; }

    public ISendEndpointProvider SendEndpointProvider
    {
        get
        {
            var field = typeof(UnitOfWork<TDbContext>).GetField(
                "_sendEndpointProvider",
                BindingFlags.NonPublic | BindingFlags.Instance
            );
            return field?.GetValue(UnitOfWork) as ISendEndpointProvider
                   ?? throw new InvalidOperationException();
        }
        set
        {
            var field = typeof(UnitOfWork<TDbContext>).GetField(
                "_sendEndpointProvider",
                BindingFlags.NonPublic | BindingFlags.Instance
            );
            field?.SetValue(UnitOfWork, value);
        }
    }
}
