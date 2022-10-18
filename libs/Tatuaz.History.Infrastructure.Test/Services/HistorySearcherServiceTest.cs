using NodaTime;
using Tatuaz.History.DataAccess.Services;
using Tatuaz.History.DataAccess.Test.Utils;
using Tatuaz.Shared.Domain.Entities.Hist.Common;

namespace Tatuaz.History.DataAccess.Test.Services;

public class HistorySearcherServiceTest
{
    private readonly HistDbContextMock _dbContextMock;
    private readonly HistorySearcherService<TestHistEntity, Guid> _historySearcherService;

    protected HistorySearcherServiceTest()
    {
        _dbContextMock = new HistDbContextMock();
        _historySearcherService = new HistorySearcherService<TestHistEntity, Guid>(_dbContextMock.Object);
    }

    private static Instant DateBeforeAdded => Instant.FromUtc(2019, 12, 31, 23, 59, 59);
    private static Instant DateAfterAddedAndBeforeModified => Instant.FromUtc(2020, 1, 1, 23, 59, 59);
    private static Instant DateAfterModifiedAndBeforeDeleted => Instant.FromUtc(2020, 1, 2, 23, 59, 59);
    private static Instant DateAfterDeleted => Instant.FromUtc(2020, 1, 3, 23, 59, 59);

    private static IEnumerable<TestHistEntity> SampleDataWithAddedModifiedDeleted()
    {
        return new List<TestHistEntity>
        {
            new()
            {
                Id = Guid.Parse("AE070011-E390-4D68-8BF5-CA34C7DE02A6"),
                Name = "Test1",
                HistState = HistState.Added,
                HistDumpedAt = Instant.FromUtc(2020, 1, 1, 0, 0, 0)
            },
            new()
            {
                Id = Guid.Parse("AE070011-E490-4D68-8BF5-CA34C7DE02A6"),
                Name = "Test2",
                HistState = HistState.Modified,
                HistDumpedAt = Instant.FromUtc(2020, 1, 2, 0, 0, 0)
            },
            new()
            {
                Id = Guid.Parse("AE070011-E590-4D68-8BF5-CA34C7DE02A6"),
                Name = "Test3",
                HistState = HistState.Deleted,
                HistDumpedAt = Instant.FromUtc(2020, 1, 3, 0, 0, 0)
            }
        };
    }

    public class GetByIdAsync : HistorySearcherServiceTest
    {
        [Fact]
        public async Task Should_ReturnNull_WhenNoHistory()
        {
            // Act
            var result = await _historySearcherService.GetByIdAsync(Guid.NewGuid(), Instant.MaxValue)
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
            var result = await _historySearcherService.GetByIdAsync(sampleData.First().Id, DateBeforeAdded)
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
                .GetByIdAsync(sampleData.First().Id, DateAfterAddedAndBeforeModified).ConfigureAwait(false);

            // Assert
#pragma warning disable CS8602
            Assert.NotNull(result);
            Assert.Equal(sampleData.First().Id, result.Id);
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
            var result = await _historySearcherService.GetByIdAsync(sampleData[1].Id, DateAfterModifiedAndBeforeDeleted)
                .ConfigureAwait(false);

            // Assert
#pragma warning disable CS8602
            Assert.NotNull(result);
            Assert.Equal(sampleData[1].Id, result.Id);
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
            var result = await _historySearcherService.GetByIdAsync(sampleData[2].Id, DateAfterDeleted)
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
            var result = await _historySearcherService.ExistsByIdAsync(Guid.NewGuid(), Instant.MaxValue)
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
            var result = await _historySearcherService.ExistsByIdAsync(sampleData.First().Id, DateBeforeAdded)
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
                .ExistsByIdAsync(sampleData.First().Id, DateAfterAddedAndBeforeModified).ConfigureAwait(false);

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
                .ExistsByIdAsync(sampleData[1].Id, DateAfterModifiedAndBeforeDeleted).ConfigureAwait(false);

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
            var result = await _historySearcherService.ExistsByIdAsync(sampleData[2].Id, DateAfterDeleted)
                .ConfigureAwait(false);

            // Assert
            Assert.False(result);
        }
    }

    public class GetBySpecificationAsync
    {
    }

    public class GetBySpecificationWithPagingAsync
    {
    }

    public class ExistsByPredicateAsync : HistorySearcherServiceTest
    {
        [Fact]
        public async Task Should_ReturnFalse_WhenNoHistory()
        {
            // Act
            var result = await _historySearcherService
                .ExistsByPredicateAsync(x => x.Id == Guid.NewGuid(), Instant.MaxValue).ConfigureAwait(false);

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
                .ExistsByPredicateAsync(x => x.Id == sampleData.First().Id, DateBeforeAdded).ConfigureAwait(false);

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
                .ExistsByPredicateAsync(x => x.Id == sampleData.First().Id, DateAfterAddedAndBeforeModified)
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
                .ExistsByPredicateAsync(x => x.Id == sampleData[1].Id, DateAfterModifiedAndBeforeDeleted)
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
                .ExistsByPredicateAsync(x => x.Id == sampleData[2].Id, DateAfterDeleted).ConfigureAwait(false);

            // Assert
            Assert.False(result);
        }

        // TODO: Add tests for predicate matching multiple different entities
    }

    public class CountByPredicateAsync : HistorySearcherServiceTest
    {
        [Fact]
        public async Task Should_ReturnZero_WhenNoHistory()
        {
            // Act
            var result = await _historySearcherService
                .CountByPredicateAsync(x => x.Id == Guid.NewGuid(), Instant.MaxValue).ConfigureAwait(false);

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
                .CountByPredicateAsync(x => x.Id == sampleData.First().Id, DateBeforeAdded).ConfigureAwait(false);

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
                .CountByPredicateAsync(x => x.Id == sampleData.First().Id, DateAfterAddedAndBeforeModified)
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
                .CountByPredicateAsync(x => x.Id == sampleData[1].Id, DateAfterModifiedAndBeforeDeleted)
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
                .CountByPredicateAsync(x => x.Id == sampleData[2].Id, DateAfterDeleted).ConfigureAwait(false);

            // Assert
            Assert.Equal(0, result);
        }

        // TODO: Add tests for predicate matching multiple different entities
    }
}