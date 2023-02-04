using System.Threading.Tasks;
using MassTransit;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;

namespace Tatuaz.Shared.Pipeline.Filters;

public class UserContextPublishFilter<T> : IFilter<PublishContext<T>> where T : class
{
    private readonly IUserContext _userContext;

    public UserContextPublishFilter(IUserContext userContext)
    {
        _userContext = userContext;
    }

    public Task Send(PublishContext<T> context, IPipe<PublishContext<T>> next)
    {
        if (_userContext.CurrentUserEmail != null)
            context.Headers.Set("CurrentUserEmail", _userContext.CurrentUserEmail);

        if (_userContext.CurrentUserAuth0Id != null)
            context.Headers.Set("CurrentUserAuth0Id", _userContext.CurrentUserAuth0Id);

        return next.Send(context);
    }

    public void Probe(ProbeContext context) { }
}
