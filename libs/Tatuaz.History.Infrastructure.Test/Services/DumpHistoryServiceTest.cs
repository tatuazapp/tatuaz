using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using NodaTime;
using Tatuaz.History.DataAccess.Exceptions;
using Tatuaz.History.DataAccess.Services;
using Tatuaz.History.DataAccess.Test.Utils;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;
using Xunit;

namespace Tatuaz.History.DataAccess.Test.Services;

public class DumpHistoryServiceTest
{
    public class DumpAsync
    {
        [Fact]
        public async Task Should_CorrectlyDumpHistory_WhenStateAddedAndNothingInDb()
        {
            var histDbContextMock = new HistDbContextMock();
            var dumpHistoryService = new DumpHistoryService<TestHistEntity, Guid>(
                histDbContextMock.Object,
                NullLogger<DumpHistoryService<TestHistEntity, Guid>>.Instance
            );

            var toDump = new TestHistEntity
            {
                Id = Guid.Parse("3AAAFB34-1C52-4B89-A863-F8840002DCAE"),
                HistId = Guid.Parse("C43F5099-CAB5-4469-9914-BE686FD45E40"),
                HistDumpedAt = Instant.FromUtc(2001, 1, 1, 0, 0),
                HistState = HistState.Added,
                Name = "Test"
            };

            await dumpHistoryService.DumpAsync(toDump).ConfigureAwait(false);

            var dumped = histDbContextMock.TestHistEntities.First();
            Assert.Equal(toDump.Id, dumped.Id);
            Assert.Equal(toDump.HistId, dumped.HistId);
            Assert.Equal(toDump.HistDumpedAt, dumped.HistDumpedAt);
            Assert.Equal(toDump.HistState, dumped.HistState);
            Assert.Equal(toDump.Name, dumped.Name);

            histDbContextMock.Verify(x => x.Add(toDump), Times.Once);
            histDbContextMock.Verify(
                x => x.SaveChangesAsync(It.IsAny<CancellationToken>()),
                Times.Once
            );
            histDbContextMock.Verify(x => x.Set<TestHistEntity>(), Times.Once);
        }

        [Fact]
        public async Task Should_CorrectlyDumpHistory_WhenStateModifiedAndOneEntityInDb()
        {
            var histDbContextMock = new HistDbContextMock();
            var dumpHistoryService = new DumpHistoryService<TestHistEntity, Guid>(
                histDbContextMock.Object,
                NullLogger<DumpHistoryService<TestHistEntity, Guid>>.Instance
            );

            var toDump = new TestHistEntity
            {
                Id = Guid.Parse("3AAAFB34-1C52-4B89-A863-F8840002DCAE"),
                HistId = Guid.Parse("C43F5099-CAB5-4469-9914-BE686FD45E40"),
                HistDumpedAt = Instant.FromUtc(2001, 1, 1, 0, 0),
                HistState = HistState.Modified,
                Name = "Test"
            };

            var existing = new TestHistEntity
            {
                Id = Guid.Parse("3AAAFB34-1C52-4B89-A863-F8840002DCAE"),
                HistId = Guid.Parse("C43F5099-CAB5-4469-9914-BE686AD45E40"),
                HistDumpedAt = Instant.FromUtc(2000, 1, 1, 0, 0),
                HistState = HistState.Added,
                Name = "Test"
            };

            histDbContextMock.TestHistEntities.Add(existing);

            await dumpHistoryService.DumpAsync(toDump).ConfigureAwait(false);

            var dumped = histDbContextMock.TestHistEntities.First(
                x => x.HistId == Guid.Parse("C43F5099-CAB5-4469-9914-BE686FD45E40")
            );
            Assert.Equal(toDump.Id, dumped.Id);
            Assert.Equal(toDump.HistId, dumped.HistId);
            Assert.Equal(toDump.HistDumpedAt, dumped.HistDumpedAt);
            Assert.Equal(toDump.HistState, dumped.HistState);
            Assert.Equal(toDump.Name, dumped.Name);

            histDbContextMock.Verify(x => x.Add(toDump), Times.Once);
            histDbContextMock.Verify(
                x => x.SaveChangesAsync(It.IsAny<CancellationToken>()),
                Times.Once
            );
            histDbContextMock.Verify(x => x.Set<TestHistEntity>(), Times.Once);
        }

