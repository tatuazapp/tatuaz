using Microsoft.EntityFrameworkCore;

using NodaTime;
using NodaTime.Testing;

using Tatuaz.Shared.Infrastructure.Abstractions;
using Tatuaz.Shared.Infrastructure.Test.Database.Simple.Models;
using Tatuaz.Testing.Fakes.Common;
using Tatuaz.Testing.Fakes.Infrastructure;

namespace Tatuaz.Shared.Infrastructure.Test.DataAccess;

public class UnitOfWorkTest
{
    protected readonly TimeSpan TestPrecision = TimeSpan.FromMilliseconds(10);

    public UnitOfWorkTest(IUnitOfWork unitOfWork, IPrimitiveValuesGenerator primitiveValuesGenerator,
        IUserAccessor userAccessor, DbContext dbContext, IClock clock)
    {
        UnitOfWork = unitOfWork;
        PrimitiveValuesGenerator = primitiveValuesGenerator;
        UserAccessor = userAccessor;
        DbContext = dbContext;
        Clock = clock;
    }

    protected IUnitOfWork UnitOfWork { get; }
    protected IPrimitiveValuesGenerator PrimitiveValuesGenerator { get; }
    protected IUserAccessor UserAccessor { get; }
    protected DbContext DbContext { get; }
    protected IClock Clock { get; }

    public class SaveChangesAsyncTest : UnitOfWorkTest
    {
        public SaveChangesAsyncTest(IUnitOfWork unitOfWork, IPrimitiveValuesGenerator primitiveValuesGenerator,
            IUserAccessor userAccessor, DbContext dbContext, IClock clock) : base(unitOfWork, primitiveValuesGenerator,
            userAccessor,
            dbContext, clock)
        {
        }

        [Fact]
        public async Task Should_SaveInsertedElement()
        {
            var expected = new Author {
                FirstName = "Jan",
                LastName = "Kowalski"
            };

            DbContext.Add(expected);
            var changes = await UnitOfWork.SaveChangesAsync();
            var actual = await DbContext.Set<Author>().FirstAsync(x => x.Id == expected.Id);

            Assert.Equal(expected, actual);
            Assert.Equal(1, changes);
        }

        [Fact]
        public async Task Should_SaveItemChangesWithTrackingElement()
        {
            var author = new Author {
                FirstName = "Jan",
                LastName = "Kowalski"
            };

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
            var expected = new Author {
                FirstName = "Jan",
                LastName = "Kowalski"
            };

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
            var author = new Author {
                FirstName = "Jan",
                LastName = "Kowalski"
            };

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
            var author = new Author {
                FirstName = "Jan",
                LastName = "Kowalski"
            };

            DbContext.Add(author);
            var changes = await UnitOfWork.SaveChangesAsync();
            var actual = await DbContext.Set<Author>().FirstAsync(x => x.Id == author.Id);

            Assert.Equal(PrimitiveValuesGenerator.Guids(0), actual.CreatedBy);
            Assert.Equal(PrimitiveValuesGenerator.Guids(0), actual.ModifiedBy);
            Assert.Equal(Clock.GetCurrentInstant().ToDateTimeUtc(), actual.CreatedOn.ToDateTimeUtc(), TestPrecision);
            Assert.Equal(Clock.GetCurrentInstant().ToDateTimeUtc(), actual.ModifiedOn.ToDateTimeUtc(), TestPrecision);
            Assert.Equal(1, changes);
        }

        [Fact]
        public async Task Should_AddUserContextToModifiedElement()
        {
            var author = new Author {
                FirstName = "Jan",
                LastName = "Kowalski"
            };

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
                actual.CreatedOn.ToDateTimeUtc(), TestPrecision);
            Assert.Equal(Clock.GetCurrentInstant().ToDateTimeUtc(), actual.ModifiedOn.ToDateTimeUtc(), TestPrecision);
            Assert.Equal(1, changes1);
            Assert.Equal(1, changes2);
        }
    }

    public class RunInTransactionAsyncTest : UnitOfWorkTest
    {
        public RunInTransactionAsyncTest(IUnitOfWork unitOfWork, IPrimitiveValuesGenerator primitiveValuesGenerator,
            IUserAccessor userAccessor, DbContext dbContext, IClock clock) : base(unitOfWork, primitiveValuesGenerator,
            userAccessor,
            dbContext, clock)
        {
        }

        [Fact]
        public async Task Should_SaveInsertedElement()
        {
            var author = new Author {
                FirstName = "Jan",
                LastName = "Kowalski"
            };

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
            var author = new Author {
                FirstName = "Jan",
                LastName = "Kowalski"
            };

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
            var author = new Author {
                FirstName = "Jan",
                LastName = "Kowalski"
            };

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
            var author = new Author {
                FirstName = "Jan",
                LastName = "Kowalski"
            };

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
            var author = new Author {
                FirstName = "Jan",
                LastName = "Kowalski"
            };

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
            var author = new Author {
                FirstName = "Jan",
                LastName = "Kowalski"
            };

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
            var author = new Author {
                FirstName = "Jan",
                LastName = "Kowalski"
            };

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
            Assert.Equal(Clock.GetCurrentInstant().ToDateTimeUtc(), actual.CreatedOn.ToDateTimeUtc(), TestPrecision);
            Assert.Equal(Clock.GetCurrentInstant().ToDateTimeUtc(), actual.ModifiedOn.ToDateTimeUtc(), TestPrecision);
            Assert.Equal(1, changes);
        }

        [Fact]
        public async Task Should_AddUserContextToModifiedElement()
        {
            var author = new Author {
                FirstName = "Jan",
                LastName = "Kowalski"
            };

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
                actual.CreatedOn.ToDateTimeUtc(), TestPrecision);
            Assert.Equal(Clock.GetCurrentInstant().ToDateTimeUtc(), actual.ModifiedOn.ToDateTimeUtc(), TestPrecision);
            Assert.Equal(1, changes1);
            Assert.Equal(1, changes2);
        }
    }
}
