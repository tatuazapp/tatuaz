using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Tatuaz.Gateway.Requests.Queries.Identity;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Identity;
using Tatuaz.Shared.Domain.Entities.Models.Identity;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;

namespace Tatuaz.Gateway.Handlers.Queries.Identity;

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
        var userRepository = scope.ServiceProvider.GetRequiredService<
            IGenericRepository<TatuazUser, HistTatuazUser, string>
        >();
        var user = await userRepository
            .GetByIdAsync(request.Email, cancellationToken: cancellationToken)
            .ConfigureAwait(false);
        return user != null && user.Auth0Id == request.Auth0Id;
    }
}
