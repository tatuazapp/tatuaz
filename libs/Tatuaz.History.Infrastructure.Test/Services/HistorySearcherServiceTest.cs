using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NodaTime;
using Tatuaz.History.DataAccess.Services;
using Tatuaz.History.DataAccess.Test.Utils;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;
using Xunit;

namespace Tatuaz.History.DataAccess.Test.Services;

public class HistorySearcherServiceTest
{
    private static readonly Guid SampleGuid1 = Guid.Parse("AE070011-E390-4D68-8BF5-CA34C7DE02A6");
    private static readonly Guid SampleGuid2 = Guid.Parse("AE070011-E390-4D68-8BF5-CA34C7DE02A5");
    private readonly HistDbContextMock _dbContextMock;
    private readonly HistorySearcherService<TestHistEntity, Guid> _historySearcherService;

    protected HistorySearcherServiceTest()
    {
        _dbContextMock = new HistDbContextMock();
        _historySearcherService = new HistorySearcherService<TestHistEntity, Guid>(
            _dbContextMock.Object
        );
    }

    private static Instant DateAdded => Instant.FromUtc(2020, 1, 1, 0, 0, 0);

    private static Instant DateModified => Instant.FromUtc(2020, 3, 1, 0, 0, 0);

    private static Instant DateDeleted => Instant.FromUtc(2020, 5, 1, 0, 0, 0);

    private static Instant DateAdded2 => Instant.FromUtc(2020, 2, 1, 0, 0, 0);

    private static Instant DateModified2 => Instant.FromUtc(2020, 4, 1, 0, 0, 0);

    private static Instant DateDeleted2 => Instant.FromUtc(2020, 6, 1, 0, 0, 0);

    private static IEnumerable<TestHistEntity> SampleDataWithAddedModifiedDeleted()
    {
        return new List<TestHistEntity>
        {
            new()
            {
                Id = SampleGuid1,
                Name = "Test1",
                HistState = HistState.Added,
                HistDumpedAt = DateAdded
            },
            new()
            {
                Id = SampleGuid1,
                Name = "Test2",
                HistState = HistState.Modified,
                HistDumpedAt = DateModified
            },
            new()
            {
                Id = SampleGuid1,
                Name = "Test3",
                HistState = HistState.Deleted,
                HistDumpedAt = DateDeleted
            }
        };
    }

    private static IEnumerable<TestHistEntity> SampleDataWithAddedModifiedDeleted2()
    {
        return new List<TestHistEntity>
        {
            new()
            {
                Id = SampleGuid2,
                Name = "Test4",
                HistState = HistState.Added,
                HistDumpedAt = DateAdded2
            },
            new()
            {
                Id = SampleGuid2,
                Name = "Test5",
                HistState = HistState.Modified,
                HistDumpedAt = DateModified2
            },
            new()
            {
                Id = SampleGuid2,
                Name = "Test6",
                HistState = HistState.Deleted,
                HistDumpedAt = DateDeleted2
            }
        };
    }

