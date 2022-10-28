using MassTransit;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Shared.Pipeline.Queues;

public class TatuazProducerBase<TRequest, TData>
    where TRequest : class
{
    private readonly IRequestClient<TRequest> _requestClient;

    public TatuazProducerBase(IRequestClient<TRequest> requestClient)
    {
        _requestClient = requestClient;
    }

    public async Task<TatuazResult<TData>> Send(TRequest message)
    {
        return (
                await
                    _requestClient
                        .GetResponse<TatuazResult<TData>>(message)
                        .ConfigureAwait(false)
            )
            .Message;
    }
}
