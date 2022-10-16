using Microsoft.EntityFrameworkCore;

using Moq;

using NodaTime;
using NodaTime.Testing;

using Tatuaz.History.Queue.Contracts;
using Tatuaz.Shared.Infrastructure.Abstractions;
using Tatuaz.Shared.Infrastructure.Test.Database.Simple.Models;
using Tatuaz.Shared.Infrastructure.Test.Helpers;
using Tatuaz.Testing.Fakes.Common;
using Tatuaz.Testing.Fakes.Infrastructure;
using Tatuaz.Testing.Mocks.Queues;

namespace Tatuaz.Shared.Infrastructure.Test.DataAccess;

public class UnitOfWorkTest
{
    private readonly TimeSpan _testPrecision = TimeSpan.FromMilliseconds(10);

    public UnitOfWorkTest(IUnitOfWork unitOfWork, IPrimitiveValuesGenerator primitiveValuesGenerator,
        IUserAccessor userAccessor, DbContext dbContext, IClock clock)
    {
        UnitOfWork = unitOfWork;
        PrimitiveValuesGenerator = primitiveValuesGenerator;
        UserAccessor = userAccessor;
        DbContext = dbContext;
        Clock = clock;
    }

    private IUnitOfWork UnitOfWork { get; }
    private IPrimitiveValuesGenerator PrimitiveValuesGenerator { get; }
    private IUserAccessor UserAccessor { get; }
    private DbContext DbContext { get; }
    private IClock Clock { get; }

    public class SaveChangesAsyncTest : UnitOfWorkTest
    {
        [Fact]
        public async Task Should_SaveInsertedElement()
        {
            var expected = new Author { FirstName = "Jan", LastName = "Kowalski" };

            DbContext.Add(expected);
            var changes = await UnitOfWork.SaveChangesAsync();
            var actual = await DbContext.Set<Author>().FirstAsync(x => x.Id == expected.Id);

            Assert.Equal(expected, actual);
            Assert.Equal(1, changes);
        }

        [Fact]
        public async Task Should_SaveItemChangesWithTrackingElement()
        {
            var author = new Author { FirstName = "Jan", LastName = "Kowalski" };

            DbContext.Add(author);
            await UnitOfWork.SaveChangesAsync();
            var expected = await DbContext.Set<Author>().FirstAsync(x => x.Id == author.Id);

            expected.FirstName = "Adam";
            var changes = await UnitOfWork.SaveChangesAsync();
            var actual = await DbContext.Set<Author>().FirstAsync(x => x.Id == author.Id);

            Assert.Equal(expected, actual);
            Assert.Equal(1, changes);
        }

        [Fact]
        public async Task Should_DiscardItemChangesWithNoTrackingElement()
        {
            var expected = new Author { FirstName = "Jan", LastName = "Kowalski" };

            DbContext.Add(expected);
            await UnitOfWork.SaveChangesAsync();
            var toChange = await DbContext.Set<Author>().AsNoTracking().FirstAsync(x => x.Id == expected.Id);

            toChange.FirstName = "Adam";
            var changes = await UnitOfWork.SaveChangesAsync();
            var actual = await DbContext.Set<Author>().FirstAsync(x => x.Id == expected.Id);

            Assert.Equal(expected, actual);
            Assert.Equal(0, changes);
        }

        [Fact]
        public async Task Should_DeleteItemOnRemoveElement()
        {
            var author = new Author { FirstName = "Jan", LastName = "Kowalski" };

            DbContext.Add(author);
            await UnitOfWork.SaveChangesAsync();
            var added = await DbContext.Set<Author>().FirstAsync(x => x.Id == author.Id);

            Assert.Equal(author, added);

            DbContext.Remove(author);
            var changes = await UnitOfWork.SaveChangesAsync();
            var deleted = await DbContext.Set<Author>().FirstOrDefaultAsync(x => x.Id == author.Id);

            Assert.Null(deleted);
            Assert.Equal(1, changes);
        }

        [Fact]
        public async Task Should_AddUserContextToInsertedElement()
        {
            var author = new Author { FirstName = "Jan", LastName = "Kowalski" };

            DbContext.Add(author);
            var changes = await UnitOfWork.SaveChangesAsync();
            var actual = await DbContext.Set<Author>().FirstAsync(x => x.Id == author.Id);

            Assert.Equal(PrimitiveValuesGenerator.Guids(0), actual.CreatedBy);
            Assert.Equal(PrimitiveValuesGenerator.Guids(0), actual.ModifiedBy);
            Assert.Equal(Clock.GetCurrentInstant().ToDateTimeUtc(), actual.CreatedOn.ToDateTimeUtc(), _testPrecision);
            Assert.Equal(Clock.GetCurrentInstant().ToDateTimeUtc(), actual.ModifiedOn.ToDateTimeUtc(), _testPrecision);
            Assert.Equal(1, changes);
        }

