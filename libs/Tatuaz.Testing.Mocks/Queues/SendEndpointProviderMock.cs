using MassTransit;

using Moq;

namespace Tatuaz.Testing.Mocks.Queues;

public class SendEndpointProviderMock : Mock<ISendEndpointProvider>
{
    public SendEndpointMock SendEndpointMock { get; }
    public SendEndpointProviderMock()
    {
        SendEndpointMock = new SendEndpointMock();
        Setup(x => x.GetSendEndpoint(It.IsAny<Uri>()))
            .Returns(Task.FromResult(SendEndpointMock.Object));
    }
}
