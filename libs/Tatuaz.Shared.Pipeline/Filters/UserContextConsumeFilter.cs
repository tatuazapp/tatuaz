using System.Threading.Tasks;
using MassTransit;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;
using Tatuaz.Shared.Pipeline.UserContext;

namespace Tatuaz.Shared.Pipeline.Filters;

public class UserContextConsumeFilter<T> : IFilter<ConsumeContext<T>>
    where T : class
{
    private readonly IUserContext _userContext;

    public UserContextConsumeFilter(IUserContext userContext)
    {
        _userContext = userContext;
    }

    public Task Send(ConsumeContext<T> context, IPipe<ConsumeContext<T>> next)
    {
        _userContext.CurrentUserEmail = context.Headers.Get<string>("CurrentUserEmail");
        _userContext.CurrentUserAuth0Id = context.Headers.Get<string>("CurrentUserAuth0Id");

        return next.Send(context);
    }

    public void Probe(ProbeContext context) { }
}