        [Fact]
        public async Task Should_AddUserContextToModifiedElement()
        {
            var author = new Author { FirstName = "Jan", LastName = "Kowalski" };

            ((UserAccessorFake)UserAccessor).SetCurrentIndex(0);

            DbContext.Add(author);
            var changes1 = await UnitOfWork.SaveChangesAsync();
            var toChange = await DbContext.Set<Author>().FirstAsync(x => x.Id == author.Id);

            ((FakeClock)Clock).Advance(Duration.FromMilliseconds(200));

            ((UserAccessorFake)UserAccessor).SetCurrentIndex(1);

            toChange.FirstName = "Adam";
            var changes2 = await UnitOfWork.SaveChangesAsync();
            var actual = await DbContext.Set<Author>().FirstAsync(x => x.Id == author.Id);

            Assert.Equal(PrimitiveValuesGenerator.Guids(0), actual.CreatedBy);
            Assert.Equal(PrimitiveValuesGenerator.Guids(1), actual.ModifiedBy);
            Assert.Equal(Clock.GetCurrentInstant().ToDateTimeUtc().AddMilliseconds(-200),
                actual.CreatedOn.ToDateTimeUtc(), _testPrecision);
            Assert.Equal(Clock.GetCurrentInstant().ToDateTimeUtc(), actual.ModifiedOn.ToDateTimeUtc(), _testPrecision);
            Assert.Equal(1, changes1);
            Assert.Equal(1, changes2);
        }

