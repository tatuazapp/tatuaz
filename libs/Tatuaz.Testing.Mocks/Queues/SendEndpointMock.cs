using MassTransit;
using Moq;

namespace Tatuaz.Testing.Mocks.Queues;

public class SendEndpointMock : Mock<ISendEndpoint>
{
    public SendEndpointMock()
    {
        Setup(x => x.Send(It.IsAny<object>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
    }
}
