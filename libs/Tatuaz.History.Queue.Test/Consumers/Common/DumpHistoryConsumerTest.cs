using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using NodaTime;
using Tatuaz.History.DataAccess.Services;
using Tatuaz.History.Queue.Consumers.Common;
using Tatuaz.History.Queue.Contracts;
using Tatuaz.History.Queue.Util;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;
using Xunit;

namespace Tatuaz.History.Queue.Test.Consumers.Common;

public class DumpHistoryConsumerTest
{
    private readonly Mock<IDumpHistoryService<TestHistEntity, Guid>> _dumpHistoryServiceMock;
    private readonly Mock<ILogger<DumpHistoryConsumer>> _loggerMock;
    private readonly ServiceProvider _serviceProvider;

    public DumpHistoryConsumerTest()
    {
        _loggerMock = new Mock<ILogger<DumpHistoryConsumer>>();
        _dumpHistoryServiceMock = new Mock<IDumpHistoryService<TestHistEntity, Guid>>();
        var services = new ServiceCollection();
        _serviceProvider = services
            .AddMassTransitTestHarness(cfg =>
            {
                cfg.AddConsumer<DumpHistoryConsumer>()
                    .Endpoint(e => e.Name = HistoryQueueConstants.DumpQueueName);
            })
            .AddScoped(_ => _loggerMock.Object)
            .AddScoped(_ => _dumpHistoryServiceMock.Object)
            .BuildServiceProvider(true);
    }

    public class TestHistEntity : HistEntity<Guid>
    {
        public string Name { get; set; } = default!;
    }

    public class TestHistEntity2 : HistEntity
    {
        public string Name { get; set; } = default!;
    }

    public class Consume : DumpHistoryConsumerTest
    {
        [Fact]
        public async Task Should_CorrectlyDeserializeDumpHistoryOrder_WhenCorrectHistEntitySent()
        {
            TestHistEntity actual = null!;
            _dumpHistoryServiceMock
                .Setup(x => x.DumpAsync(It.IsAny<TestHistEntity>(), It.IsAny<CancellationToken>()))
                .Callback<TestHistEntity, CancellationToken>(
                    (entity, ct) => { actual = entity; }
                );
            var harness = _serviceProvider.GetRequiredService<ITestHarness>();
            await harness.Start().ConfigureAwait(false);
            var testHistEntity = new TestHistEntity
            {
                Id = Guid.NewGuid(),
                Name = "Test",
                HistDumpedAt = Instant.FromUtc(2001, 1, 1, 0, 0),
                HistId = Guid.NewGuid(),
                HistState = HistState.Added
            };
            var sentEndpoint = await harness.Bus
                .GetSendEndpoint(HistoryQueueConstants.DumpQueueUri)
                .ConfigureAwait(false);

            await sentEndpoint
                .Send(HistorySerializer.SerializeDumpHistoryOrder(testHistEntity))
                .ConfigureAwait(false);

            Assert.True(await harness.Sent.Any<DumpHistoryOrder>().ConfigureAwait(false));
            Assert.True(await harness.Consumed.Any<DumpHistoryOrder>().ConfigureAwait(false));

            _dumpHistoryServiceMock.Verify(
                x => x.DumpAsync(It.IsAny<TestHistEntity>(), It.IsAny<CancellationToken>()),
                Times.Once
            );
            Assert.NotNull(actual);
            Assert.Equal(testHistEntity.Id, actual.Id);
            Assert.Equal(testHistEntity.Name, actual.Name);
            Assert.Equal(testHistEntity.HistDumpedAt, actual.HistDumpedAt);
            Assert.Equal(testHistEntity.HistId, actual.HistId);
            Assert.Equal(testHistEntity.HistState, actual.HistState);
        }

        [Fact]
        public async Task Should_Fail_WhenNoIdProvided()
        {
            var harness = _serviceProvider.GetRequiredService<ITestHarness>();
            await harness.Start().ConfigureAwait(false);
            var testHistEntity = new TestHistEntity2
            {
                Name = "Test",
                HistDumpedAt = Instant.FromUtc(2001, 1, 1, 0, 0),
                HistState = HistState.Added
            };
            var sentEndpoint = await harness.Bus
                .GetSendEndpoint(HistoryQueueConstants.DumpQueueUri)
                .ConfigureAwait(false);

            await sentEndpoint
                .Send(HistorySerializer.SerializeDumpHistoryOrder(testHistEntity))
                .ConfigureAwait(false);

            Assert.True(await harness.Sent.Any<DumpHistoryOrder>().ConfigureAwait(false));
            Assert.True(await harness.Consumed.Any<DumpHistoryOrder>().ConfigureAwait(false));

            _dumpHistoryServiceMock.Verify(
                x => x.DumpAsync(It.IsAny<TestHistEntity>(), It.IsAny<CancellationToken>()),
                Times.Never
            );
        }
    }
}