        [Fact]
        public async Task Should_CorrectlyDumpHistory_WhenStateDeletedAndOneEntityInDb()
        {
            var histDbContextMock = new HistDbContextMock();
            var dumpHistoryService = new DumpHistoryService<TestHistEntity, Guid>(
                histDbContextMock.Object,
                NullLogger<DumpHistoryService<TestHistEntity, Guid>>.Instance
            );

            var toDump = new TestHistEntity
            {
                Id = Guid.Parse("3AAAFB34-1C52-4B89-A863-F8840002DCAE"),
                HistId = Guid.Parse("C43F5099-CAB5-4469-9914-BE686FD45E40"),
                HistDumpedAt = Instant.FromUtc(2001, 1, 1, 0, 0),
                HistState = HistState.Deleted,
                Name = "Test"
            };

            var existing = new TestHistEntity
            {
                Id = Guid.Parse("3AAAFB34-1C52-4B89-A863-F8840002DCAE"),
                HistId = Guid.Parse("C43F5099-CAB5-4469-9914-BE686AD45E40"),
                HistDumpedAt = Instant.FromUtc(2000, 1, 1, 0, 0),
                HistState = HistState.Added,
                Name = "Test"
            };

            histDbContextMock.TestHistEntities.Add(existing);

            await dumpHistoryService.DumpAsync(toDump).ConfigureAwait(false);

            var dumped = histDbContextMock.TestHistEntities.First(
                x => x.HistId == Guid.Parse("C43F5099-CAB5-4469-9914-BE686FD45E40")
            );
            Assert.Equal(toDump.Id, dumped.Id);
            Assert.Equal(toDump.HistId, dumped.HistId);
            Assert.Equal(toDump.HistDumpedAt, dumped.HistDumpedAt);
            Assert.Equal(toDump.HistState, dumped.HistState);
            Assert.Equal(toDump.Name, dumped.Name);

            histDbContextMock.Verify(x => x.Add(toDump), Times.Once);
            histDbContextMock.Verify(
                x => x.SaveChangesAsync(It.IsAny<CancellationToken>()),
                Times.Once
            );
            histDbContextMock.Verify(x => x.Set<TestHistEntity>(), Times.Once);
        }

        [Fact]
        public async Task Should_Throw_WhenStateAddedAndOneEntityInDb()
        {
            var histDbContextMock = new HistDbContextMock();
            var dumpHistoryService = new DumpHistoryService<TestHistEntity, Guid>(
                histDbContextMock.Object,
                NullLogger<DumpHistoryService<TestHistEntity, Guid>>.Instance
            );

            var toDump = new TestHistEntity
            {
                Id = Guid.Parse("3AAAFB34-1C52-4B89-A863-F8840002DCAE"),
                HistId = Guid.Parse("C43F5099-CAB5-4469-9914-BE686FD45E40"),
                HistDumpedAt = Instant.FromUtc(2001, 1, 1, 0, 0),
                HistState = HistState.Added,
                Name = "Test"
            };

            var existing = new TestHistEntity
            {
                Id = Guid.Parse("3AAAFB34-1C52-4B89-A863-F8840002DCAE"),
                HistId = Guid.Parse("C43F5099-CAB5-4469-9914-BE686AD45E40"),
                HistDumpedAt = Instant.FromUtc(2000, 1, 1, 0, 0),
                HistState = HistState.Added,
                Name = "Test"
            };

            histDbContextMock.TestHistEntities.Add(existing);

            await Assert
                .ThrowsAsync<HistException>(
                    async () => await dumpHistoryService.DumpAsync(toDump).ConfigureAwait(false)
                )
                .ConfigureAwait(false);
        }

        [Fact]
        public async Task Should_Throw_WhenStateModifiedAndNothingInDb()
        {
            var histDbContextMock = new HistDbContextMock();
            var dumpHistoryService = new DumpHistoryService<TestHistEntity, Guid>(
                histDbContextMock.Object,
                NullLogger<DumpHistoryService<TestHistEntity, Guid>>.Instance
            );

            var toDump = new TestHistEntity
            {
                Id = Guid.Parse("3AAAFB34-1C52-4B89-A863-F8840002DCAE"),
                HistId = Guid.Parse("C43F5099-CAB5-4469-9914-BE686FD45E40"),
                HistDumpedAt = Instant.FromUtc(2001, 1, 1, 0, 0),
                HistState = HistState.Modified,
                Name = "Test"
            };

            await Assert
                .ThrowsAsync<HistException>(
                    async () => await dumpHistoryService.DumpAsync(toDump).ConfigureAwait(false)
                )
                .ConfigureAwait(false);
        }

        [Fact]
        public async Task Should_Throw_WhenStateDeletedAndNothingInDb()
        {
            var histDbContextMock = new HistDbContextMock();
            var dumpHistoryService = new DumpHistoryService<TestHistEntity, Guid>(
                histDbContextMock.Object,
                NullLogger<DumpHistoryService<TestHistEntity, Guid>>.Instance
            );

            var toDump = new TestHistEntity
            {
                Id = Guid.Parse("3AAAFB34-1C52-4B89-A863-F8840002DCAE"),
                HistId = Guid.Parse("C43F5099-CAB5-4469-9914-BE686FD45E40"),
                HistDumpedAt = Instant.FromUtc(2001, 1, 1, 0, 0),
                HistState = HistState.Deleted,
                Name = "Test"
            };

            await Assert
                .ThrowsAsync<HistException>(
                    async () => await dumpHistoryService.DumpAsync(toDump).ConfigureAwait(false)
                )
                .ConfigureAwait(false);
        }
    }
}