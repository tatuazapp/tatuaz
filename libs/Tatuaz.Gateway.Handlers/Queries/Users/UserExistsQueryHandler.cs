using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Tatuaz.Gateway.Infrastructure;
using Tatuaz.Gateway.Requests.Queries.Users;

namespace Tatuaz.Gateway.Handlers.Queries.Users;

public class UserExistsQueryHandler : IRequestHandler<UserExistsQuery, bool>
{
    private readonly IServiceScopeFactory _scopeFactory;

    public UserExistsQueryHandler(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    public async Task<bool> Handle(UserExistsQuery request, CancellationToken cancellationToken)
    {
        using var scope = _scopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<GatewayDbContext>();
        return await dbContext.TatuazUsers
            .FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken)
            .ConfigureAwait(false) != null;
    }
}