        [Fact]
        public async Task Should_DumpHistoryChangesOnCreate()
        {
            var sendEndpointProviderMock = new SendEndpointProviderMock();
            ((UnitOfWork)UnitOfWork).ReplaceSendEndpointProvider(sendEndpointProviderMock.Object);

            var author = new Author { FirstName = "Jan", LastName = "Kowalski" };

            DbContext.Add(author);
            await UnitOfWork.SaveChangesAsync();

            sendEndpointProviderMock.Verify(x => x.GetSendEndpoint(It.IsAny<Uri>()), Times.Once);
            sendEndpointProviderMock.SendEndpointMock.Verify(
                x => x.Send(It.IsAny<DumpHistoryOrder>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Should_DumpMultipleHistoryChangesOnCreate()
        {
            var sendEndpointProviderMock = new SendEndpointProviderMock();
            ((UnitOfWork)UnitOfWork).ReplaceSendEndpointProvider(sendEndpointProviderMock.Object);

            var author = new Author { FirstName = "Jan", LastName = "Kowalski" };
            var book = new Book { Title = "Test", Author = author };

            DbContext.Add(author);
            DbContext.Add(book);
            await UnitOfWork.SaveChangesAsync();

            sendEndpointProviderMock.Verify(x => x.GetSendEndpoint(It.IsAny<Uri>()), Times.Once);
            sendEndpointProviderMock.SendEndpointMock.Verify(
                x => x.Send(It.IsAny<DumpHistoryOrder>(), It.IsAny<CancellationToken>()), Times.Exactly(2));
        }

        [Fact]
        public async Task Should_DumpHistoryChangesOnUpdate()
        {
            var sendEndpointProviderMock = new SendEndpointProviderMock();
            ((UnitOfWork)UnitOfWork).ReplaceSendEndpointProvider(sendEndpointProviderMock.Object);

            var author = new Author { FirstName = "Jan", LastName = "Kowalski" };

            DbContext.Add(author);
            await UnitOfWork.SaveChangesAsync();

            author.FirstName = "Adam";
            await UnitOfWork.SaveChangesAsync();

            sendEndpointProviderMock.Verify(x => x.GetSendEndpoint(It.IsAny<Uri>()), Times.Exactly(2));
            sendEndpointProviderMock.SendEndpointMock.Verify(
                x => x.Send(It.IsAny<DumpHistoryOrder>(), It.IsAny<CancellationToken>()), Times.Exactly(2));
        }

        [Fact]
        public async Task Should_DumpMultipleHistoryChangesOnUpdate()
        {
            var sendEndpointProviderMock = new SendEndpointProviderMock();
            ((UnitOfWork)UnitOfWork).ReplaceSendEndpointProvider(sendEndpointProviderMock.Object);

            var author = new Author { FirstName = "Jan", LastName = "Kowalski" };
            var book = new Book { Title = "Test", Author = author };

            DbContext.Add(author);
            DbContext.Add(book);
            await UnitOfWork.SaveChangesAsync();

            author.FirstName = "Adam";
            book.Title = "Test2";
            await UnitOfWork.SaveChangesAsync();

            sendEndpointProviderMock.Verify(x => x.GetSendEndpoint(It.IsAny<Uri>()), Times.Exactly(2));
            sendEndpointProviderMock.SendEndpointMock.Verify(
                x => x.Send(It.IsAny<DumpHistoryOrder>(), It.IsAny<CancellationToken>()), Times.Exactly(4));
        }

        [Fact]
        public async Task Should_DumpHistoryChangesOnDelete()
        {
            var sendEndpointProviderMock = new SendEndpointProviderMock();
            ((UnitOfWork)UnitOfWork).ReplaceSendEndpointProvider(sendEndpointProviderMock.Object);

            var author = new Author { FirstName = "Jan", LastName = "Kowalski" };

            DbContext.Add(author);
            await UnitOfWork.SaveChangesAsync();

            DbContext.Remove(author);
            await UnitOfWork.SaveChangesAsync();

            sendEndpointProviderMock.Verify(x => x.GetSendEndpoint(It.IsAny<Uri>()), Times.Exactly(2));
            sendEndpointProviderMock.SendEndpointMock.Verify(
                x => x.Send(It.IsAny<DumpHistoryOrder>(), It.IsAny<CancellationToken>()), Times.Exactly(2));
        }

        [Fact]
        public async Task Should_DumpMultipleHistoryChangesOnDelete()
        {
            var sendEndpointProviderMock = new SendEndpointProviderMock();
            ((UnitOfWork)UnitOfWork).ReplaceSendEndpointProvider(sendEndpointProviderMock.Object);

            var author = new Author { FirstName = "Jan", LastName = "Kowalski" };
            var book = new Book { Title = "Test", Author = author };

            DbContext.Add(author);
            DbContext.Add(book);
            await UnitOfWork.SaveChangesAsync();

            DbContext.Remove(author);
            DbContext.Remove(book);
            await UnitOfWork.SaveChangesAsync();

            sendEndpointProviderMock.Verify(x => x.GetSendEndpoint(It.IsAny<Uri>()), Times.Exactly(2));
            sendEndpointProviderMock.SendEndpointMock.Verify(
                x => x.Send(It.IsAny<DumpHistoryOrder>(), It.IsAny<CancellationToken>()), Times.Exactly(4));
        }

        public SaveChangesAsyncTest(IUnitOfWork unitOfWork, IPrimitiveValuesGenerator primitiveValuesGenerator,
            IUserAccessor userAccessor, DbContext dbContext, IClock clock) : base(unitOfWork, primitiveValuesGenerator,
            userAccessor, dbContext, clock)
        {
        }
    }

    public class RunInTransactionAsyncTest : UnitOfWorkTest
    {
        [Fact]
        public async Task Should_SaveInsertedElement()
        {
            var author = new Author { FirstName = "Jan", LastName = "Kowalski" };

            var changes = 0;
            await UnitOfWork.RunInTransactionAsync(
                async ct => {
                    DbContext.Add(author);
                    changes = await UnitOfWork.SaveChangesAsync(ct);
                }
            );

            var actual = await DbContext.Set<Author>().FirstAsync(x => x.Id == author.Id);

            Assert.Equal(author, actual);
            Assert.Equal(1, changes);
        }

        [Fact]
        public async Task Should_SaveItemChangesWithTrackingElement()
        {
            var author = new Author { FirstName = "Jan", LastName = "Kowalski" };

            DbContext.Add(author);
            await UnitOfWork.SaveChangesAsync();
            var expected = await DbContext.Set<Author>().FirstAsync(x => x.Id == author.Id);

            var changes = 0;
            await UnitOfWork.RunInTransactionAsync(
                async ct => {
                    expected.FirstName = "Adam";
                    changes = await UnitOfWork.SaveChangesAsync(ct);
                }
            );

            var actual = await DbContext.Set<Author>().FirstAsync(x => x.Id == author.Id);

            Assert.Equal(expected, actual);
            Assert.Equal(1, changes);
        }

        [Fact]
        public async Task Should_DiscardItemChangesWithNoTrackingElement()
        {
            var author = new Author { FirstName = "Jan", LastName = "Kowalski" };

            DbContext.Add(author);
            await UnitOfWork.SaveChangesAsync();
            var toChange = await DbContext.Set<Author>().AsNoTracking().FirstAsync(x => x.Id == author.Id);

            var changes = 0;
            await UnitOfWork.RunInTransactionAsync(
                async ct => {
                    toChange.FirstName = "Jan";
                    changes = await UnitOfWork.SaveChangesAsync(ct);
                }
            );
            var actual = await DbContext.Set<Author>().FirstAsync(x => x.Id == author.Id);

            Assert.Equal(author, actual);
            Assert.Equal(0, changes);
        }

        [Fact]
        public async Task Should_DeleteItemOnRemoveElement()
        {
            var author = new Author { FirstName = "Jan", LastName = "Kowalski" };

            DbContext.Add(author);
            await UnitOfWork.SaveChangesAsync();
            var added = await DbContext.Set<Author>().FirstAsync(x => x.Id == author.Id);

            Assert.Equal(author, added);

            var changes = 0;
            await UnitOfWork.RunInTransactionAsync(
                async ct => {
                    DbContext.Remove(author);
                    changes = await UnitOfWork.SaveChangesAsync(ct);
                }
            );
            var deleted = await DbContext.Set<Author>().FirstOrDefaultAsync(x => x.Id == author.Id);

            Assert.Null(deleted);
            Assert.Equal(1, changes);
        }

        [Fact]
        public async Task Should_RollbackOnFailure()
        {
            var author = new Author { FirstName = "Jan", LastName = "Kowalski" };

            var changes1 = 0;
            var changes2 = 0;
            await UnitOfWork.RunInTransactionAsync(
                async ct => {
                    DbContext.Add(author);
                    changes1 = await UnitOfWork.SaveChangesAsync(ct);

                    DbContext.Add(author);
                    changes2 = await UnitOfWork.SaveChangesAsync(ct);
                }
            );

            var addedEntity = await DbContext.Set<Author>().FirstOrDefaultAsync(x => x.Id == author.Id);

            Assert.Null(addedEntity);
            Assert.Equal(1, changes1);
            Assert.Equal(0, changes2);
        }

        [Fact]
        public async Task Should_ExecuteOnFailureOnFailure()
        {
            var author = new Author { FirstName = "Jan", LastName = "Kowalski" };

            var changes1 = 0;
            var changes2 = 0;
            var failureExecuted = false;
            await UnitOfWork.RunInTransactionAsync(
                async ct => {
                    DbContext.Add(author);
                    changes1 = await UnitOfWork.SaveChangesAsync(ct);

                    DbContext.Add(author);
                    changes2 = await UnitOfWork.SaveChangesAsync(ct);
                },
                _ => failureExecuted = true
            );

            Assert.True(failureExecuted);
            Assert.Equal(1, changes1);
            Assert.Equal(0, changes2);
        }

        [Fact]
        public async Task Should_AddUserContextToInsertedElement()
        {
            var author = new Author { FirstName = "Jan", LastName = "Kowalski" };

            ((UserAccessorFake)UserAccessor).SetCurrentIndex(0);

            var changes = 0;
            await UnitOfWork.RunInTransactionAsync(
                async ct => {
                    DbContext.Add(author);
                    changes = await UnitOfWork.SaveChangesAsync(ct);
                }
            );
            var actual = await DbContext.Set<Author>().FirstAsync(x => x.Id == author.Id);

            Assert.Equal(PrimitiveValuesGenerator.Guids(0), actual.CreatedBy);
            Assert.Equal(PrimitiveValuesGenerator.Guids(0), actual.ModifiedBy);
            Assert.Equal(Clock.GetCurrentInstant().ToDateTimeUtc(), actual.CreatedOn.ToDateTimeUtc(), _testPrecision);
            Assert.Equal(Clock.GetCurrentInstant().ToDateTimeUtc(), actual.ModifiedOn.ToDateTimeUtc(), _testPrecision);
            Assert.Equal(1, changes);
        }

        [Fact]
        public async Task Should_AddUserContextToModifiedElement()
        {
            var author = new Author { FirstName = "Jan", LastName = "Kowalski" };

            ((UserAccessorFake)UserAccessor).SetCurrentIndex(0);

            var changes1 = 0;
            await UnitOfWork.RunInTransactionAsync(
                async ct => {
                    DbContext.Add(author);
                    changes1 = await UnitOfWork.SaveChangesAsync(ct);
                }
            );
            var toChange = await DbContext.Set<Author>().FirstAsync(x => x.Id == author.Id);

            ((FakeClock)Clock).Advance(Duration.FromMilliseconds(200));

            ((UserAccessorFake)UserAccessor).SetCurrentIndex(1);

            var changes2 = 0;
            await UnitOfWork.RunInTransactionAsync(
                async ct => {
                    toChange.FirstName = "Adam";
                    changes2 = await UnitOfWork.SaveChangesAsync(ct);
                }
            );
            var actual = await DbContext.Set<Author>().FirstAsync(x => x.Id == author.Id);

            Assert.Equal(PrimitiveValuesGenerator.Guids(0), actual.CreatedBy);
            Assert.Equal(PrimitiveValuesGenerator.Guids(1), actual.ModifiedBy);
            Assert.Equal(Clock.GetCurrentInstant().ToDateTimeUtc().AddMilliseconds(-200),
                actual.CreatedOn.ToDateTimeUtc(), _testPrecision);
            Assert.Equal(Clock.GetCurrentInstant().ToDateTimeUtc(), actual.ModifiedOn.ToDateTimeUtc(), _testPrecision);
            Assert.Equal(1, changes1);
            Assert.Equal(1, changes2);
        }

        [Fact]
        public async Task Should_DumpHistoryChangesOnCreate()
        {
            var sendEndpointProviderMock = new SendEndpointProviderMock();
            ((UnitOfWork)UnitOfWork).ReplaceSendEndpointProvider(sendEndpointProviderMock.Object);

            var author = new Author { FirstName = "Jan", LastName = "Kowalski" };

            await UnitOfWork.RunInTransactionAsync(
                async ct => {
                    DbContext.Add(author);
                    await UnitOfWork.SaveChangesAsync(ct);
                }
            );

            sendEndpointProviderMock.Verify(x => x.GetSendEndpoint(It.IsAny<Uri>()), Times.Once);
            sendEndpointProviderMock.SendEndpointMock.Verify(
                x => x.Send(It.IsAny<DumpHistoryOrder>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Should_DumpMultipleHistoryChangesOnCreate()
        {
            var sendEndpointProviderMock = new SendEndpointProviderMock();
            ((UnitOfWork)UnitOfWork).ReplaceSendEndpointProvider(sendEndpointProviderMock.Object);

            var author = new Author { FirstName = "Jan", LastName = "Kowalski" };
            var book = new Book { Title = "Test", Author = author };

            await UnitOfWork.RunInTransactionAsync(
                async ct => {
                    DbContext.Add(author);
                    DbContext.Add(book);
                    await UnitOfWork.SaveChangesAsync(ct);
                }
            );

            sendEndpointProviderMock.Verify(x => x.GetSendEndpoint(It.IsAny<Uri>()), Times.Once);
            sendEndpointProviderMock.SendEndpointMock.Verify(
                x => x.Send(It.IsAny<DumpHistoryOrder>(), It.IsAny<CancellationToken>()), Times.Exactly(2));
        }

        [Fact]
        public async Task Should_DumpHistoryChangesOnUpdate()
        {
            var sendEndpointProviderMock = new SendEndpointProviderMock();
            ((UnitOfWork)UnitOfWork).ReplaceSendEndpointProvider(sendEndpointProviderMock.Object);

            var author = new Author { FirstName = "Jan", LastName = "Kowalski" };

            DbContext.Add(author);
            await UnitOfWork.SaveChangesAsync();

            await UnitOfWork.RunInTransactionAsync(
                async ct => {
                    author.FirstName = "Janusz";
                    await UnitOfWork.SaveChangesAsync(ct);
                }
            );

            sendEndpointProviderMock.Verify(x => x.GetSendEndpoint(It.IsAny<Uri>()), Times.Exactly(2));
            sendEndpointProviderMock.SendEndpointMock.Verify(
                x => x.Send(It.IsAny<DumpHistoryOrder>(), It.IsAny<CancellationToken>()), Times.Exactly(2));
        }

        [Fact]
        public async Task Should_DumpMultipleHistoryChangesOnUpdate()
        {
            var sendEndpointProviderMock = new SendEndpointProviderMock();
            ((UnitOfWork)UnitOfWork).ReplaceSendEndpointProvider(sendEndpointProviderMock.Object);

            var author = new Author { FirstName = "Jan", LastName = "Kowalski" };
            var book = new Book { Title = "Test", Author = author };

            DbContext.Add(author);
            DbContext.Add(book);
            await UnitOfWork.SaveChangesAsync();

            await UnitOfWork.RunInTransactionAsync(
                async ct => {
                    author.FirstName = "Janusz";
                    book.Title = "Test2";
                    await UnitOfWork.SaveChangesAsync(ct);
                }
            );

            sendEndpointProviderMock.Verify(x => x.GetSendEndpoint(It.IsAny<Uri>()), Times.Exactly(2));
            sendEndpointProviderMock.SendEndpointMock.Verify(
                x => x.Send(It.IsAny<DumpHistoryOrder>(), It.IsAny<CancellationToken>()), Times.Exactly(4));
        }

        [Fact]
        public async Task Should_DumpHistoryChangesOnDelete()
        {
            var sendEndpointProviderMock = new SendEndpointProviderMock();
            ((UnitOfWork)UnitOfWork).ReplaceSendEndpointProvider(sendEndpointProviderMock.Object);

            var author = new Author { FirstName = "Jan", LastName = "Kowalski" };

            DbContext.Add(author);
            await UnitOfWork.SaveChangesAsync();

            await UnitOfWork.RunInTransactionAsync(
                async ct => {
                    DbContext.Remove(author);
                    await UnitOfWork.SaveChangesAsync(ct);
                }
            );

            sendEndpointProviderMock.Verify(x => x.GetSendEndpoint(It.IsAny<Uri>()), Times.Exactly(2));
            sendEndpointProviderMock.SendEndpointMock.Verify(
                x => x.Send(It.IsAny<DumpHistoryOrder>(), It.IsAny<CancellationToken>()), Times.Exactly(2));
        }

        [Fact]
        public async Task Should_DumpMultipleHistoryChangesOnDelete()
        {
            var sendEndpointProviderMock = new SendEndpointProviderMock();
            ((UnitOfWork)UnitOfWork).ReplaceSendEndpointProvider(sendEndpointProviderMock.Object);

            var author = new Author { FirstName = "Jan", LastName = "Kowalski" };
            var book = new Book { Title = "Test", Author = author };

            DbContext.Add(author);
            DbContext.Add(book);
            await UnitOfWork.SaveChangesAsync();

            await UnitOfWork.RunInTransactionAsync(
                async ct => {
                    DbContext.Remove(author);
                    DbContext.Remove(book);
                    await UnitOfWork.SaveChangesAsync(ct);
                }
            );

            sendEndpointProviderMock.Verify(x => x.GetSendEndpoint(It.IsAny<Uri>()), Times.Exactly(2));
            sendEndpointProviderMock.SendEndpointMock.Verify(
                x => x.Send(It.IsAny<DumpHistoryOrder>(), It.IsAny<CancellationToken>()), Times.Exactly(4));
        }
        
        [Fact]
        public async Task ShouldNot_DumpHistoryChangesDirectlyAfterSaveChangesAsync()
        {
            var sendEndpointProviderMock = new SendEndpointProviderMock();
            ((UnitOfWork)UnitOfWork).ReplaceSendEndpointProvider(sendEndpointProviderMock.Object);

            var author = new Author { FirstName = "Jan", LastName = "Kowalski" };

            await UnitOfWork.RunInTransactionAsync(
                async ct => {
                    DbContext.Add(author);
                    await UnitOfWork.SaveChangesAsync(ct);
                    sendEndpointProviderMock.Verify(x => x.GetSendEndpoint(It.IsAny<Uri>()), Times.Never);
                    sendEndpointProviderMock.SendEndpointMock.Verify(
                        x => x.Send(It.IsAny<DumpHistoryOrder>(), It.IsAny<CancellationToken>()), Times.Never);
                }
            );

            sendEndpointProviderMock.Verify(x => x.GetSendEndpoint(It.IsAny<Uri>()), Times.Once);
            sendEndpointProviderMock.SendEndpointMock.Verify(
                x => x.Send(It.IsAny<DumpHistoryOrder>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        public RunInTransactionAsyncTest(IUnitOfWork unitOfWork, IPrimitiveValuesGenerator primitiveValuesGenerator,
            IUserAccessor userAccessor, DbContext dbContext, IClock clock) : base(unitOfWork, primitiveValuesGenerator,
            userAccessor, dbContext, clock)
        {
        }
    }
}
