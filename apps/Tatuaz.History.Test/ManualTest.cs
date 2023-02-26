using System;
using System.Threading.Tasks;
using MassTransit;
using Tatuaz.Dashboard.Emails;
using Tatuaz.Dashboard.Queue;
using Tatuaz.Dashboard.Queue.Contracts.Emails;
using Xunit;

namespace Tatuaz.History.Test;

// this is trash left for testing purposes
public class ManualTest
{
    private readonly ISendEndpointProvider _sendEndpointProvider;

    public ManualTest(ISendEndpointProvider sendEndpointProvider)
    {
        _sendEndpointProvider = sendEndpointProvider;
    }

    [Fact]
    public async Task Test()
    {
        return; // comment this line to run the test
        var sendEndpoint = await _sendEndpointProvider
            .GetSendEndpoint(DashboardQueueConstants.SendEmailQueueUri)
            .ConfigureAwait(false);

        await sendEndpoint
            .Send(
                new SendEmail("lopogo69@gmail.com", Guid.NewGuid(), EmailType.Test, Guid.NewGuid())
            )
            .ConfigureAwait(false);
    }
}
