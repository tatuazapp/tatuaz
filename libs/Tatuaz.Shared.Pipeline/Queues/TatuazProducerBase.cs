using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Shared.Pipeline.Queues;

public class TatuazProducerBase<TRequest, TData> : IUserContextEnjoyer
    where TRequest : TatuazMessage
{
    private readonly ILogger _logger;
    private readonly IRequestClient<TRequest> _requestClient;
    private IUserContext _userContext;

    public TatuazProducerBase(
        IRequestClient<TRequest> requestClient,
        IUserContext userContext,
        ILogger logger
    )
    {
        _requestClient = requestClient;
        _userContext = userContext;
        _logger = logger;
    }

    public async Task<TatuazResult<TData>> Send(
        TRequest message,
        CancellationToken cancellationToken = default
    )
    {
        _logger.LogInformation(
            "Sending message {Message} from {ClassName}",
            message,
            GetType().Name
        );
        message = message with { UserId = _userContext.CurrentUserId };
        return (
            await _requestClient
                .GetResponse<TatuazResult<TData>>(message, cancellationToken)
                .ConfigureAwait(false)
        ).Message;
    }

    public void SetUserContext(IUserContext userContext)
    {
        _userContext = userContext;
    }
}
