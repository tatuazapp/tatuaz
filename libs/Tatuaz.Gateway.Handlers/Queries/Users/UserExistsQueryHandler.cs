using MediatR;
using Tatuaz.Gateway.Infrastructure;
using Tatuaz.Gateway.Requests.Queries.Users;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Identity;
using Tatuaz.Shared.Domain.Entities.Models.Identity;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;

namespace Tatuaz.Gateway.Handlers.Queries.Users;

public class UserExistsQueryHandler : IRequestHandler<UserExistsQuery, bool>
{
    private readonly IGenericRepository<GatewayDbContext, TatuazUser, HistTatuazUser, string> _userRepository;

    public UserExistsQueryHandler(IGenericRepository<GatewayDbContext, TatuazUser, HistTatuazUser, string> userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> Handle(UserExistsQuery request, CancellationToken cancellationToken)
    {
        return await _userRepository.ExistsByIdAsync(request.UserId, cancellationToken).ConfigureAwait(false);
    }
}
