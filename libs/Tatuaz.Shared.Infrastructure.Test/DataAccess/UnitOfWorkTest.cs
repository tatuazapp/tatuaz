using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using NodaTime;
using NodaTime.Testing;
using Tatuaz.History.Queue.Contracts;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;
using Tatuaz.Shared.Infrastructure.DataAccess;
using Tatuaz.Shared.Infrastructure.Test.Database.Simple.Fakers;
using Tatuaz.Shared.Infrastructure.Test.Database.Simple.Models;
using Tatuaz.Shared.Infrastructure.Test.Helpers;
using Tatuaz.Testing.Fakes.Common;
using Tatuaz.Testing.Fakes.Infrastructure;
using Tatuaz.Testing.Mocks.Queues;
using Xunit;

namespace Tatuaz.Shared.Infrastructure.Test.DataAccess;

public class UnitOfWorkTest
{
    private readonly IClock _clock;
    private readonly DbContext _dbContext;
    private readonly IPrimitiveValuesGenerator _primitiveValuesGenerator;
    private readonly TimeSpan _testPrecision = TimeSpan.FromMilliseconds(10);

    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserAccessor _userAccessor;

    public UnitOfWorkTest(
        IUnitOfWork unitOfWork,
        IPrimitiveValuesGenerator primitiveValuesGenerator,
        IUserAccessor userAccessor,
        DbContext dbContext,
        IClock clock
    )
    {
        _unitOfWork = unitOfWork;
        _primitiveValuesGenerator = primitiveValuesGenerator;
        _userAccessor = userAccessor;
        _dbContext = dbContext;
        _clock = clock;
    }

    public class SaveChangesAsyncTest : UnitOfWorkTest
    {
        public SaveChangesAsyncTest(
            IUnitOfWork unitOfWork,
            IPrimitiveValuesGenerator primitiveValuesGenerator,
            IUserAccessor userAccessor,
            DbContext dbContext,
            IClock clock
        ) : base(unitOfWork, primitiveValuesGenerator, userAccessor, dbContext, clock)
        {
        }

        [Fact]
        public async Task Should_SaveInsertedElement()
        {
            var expected = AuthorFaker.Generate();

            _dbContext.Add(expected);
            var changes = await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);
            var actual = await _dbContext
                .Set<Author>()
                .FirstAsync(x => x.Id == expected.Id)
                .ConfigureAwait(false);

            Assert.Equal(expected, actual);
            Assert.Equal(1, changes);
        }

        [Fact]
        public async Task Should_SaveItemChangesWithTrackingElement()
        {
            var author = AuthorFaker.Generate();

            _dbContext.Add(author);
            await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);
            var expected = await _dbContext
                .Set<Author>()
                .FirstAsync(x => x.Id == author.Id)
                .ConfigureAwait(false);

