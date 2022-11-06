using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Shared.Pipeline.Queues;

public class TatuazProducerBase<TRequest, TData>
    where TRequest : TatuazMessage
{
    private readonly ILogger _logger;
    private readonly IRequestClient<TRequest> _requestClient;
    private readonly IUserAccessor _userAccessor;

    public TatuazProducerBase(IRequestClient<TRequest> requestClient, IUserAccessor userAccessor, ILogger logger)
    {
        _requestClient = requestClient;
        _userAccessor = userAccessor;
        _logger = logger;
    }

    public async Task<TatuazResult<TData>> Send(TRequest message, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Sending message {Message} from {ClassName}", message, GetType().Name);
        message = message with { UserId = _userAccessor.CurrentUserId };
        return (
                await
                    _requestClient
                        .GetResponse<TatuazResult<TData>>(message, cancellationToken)
                        .ConfigureAwait(false)
            )
            .Message;
    }
}
