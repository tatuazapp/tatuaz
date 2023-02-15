using System.Threading.Tasks;
using MassTransit;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;
using Tatuaz.Shared.Pipeline.UserContext;

namespace Tatuaz.Shared.Pipeline.Filters;

public class UserContextSendFilter<T> : IFilter<SendContext<T>>
    where T : class
{
    private readonly IUserContext _userContext;

    public UserContextSendFilter(IUserContext userContext)
    {
        _userContext = userContext;
    }

    public Task Send(SendContext<T> context, IPipe<SendContext<T>> next)
    {
        if (_userContext.CurrentUserEmail != null)
            context.Headers.Set("CurrentUserEmail", _userContext.CurrentUserEmail);

        if (_userContext.CurrentUserAuth0Id != null)
            context.Headers.Set("CurrentUserAuth0Id", _userContext.CurrentUserAuth0Id);

        return next.Send(context);
    }

    public void Probe(ProbeContext context) { }
}