            expected.FirstName = "Adam";
            var changes = await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);
            var actual = await _dbContext
                .Set<Author>()
                .FirstAsync(x => x.Id == author.Id)
                .ConfigureAwait(false);

            Assert.Equal(expected, actual);
            Assert.Equal(1, changes);
        }

        [Fact]
        public async Task Should_DiscardItemChangesWithNoTrackingElement()
        {
            var expected = AuthorFaker.Generate();

            _dbContext.Add(expected);
            await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);
            var toChange = await _dbContext
                .Set<Author>()
                .AsNoTracking()
                .FirstAsync(x => x.Id == expected.Id)
                .ConfigureAwait(false);

            toChange.FirstName = "Adam";
            var changes = await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);
            var actual = await _dbContext
                .Set<Author>()
                .FirstAsync(x => x.Id == expected.Id)
                .ConfigureAwait(false);

            Assert.Equal(expected, actual);
            Assert.Equal(0, changes);
        }

        [Fact]
        public async Task Should_DeleteItemOnRemoveElement()
        {
            var author = AuthorFaker.Generate();

            _dbContext.Add(author);
            await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);
            var added = await _dbContext
                .Set<Author>()
                .FirstAsync(x => x.Id == author.Id)
                .ConfigureAwait(false);

            Assert.Equal(author, added);

            _dbContext.Remove(author);
            var changes = await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);
            var deleted = await _dbContext
                .Set<Author>()
                .FirstOrDefaultAsync(x => x.Id == author.Id)
                .ConfigureAwait(false);

            Assert.Null(deleted);
            Assert.Equal(1, changes);
        }

        [Fact]
        public async Task Should_AddUserContextToInsertedElement()
        {
            var author = AuthorFaker.Generate();

            _dbContext.Add(author);
            var changes = await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);
            var actual = await _dbContext
                .Set<Author>()
                .FirstAsync(x => x.Id == author.Id)
                .ConfigureAwait(false);

            Assert.Equal(_primitiveValuesGenerator.Guids(0).ToString(), actual.CreatedBy);
            Assert.Equal(_primitiveValuesGenerator.Guids(0).ToString(), actual.ModifiedBy);
            Assert.Equal(
                _clock.GetCurrentInstant().ToDateTimeUtc(),
                actual.CreatedAt.ToDateTimeUtc(),
                _testPrecision
            );
            Assert.Equal(
                _clock.GetCurrentInstant().ToDateTimeUtc(),
                actual.ModifiedAt.ToDateTimeUtc(),
                _testPrecision
            );
            Assert.Equal(1, changes);
        }

        [Fact]
        public async Task Should_AddUserContextToModifiedElement()
        {
            var author = AuthorFaker.Generate();

            ((UserAccessorFake)_userAccessor).SetCurrentIndex(0);

            _dbContext.Add(author);
            var changes1 = await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);
            var toChange = await _dbContext
                .Set<Author>()
                .FirstAsync(x => x.Id == author.Id)
                .ConfigureAwait(false);

            ((FakeClock)_clock).Advance(Duration.FromMilliseconds(200));

            ((UserAccessorFake)_userAccessor).SetCurrentIndex(1);

            toChange.FirstName = "Adam";
            var changes2 = await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);
            var actual = await _dbContext
                .Set<Author>()
                .FirstAsync(x => x.Id == author.Id)
                .ConfigureAwait(false);

            Assert.Equal(_primitiveValuesGenerator.Guids(0).ToString(), actual.CreatedBy);
            Assert.Equal(_primitiveValuesGenerator.Guids(1).ToString(), actual.ModifiedBy);
            Assert.Equal(
                _clock.GetCurrentInstant().ToDateTimeUtc().AddMilliseconds(-200),
                actual.CreatedAt.ToDateTimeUtc(),
                _testPrecision
            );
            Assert.Equal(
                _clock.GetCurrentInstant().ToDateTimeUtc(),
                actual.ModifiedAt.ToDateTimeUtc(),
                _testPrecision
            );
            Assert.Equal(1, changes1);
            Assert.Equal(1, changes2);
        }

        [Fact]
        public async Task Should_DumpHistoryChangesOnCreate()
        {
            var sendEndpointProviderMock = new SendEndpointProviderMock();
            new UnitOfWorkTestAccessor((UnitOfWork)_unitOfWork).SendEndpointProvider =
                sendEndpointProviderMock.Object;

            var author = AuthorFaker.Generate();

            _dbContext.Add(author);
            await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);

            sendEndpointProviderMock.Verify(x => x.GetSendEndpoint(It.IsAny<Uri>()), Times.Once);
            sendEndpointProviderMock.SendEndpointMock.Verify(
                x => x.Send(It.IsAny<DumpHistoryOrder>(), It.IsAny<CancellationToken>()),
                Times.Once
            );
        }

        [Fact]
        public async Task Should_DumpMultipleHistoryChangesOnCreate()
        {
            var sendEndpointProviderMock = new SendEndpointProviderMock();
            new UnitOfWorkTestAccessor((UnitOfWork)_unitOfWork).SendEndpointProvider =
                sendEndpointProviderMock.Object;

            var author = AuthorFaker.Generate();
            var book = BookFaker.FromAuthorId(author.Id);

            _dbContext.Add(author);
            _dbContext.Add(book);
            await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);

            sendEndpointProviderMock.Verify(x => x.GetSendEndpoint(It.IsAny<Uri>()), Times.Once);
            sendEndpointProviderMock.SendEndpointMock.Verify(
                x => x.Send(It.IsAny<DumpHistoryOrder>(), It.IsAny<CancellationToken>()),
                Times.Exactly(2)
            );
        }

        [Fact]
        public async Task Should_DumpHistoryChangesOnUpdate()
        {
            var sendEndpointProviderMock = new SendEndpointProviderMock();
            new UnitOfWorkTestAccessor((UnitOfWork)_unitOfWork).SendEndpointProvider =
                sendEndpointProviderMock.Object;

            var author = AuthorFaker.Generate();

            _dbContext.Add(author);
            await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);

            author.FirstName = "Adam";
            await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);

            sendEndpointProviderMock.Verify(
                x => x.GetSendEndpoint(It.IsAny<Uri>()),
                Times.Exactly(2)
            );
            sendEndpointProviderMock.SendEndpointMock.Verify(
                x => x.Send(It.IsAny<DumpHistoryOrder>(), It.IsAny<CancellationToken>()),
                Times.Exactly(2)
            );
        }

        [Fact]
        public async Task Should_DumpMultipleHistoryChangesOnUpdate()
        {
            var sendEndpointProviderMock = new SendEndpointProviderMock();
            new UnitOfWorkTestAccessor((UnitOfWork)_unitOfWork).SendEndpointProvider =
                sendEndpointProviderMock.Object;

            var author = AuthorFaker.Generate();
            var book = BookFaker.FromAuthorId(author.Id);

            _dbContext.Add(author);
            _dbContext.Add(book);
            await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);

            author.FirstName = "Adam";
            book.Title = "Test2";
            await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);

            sendEndpointProviderMock.Verify(
                x => x.GetSendEndpoint(It.IsAny<Uri>()),
                Times.Exactly(2)
            );
            sendEndpointProviderMock.SendEndpointMock.Verify(
                x => x.Send(It.IsAny<DumpHistoryOrder>(), It.IsAny<CancellationToken>()),
                Times.Exactly(4)
            );
        }

        [Fact]
        public async Task Should_DumpHistoryChangesOnDelete()
        {
            var sendEndpointProviderMock = new SendEndpointProviderMock();
            new UnitOfWorkTestAccessor((UnitOfWork)_unitOfWork).SendEndpointProvider =
                sendEndpointProviderMock.Object;

            var author = AuthorFaker.Generate();

            _dbContext.Add(author);
            await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);

            _dbContext.Remove(author);
            await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);

            sendEndpointProviderMock.Verify(
                x => x.GetSendEndpoint(It.IsAny<Uri>()),
                Times.Exactly(2)
            );
            sendEndpointProviderMock.SendEndpointMock.Verify(
                x => x.Send(It.IsAny<DumpHistoryOrder>(), It.IsAny<CancellationToken>()),
                Times.Exactly(2)
            );
        }

        [Fact]
        public async Task Should_DumpMultipleHistoryChangesOnDelete()
        {
            var sendEndpointProviderMock = new SendEndpointProviderMock();
            new UnitOfWorkTestAccessor((UnitOfWork)_unitOfWork).SendEndpointProvider =
                sendEndpointProviderMock.Object;

            var author = AuthorFaker.Generate();
            var book = BookFaker.FromAuthorId(author.Id);

            _dbContext.Add(author);
            _dbContext.Add(book);
            await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);

            _dbContext.Remove(author);
            _dbContext.Remove(book);
            await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);

            sendEndpointProviderMock.Verify(
                x => x.GetSendEndpoint(It.IsAny<Uri>()),
                Times.Exactly(2)
            );
            sendEndpointProviderMock.SendEndpointMock.Verify(
                x => x.Send(It.IsAny<DumpHistoryOrder>(), It.IsAny<CancellationToken>()),
                Times.Exactly(4)
            );
        }
    }

    public class RunInTransactionAsyncTest : UnitOfWorkTest
    {
        public RunInTransactionAsyncTest(
            IUnitOfWork unitOfWork,
            IPrimitiveValuesGenerator primitiveValuesGenerator,
            IUserAccessor userAccessor,
            DbContext dbContext,
            IClock clock
        ) : base(unitOfWork, primitiveValuesGenerator, userAccessor, dbContext, clock)
        {
        }

        [Fact]
        public async Task Should_SaveInsertedElement()
        {
            var author = AuthorFaker.Generate();

            var changes = 0;
            await _unitOfWork
                .RunInTransactionAsync(async ct =>
                {
                    _dbContext.Add(author);
                    changes = await _unitOfWork.SaveChangesAsync(ct).ConfigureAwait(false);
                })
                .ConfigureAwait(false);

            var actual = await _dbContext
                .Set<Author>()
                .FirstAsync(x => x.Id == author.Id)
                .ConfigureAwait(false);

            Assert.Equal(author, actual);
            Assert.Equal(1, changes);
        }

        [Fact]
        public async Task Should_SaveItemChangesWithTrackingElement()
        {
            var author = AuthorFaker.Generate();

            _dbContext.Add(author);
            await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);
            var expected = await _dbContext
                .Set<Author>()
                .FirstAsync(x => x.Id == author.Id)
                .ConfigureAwait(false);

            var changes = 0;
            await _unitOfWork
                .RunInTransactionAsync(async ct =>
                {
                    expected.FirstName = "Adam";
                    changes = await _unitOfWork.SaveChangesAsync(ct).ConfigureAwait(false);
                })
                .ConfigureAwait(false);

            var actual = await _dbContext
                .Set<Author>()
                .FirstAsync(x => x.Id == author.Id)
                .ConfigureAwait(false);

            Assert.Equal(expected, actual);
            Assert.Equal(1, changes);
        }

        [Fact]
        public async Task Should_DiscardItemChangesWithNoTrackingElement()
        {
            var author = AuthorFaker.Generate();

            _dbContext.Add(author);
            await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);
            var toChange = await _dbContext
                .Set<Author>()
                .AsNoTracking()
                .FirstAsync(x => x.Id == author.Id)
                .ConfigureAwait(false);

            var changes = 0;
            await _unitOfWork
                .RunInTransactionAsync(async ct =>
                {
                    toChange.FirstName = "Jan";
                    changes = await _unitOfWork.SaveChangesAsync(ct).ConfigureAwait(false);
                })
                .ConfigureAwait(false);
            var actual = await _dbContext
                .Set<Author>()
                .FirstAsync(x => x.Id == author.Id)
                .ConfigureAwait(false);

            Assert.Equal(author, actual);
            Assert.Equal(0, changes);
        }

        [Fact]
        public async Task Should_DeleteItemOnRemoveElement()
        {
            var author = AuthorFaker.Generate();

            _dbContext.Add(author);
            await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);
            var added = await _dbContext
                .Set<Author>()
                .FirstAsync(x => x.Id == author.Id)
                .ConfigureAwait(false);

            Assert.Equal(author, added);

            var changes = 0;
            await _unitOfWork
                .RunInTransactionAsync(async ct =>
                {
                    _dbContext.Remove(author);
                    changes = await _unitOfWork.SaveChangesAsync(ct).ConfigureAwait(false);
                })
                .ConfigureAwait(false);
            var deleted = await _dbContext
                .Set<Author>()
                .FirstOrDefaultAsync(x => x.Id == author.Id)
                .ConfigureAwait(false);

            Assert.Null(deleted);
            Assert.Equal(1, changes);
        }

        [Fact]
        public async Task Should_RollbackOnFailure()
        {
            var author = AuthorFaker.Generate();

            var changes1 = 0;
            var changes2 = 0;
            await _unitOfWork
                .RunInTransactionAsync(async ct =>
                {
                    _dbContext.Add(author);
                    changes1 = await _unitOfWork.SaveChangesAsync(ct).ConfigureAwait(false);

                    _dbContext.Add(author);
                    changes2 = await _unitOfWork.SaveChangesAsync(ct).ConfigureAwait(false);
                })
                .ConfigureAwait(false);

            var addedEntity = await _dbContext
                .Set<Author>()
                .FirstOrDefaultAsync(x => x.Id == author.Id)
                .ConfigureAwait(false);

            Assert.Null(addedEntity);
            Assert.Equal(1, changes1);
            Assert.Equal(0, changes2);
        }

        [Fact]
        public async Task Should_ExecuteOnFailureOnFailure()
        {
            var author = AuthorFaker.Generate();

            var changes1 = 0;
            var changes2 = 0;
            var failureExecuted = false;
            await _unitOfWork
                .RunInTransactionAsync(
                    async ct =>
                    {
                        _dbContext.Add(author);
                        changes1 = await _unitOfWork.SaveChangesAsync(ct).ConfigureAwait(false);

                        _dbContext.Add(author);
                        changes2 = await _unitOfWork.SaveChangesAsync(ct).ConfigureAwait(false);
                    },
                    _ => failureExecuted = true
                )
                .ConfigureAwait(false);

            Assert.True(failureExecuted);
            Assert.Equal(1, changes1);
            Assert.Equal(0, changes2);
        }

        [Fact]
        public async Task Should_AddUserContextToInsertedElement()
        {
            var author = AuthorFaker.Generate();

            ((UserAccessorFake)_userAccessor).SetCurrentIndex(0);

            var changes = 0;
            await _unitOfWork
                .RunInTransactionAsync(async ct =>
                {
                    _dbContext.Add(author);
                    changes = await _unitOfWork.SaveChangesAsync(ct).ConfigureAwait(false);
                })
                .ConfigureAwait(false);
            var actual = await _dbContext
                .Set<Author>()
                .FirstAsync(x => x.Id == author.Id)
                .ConfigureAwait(false);

            Assert.Equal(_primitiveValuesGenerator.Guids(0).ToString(), actual.CreatedBy);
            Assert.Equal(_primitiveValuesGenerator.Guids(0).ToString(), actual.ModifiedBy);
            Assert.Equal(
                _clock.GetCurrentInstant().ToDateTimeUtc(),
                actual.CreatedAt.ToDateTimeUtc(),
                _testPrecision
            );
            Assert.Equal(
                _clock.GetCurrentInstant().ToDateTimeUtc(),
                actual.ModifiedAt.ToDateTimeUtc(),
                _testPrecision
            );
            Assert.Equal(1, changes);
        }

        [Fact]
        public async Task Should_AddUserContextToModifiedElement()
        {
            var author = AuthorFaker.Generate();

            ((UserAccessorFake)_userAccessor).SetCurrentIndex(0);

            var changes1 = 0;
            await _unitOfWork
                .RunInTransactionAsync(async ct =>
                {
                    _dbContext.Add(author);
                    changes1 = await _unitOfWork.SaveChangesAsync(ct).ConfigureAwait(false);
                })
                .ConfigureAwait(false);
            var toChange = await _dbContext
                .Set<Author>()
                .FirstAsync(x => x.Id == author.Id)
                .ConfigureAwait(false);

            ((FakeClock)_clock).Advance(Duration.FromMilliseconds(200));

            ((UserAccessorFake)_userAccessor).SetCurrentIndex(1);

            var changes2 = 0;
            await _unitOfWork
                .RunInTransactionAsync(async ct =>
                {
                    toChange.FirstName = "Adam";
                    changes2 = await _unitOfWork.SaveChangesAsync(ct).ConfigureAwait(false);
                })
                .ConfigureAwait(false);
            var actual = await _dbContext
                .Set<Author>()
                .FirstAsync(x => x.Id == author.Id)
                .ConfigureAwait(false);

            Assert.Equal(_primitiveValuesGenerator.Guids(0).ToString(), actual.CreatedBy);
            Assert.Equal(_primitiveValuesGenerator.Guids(1).ToString(), actual.ModifiedBy);
            Assert.Equal(
                _clock.GetCurrentInstant().ToDateTimeUtc().AddMilliseconds(-200),
                actual.CreatedAt.ToDateTimeUtc(),
                _testPrecision
            );
            Assert.Equal(
                _clock.GetCurrentInstant().ToDateTimeUtc(),
                actual.ModifiedAt.ToDateTimeUtc(),
                _testPrecision
            );
            Assert.Equal(1, changes1);
            Assert.Equal(1, changes2);
        }

        [Fact]
        public async Task Should_DumpHistoryChangesOnCreate()
        {
            var sendEndpointProviderMock = new SendEndpointProviderMock();
            new UnitOfWorkTestAccessor((UnitOfWork)_unitOfWork).SendEndpointProvider =
                sendEndpointProviderMock.Object;

            var author = AuthorFaker.Generate();

            await _unitOfWork
                .RunInTransactionAsync(async ct =>
                {
                    _dbContext.Add(author);
                    await _unitOfWork.SaveChangesAsync(ct).ConfigureAwait(false);
                })
                .ConfigureAwait(false);

            sendEndpointProviderMock.Verify(x => x.GetSendEndpoint(It.IsAny<Uri>()), Times.Once);
            sendEndpointProviderMock.SendEndpointMock.Verify(
                x => x.Send(It.IsAny<DumpHistoryOrder>(), It.IsAny<CancellationToken>()),
                Times.Once
            );
        }

        [Fact]
        public async Task Should_DumpMultipleHistoryChangesOnCreate()
        {
            var sendEndpointProviderMock = new SendEndpointProviderMock();
            new UnitOfWorkTestAccessor((UnitOfWork)_unitOfWork).SendEndpointProvider =
                sendEndpointProviderMock.Object;

            var author = AuthorFaker.Generate();
            var book = BookFaker.FromAuthorId(author.Id);

            await _unitOfWork
                .RunInTransactionAsync(async ct =>
                {
                    _dbContext.Add(author);
                    _dbContext.Add(book);
                    await _unitOfWork.SaveChangesAsync(ct).ConfigureAwait(false);
                })
                .ConfigureAwait(false);

            sendEndpointProviderMock.Verify(x => x.GetSendEndpoint(It.IsAny<Uri>()), Times.Once);
            sendEndpointProviderMock.SendEndpointMock.Verify(
                x => x.Send(It.IsAny<DumpHistoryOrder>(), It.IsAny<CancellationToken>()),
                Times.Exactly(2)
            );
        }

        [Fact]
        public async Task Should_DumpHistoryChangesOnUpdate()
        {
            var sendEndpointProviderMock = new SendEndpointProviderMock();
            new UnitOfWorkTestAccessor((UnitOfWork)_unitOfWork).SendEndpointProvider =
                sendEndpointProviderMock.Object;

            var author = AuthorFaker.Generate();

            _dbContext.Add(author);
            await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);

            await _unitOfWork
                .RunInTransactionAsync(async ct =>
                {
                    author.FirstName = "Janusz";
                    await _unitOfWork.SaveChangesAsync(ct).ConfigureAwait(false);
                })
                .ConfigureAwait(false);

            sendEndpointProviderMock.Verify(
                x => x.GetSendEndpoint(It.IsAny<Uri>()),
                Times.Exactly(2)
            );
            sendEndpointProviderMock.SendEndpointMock.Verify(
                x => x.Send(It.IsAny<DumpHistoryOrder>(), It.IsAny<CancellationToken>()),
                Times.Exactly(2)
            );
        }

        [Fact]
        public async Task Should_DumpMultipleHistoryChangesOnUpdate()
        {
            var sendEndpointProviderMock = new SendEndpointProviderMock();
            new UnitOfWorkTestAccessor((UnitOfWork)_unitOfWork).SendEndpointProvider =
                sendEndpointProviderMock.Object;

            var author = AuthorFaker.Generate();
            var book = BookFaker.FromAuthorId(author.Id);

            _dbContext.Add(author);
            _dbContext.Add(book);
            await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);

            await _unitOfWork
                .RunInTransactionAsync(async ct =>
                {
                    author.FirstName = "Janusz";
                    book.Title = "Test2";
                    await _unitOfWork.SaveChangesAsync(ct).ConfigureAwait(false);
                })
                .ConfigureAwait(false);

            sendEndpointProviderMock.Verify(
                x => x.GetSendEndpoint(It.IsAny<Uri>()),
                Times.Exactly(2)
            );
            sendEndpointProviderMock.SendEndpointMock.Verify(
                x => x.Send(It.IsAny<DumpHistoryOrder>(), It.IsAny<CancellationToken>()),
                Times.Exactly(4)
            );
        }

        [Fact]
        public async Task Should_DumpHistoryChangesOnDelete()
        {
            var sendEndpointProviderMock = new SendEndpointProviderMock();
            new UnitOfWorkTestAccessor((UnitOfWork)_unitOfWork).SendEndpointProvider =
                sendEndpointProviderMock.Object;

            var author = AuthorFaker.Generate();

            _dbContext.Add(author);
            await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);

            await _unitOfWork
                .RunInTransactionAsync(async ct =>
                {
                    _dbContext.Remove(author);
                    await _unitOfWork.SaveChangesAsync(ct).ConfigureAwait(false);
                })
                .ConfigureAwait(false);

            sendEndpointProviderMock.Verify(
                x => x.GetSendEndpoint(It.IsAny<Uri>()),
                Times.Exactly(2)
            );
            sendEndpointProviderMock.SendEndpointMock.Verify(
                x => x.Send(It.IsAny<DumpHistoryOrder>(), It.IsAny<CancellationToken>()),
                Times.Exactly(2)
            );
        }

        [Fact]
        public async Task Should_DumpMultipleHistoryChangesOnDelete()
        {
            var sendEndpointProviderMock = new SendEndpointProviderMock();
            new UnitOfWorkTestAccessor((UnitOfWork)_unitOfWork).SendEndpointProvider =
                sendEndpointProviderMock.Object;

            var author = AuthorFaker.Generate();
            var book = BookFaker.FromAuthorId(author.Id);

            _dbContext.Add(author);
            _dbContext.Add(book);
            await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);

            await _unitOfWork
                .RunInTransactionAsync(async ct =>
                {
                    _dbContext.Remove(author);
                    _dbContext.Remove(book);
                    await _unitOfWork.SaveChangesAsync(ct).ConfigureAwait(false);
                })
                .ConfigureAwait(false);

            sendEndpointProviderMock.Verify(
                x => x.GetSendEndpoint(It.IsAny<Uri>()),
                Times.Exactly(2)
            );
            sendEndpointProviderMock.SendEndpointMock.Verify(
                x => x.Send(It.IsAny<DumpHistoryOrder>(), It.IsAny<CancellationToken>()),
                Times.Exactly(4)
            );
        }

        [Fact]
        public async Task ShouldNot_DumpHistoryChangesDirectlyAfterSaveChangesAsync()
        {
            var sendEndpointProviderMock = new SendEndpointProviderMock();
            new UnitOfWorkTestAccessor((UnitOfWork)_unitOfWork).SendEndpointProvider =
                sendEndpointProviderMock.Object;

            var author = AuthorFaker.Generate();

            await _unitOfWork
                .RunInTransactionAsync(async ct =>
                {
                    _dbContext.Add(author);
                    await _unitOfWork.SaveChangesAsync(ct).ConfigureAwait(false);
                    sendEndpointProviderMock.Verify(
                        x => x.GetSendEndpoint(It.IsAny<Uri>()),
                        Times.Never
                    );
                    sendEndpointProviderMock.SendEndpointMock.Verify(
                        x => x.Send(It.IsAny<DumpHistoryOrder>(), It.IsAny<CancellationToken>()),
                        Times.Never
                    );
                })
                .ConfigureAwait(false);

            sendEndpointProviderMock.Verify(x => x.GetSendEndpoint(It.IsAny<Uri>()), Times.Once);
            sendEndpointProviderMock.SendEndpointMock.Verify(
                x => x.Send(It.IsAny<DumpHistoryOrder>(), It.IsAny<CancellationToken>()),
                Times.Once
            );
        }
    }

#pragma warning disable CA1823
    // ReSharper disable once UnusedMember.Local
    private static readonly BookFaker BookFaker = new();

    // ReSharper disable once UnusedMember.Local
    private static readonly AuthorFaker AuthorFaker = new();

    // ReSharper disable once UnusedMember.Local
    private static readonly AwardFaker AwardFaker = new();
#pragma warning restore CA1823
}