    public class GetByIdAsync : HistorySearcherServiceTest
    {
        [Fact]
        public async Task Should_ReturnNull_WhenNoHistory()
        {
            // Act
            var result = await _historySearcherService
                .GetByIdAsync(Guid.NewGuid(), Instant.MaxValue)
                .ConfigureAwait(false);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Should_ReturnNull_WhenDateBeforeAddedProvided()
        {
            // Arrange
            var sampleData = SampleDataWithAddedModifiedDeleted().ToList();
            _dbContextMock.TestHistEntities.AddRange(sampleData);

            // Act
            var result = await _historySearcherService
                .GetByIdAsync(SampleGuid1, DateAdded.Minus(Duration.FromMilliseconds(1)))
                .ConfigureAwait(false);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Should_ReturnAdded_WhenDateAfterAddedAndBeforeModifiedProvided()
        {
            // Arrange
            var sampleData = SampleDataWithAddedModifiedDeleted().ToList();
            _dbContextMock.TestHistEntities.AddRange(sampleData);

            // Act
            var result = await _historySearcherService
                .GetByIdAsync(SampleGuid1, DateAdded.Plus(Duration.FromMilliseconds(1)))
                .ConfigureAwait(false);

            // Assert
#pragma warning disable CS8602
            Assert.NotNull(result);
            Assert.Equal(SampleGuid1, result.Id);
            Assert.Equal(sampleData.First().Name, result.Name);
            Assert.Equal(sampleData.First().HistState, result.HistState);
            Assert.Equal(sampleData.First().HistDumpedAt, result.HistDumpedAt);
#pragma warning restore CS8602
        }

        [Fact]
        public async Task Should_ReturnModified_WhenDateAfterModifiedAndBeforeDeletedProvided()
        {
            // Arrange
            var sampleData = SampleDataWithAddedModifiedDeleted().ToList();
            _dbContextMock.TestHistEntities.AddRange(sampleData);

            // Act
            var result = await _historySearcherService
                .GetByIdAsync(SampleGuid1, DateDeleted.Minus(Duration.FromMilliseconds(1)))
                .ConfigureAwait(false);

            // Assert
#pragma warning disable CS8602
            Assert.NotNull(result);
            Assert.Equal(SampleGuid1, result.Id);
            Assert.Equal(sampleData[1].Name, result.Name);
            Assert.Equal(sampleData[1].HistState, result.HistState);
            Assert.Equal(sampleData[1].HistDumpedAt, result.HistDumpedAt);
#pragma warning restore CS8602
        }

        [Fact]
        public async Task Should_ReturnNull_WhenDateAfterDeletedProvided()
        {
            // Arrange
            var sampleData = SampleDataWithAddedModifiedDeleted().ToList();
            _dbContextMock.TestHistEntities.AddRange(sampleData);

            // Act
            var result = await _historySearcherService
                .GetByIdAsync(SampleGuid1, DateDeleted.Plus(Duration.FromMilliseconds(1)))
                .ConfigureAwait(false);

            // Assert
            Assert.Null(result);
        }
    }

    public class ExistsByIdAsync : HistorySearcherServiceTest
    {
        [Fact]
        public async Task Should_ReturnFalse_WhenNoHistory()
        {
            // Act
            var result = await _historySearcherService
                .ExistsByIdAsync(Guid.NewGuid(), Instant.MaxValue)
                .ConfigureAwait(false);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task Should_ReturnFalse_WhenDateBeforeAddedProvided()
        {
            // Arrange
            var sampleData = SampleDataWithAddedModifiedDeleted().ToList();
            _dbContextMock.TestHistEntities.AddRange(sampleData);

            // Act
            var result = await _historySearcherService
                .ExistsByIdAsync(SampleGuid1, DateAdded.Minus(Duration.FromMilliseconds(1)))
                .ConfigureAwait(false);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task Should_ReturnTrue_WhenDateAfterAddedAndBeforeModifiedProvided()
        {
            // Arrange
            var sampleData = SampleDataWithAddedModifiedDeleted().ToList();
            _dbContextMock.TestHistEntities.AddRange(sampleData);

            // Act
            var result = await _historySearcherService
                .ExistsByIdAsync(SampleGuid1, DateAdded.Plus(Duration.FromMilliseconds(1)))
                .ConfigureAwait(false);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task Should_ReturnTrue_WhenDateAfterModifiedAndBeforeDeletedProvided()
        {
            // Arrange
            var sampleData = SampleDataWithAddedModifiedDeleted().ToList();
            _dbContextMock.TestHistEntities.AddRange(sampleData);

            // Act
            var result = await _historySearcherService
                .ExistsByIdAsync(SampleGuid1, DateDeleted.Minus(Duration.FromMilliseconds(1)))
                .ConfigureAwait(false);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task Should_ReturnFalse_WhenDateAfterDeletedProvided()
        {
            // Arrange
            var sampleData = SampleDataWithAddedModifiedDeleted().ToList();
            _dbContextMock.TestHistEntities.AddRange(sampleData);

            // Act
            var result = await _historySearcherService
                .ExistsByIdAsync(sampleData[2].Id, DateDeleted.Plus(Duration.FromMilliseconds(1)))
                .ConfigureAwait(false);

            // Assert
            Assert.False(result);
        }
    }

    public class GetBySpecificationAsync { }

    public class GetBySpecificationWithPagingAsync { }

    public class ExistsByPredicateAsync : HistorySearcherServiceTest
    {
        [Fact]
        public async Task Should_ReturnFalse_WhenNoHistory()
        {
            // Act
            var result = await _historySearcherService
                .ExistsByPredicateAsync(x => x.Id == Guid.NewGuid(), Instant.MaxValue)
                .ConfigureAwait(false);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task Should_ReturnFalse_WhenDateBeforeAddedProvided()
        {
            // Arrange
            var sampleData = SampleDataWithAddedModifiedDeleted().ToList();
            _dbContextMock.TestHistEntities.AddRange(sampleData);

            // Act
            var result = await _historySearcherService
                .ExistsByPredicateAsync(
                    x => x.Id == SampleGuid1,
                    DateAdded.Minus(Duration.FromMilliseconds(1))
                )
                .ConfigureAwait(false);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task Should_ReturnFalse_WhenDateBeforeAddedProvided2()
        {
            // Arrange
            var sampleData = SampleDataWithAddedModifiedDeleted().ToList();
            _dbContextMock.TestHistEntities.AddRange(sampleData);

            var sampleData2 = SampleDataWithAddedModifiedDeleted2().ToList();
            _dbContextMock.TestHistEntities.AddRange(sampleData2);

            // Act
            var result = await _historySearcherService
                .ExistsByPredicateAsync(
                    x => x.Id == SampleGuid1 || x.Id == SampleGuid2,
                    DateAdded.Minus(Duration.FromMilliseconds(1))
                )
                .ConfigureAwait(false);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task Should_ReturnTrue_WhenDateAfterAddedAndBeforeModifiedProvided()
        {
            // Arrange
            var sampleData = SampleDataWithAddedModifiedDeleted().ToList();
            _dbContextMock.TestHistEntities.AddRange(sampleData);

            // Act
            var result = await _historySearcherService
                .ExistsByPredicateAsync(
                    x => x.Id == SampleGuid1,
                    DateAdded.Plus(Duration.FromMilliseconds(1))
                )
                .ConfigureAwait(false);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task Should_ReturnTrue_WhenDateAfterAddedAndBeforeModifiedProvided2()
        {
            // Arrange
            var sampleData = SampleDataWithAddedModifiedDeleted().ToList();
            _dbContextMock.TestHistEntities.AddRange(sampleData);

            var sampleData2 = SampleDataWithAddedModifiedDeleted2().ToList();
            _dbContextMock.TestHistEntities.AddRange(sampleData2);

            // Act
            var result = await _historySearcherService
                .ExistsByPredicateAsync(
                    x => x.Id == SampleGuid1 || x.Id == SampleGuid2,
                    DateAdded.Plus(Duration.FromMilliseconds(1))
                )
                .ConfigureAwait(false);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task Should_ReturnTrue_WhenDateAfterModifiedAndBeforeDeletedProvided()
        {
            // Arrange
            var sampleData = SampleDataWithAddedModifiedDeleted().ToList();
            _dbContextMock.TestHistEntities.AddRange(sampleData);

            // Act
            var result = await _historySearcherService
                .ExistsByPredicateAsync(
                    x => x.Id == SampleGuid1,
                    DateDeleted.Minus(Duration.FromMilliseconds(1))
                )
                .ConfigureAwait(false);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task Should_ReturnTrue_WhenDateAfterModifiedAndBeforeDeletedProvided2()
        {
            // Arrange
            var sampleData = SampleDataWithAddedModifiedDeleted().ToList();
            _dbContextMock.TestHistEntities.AddRange(sampleData);

            var sampleData2 = SampleDataWithAddedModifiedDeleted2().ToList();
            _dbContextMock.TestHistEntities.AddRange(sampleData2);

            // Act
            var result = await _historySearcherService
                .ExistsByPredicateAsync(
                    x => x.Id == SampleGuid1 || x.Id == SampleGuid2,
                    DateDeleted.Minus(Duration.FromMilliseconds(1))
                )
                .ConfigureAwait(false);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task Should_ReturnFalse_WhenDateAfterDeletedProvided()
        {
            // Arrange
            var sampleData = SampleDataWithAddedModifiedDeleted().ToList();
            _dbContextMock.TestHistEntities.AddRange(sampleData);

            // Act
            var result = await _historySearcherService
                .ExistsByPredicateAsync(
                    x => x.Id == SampleGuid1,
                    DateDeleted.Plus(Duration.FromMilliseconds(1))
                )
                .ConfigureAwait(false);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task Should_ReturnFalse_WhenDateAfterDeletedProvided2()
        {
            // Arrange
            var sampleData = SampleDataWithAddedModifiedDeleted().ToList();
            _dbContextMock.TestHistEntities.AddRange(sampleData);

            var sampleData2 = SampleDataWithAddedModifiedDeleted2().ToList();
            _dbContextMock.TestHistEntities.AddRange(sampleData2);

            // Act
            var result = await _historySearcherService
                .ExistsByPredicateAsync(
                    x => x.Id == SampleGuid1 || x.Id == SampleGuid2,
                    DateDeleted2.Plus(Duration.FromMilliseconds(1))
                )
                .ConfigureAwait(false);

            // Assert
            Assert.False(result);
        }
    }

    public class CountByPredicateAsync : HistorySearcherServiceTest
    {
        [Fact]
        public async Task Should_ReturnZero_WhenNoHistory()
        {
            // Act
            var result = await _historySearcherService
                .CountByPredicateAsync(x => x.Id == Guid.NewGuid(), Instant.MaxValue)
                .ConfigureAwait(false);

            // Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public async Task Should_ReturnZero_WhenDateBeforeAddedProvided()
        {
            // Arrange
            var sampleData = SampleDataWithAddedModifiedDeleted().ToList();
            _dbContextMock.TestHistEntities.AddRange(sampleData);

            // Act
            var result = await _historySearcherService
                .CountByPredicateAsync(
                    x => x.Id == SampleGuid1,
                    DateAdded.Minus(Duration.FromMilliseconds(1))
                )
                .ConfigureAwait(false);

            // Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public async Task Should_ReturnOne_WhenDateAfterAddedAndBeforeModifiedProvided()
        {
            // Arrange
            var sampleData = SampleDataWithAddedModifiedDeleted().ToList();
            _dbContextMock.TestHistEntities.AddRange(sampleData);

            // Act
            var result = await _historySearcherService
                .CountByPredicateAsync(
                    x => x.Id == SampleGuid1,
                    DateAdded.Plus(Duration.FromMilliseconds(1))
                )
                .ConfigureAwait(false);

            // Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task Should_ReturnOne_WhenDateAfterModifiedAndBeforeDeletedProvided()
        {
            // Arrange
            var sampleData = SampleDataWithAddedModifiedDeleted().ToList();
            _dbContextMock.TestHistEntities.AddRange(sampleData);

            // Act
            var result = await _historySearcherService
                .CountByPredicateAsync(
                    x => x.Id == SampleGuid1,
                    DateDeleted.Minus(Duration.FromMilliseconds(1))
                )
                .ConfigureAwait(false);

            // Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task Should_ReturnZero_WhenDateAfterDeletedProvided()
        {
            // Arrange
            var sampleData = SampleDataWithAddedModifiedDeleted().ToList();
            _dbContextMock.TestHistEntities.AddRange(sampleData);

            // Act
            var result = await _historySearcherService
                .CountByPredicateAsync(
                    x => x.Id == sampleData[2].Id,
                    DateDeleted.Plus(Duration.FromMilliseconds(1))
                )
                .ConfigureAwait(false);

            // Assert
            Assert.Equal(0, result);
        }

        // TODO: Add tests for predicate matching multiple different entities
    }
}
