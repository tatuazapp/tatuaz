using Microsoft.EntityFrameworkCore;
using Moq;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;
using Tatuaz.Shared.Infrastructure.Abstractions.Paging;
using Tatuaz.Shared.Infrastructure.Abstractions.Specification;
using Tatuaz.Shared.Infrastructure.Test.Database.Simple.HistModels;
using Tatuaz.Shared.Infrastructure.Test.Database.Simple.Models;

namespace Tatuaz.Shared.Infrastructure.Test.DataAccess;

public class GenericRepositoryTest
{
    private GenericRepositoryTest(DbContext dbContext, IGenericRepository<Author, HistAuthor, Guid> authorRepository)
    {
        DbContext = dbContext;
        AuthorRepository = authorRepository;
    }

    private DbContext DbContext { get; }
    private IGenericRepository<Author, HistAuthor, Guid> AuthorRepository { get; }

    public class GetByIdAsyncTest : GenericRepositoryTest
    {
        public GetByIdAsyncTest(DbContext dbContext, IGenericRepository<Author, HistAuthor, Guid> authorRepository) :
            base(dbContext, authorRepository)
        {
        }

        [Fact]
        public async Task Should_ReturnSavedEntity()
        {
            var author = new Author { FirstName = "Jan", LastName = "Kowalski" };
            await DbContext.AddAsync(author).ConfigureAwait(false);
            await DbContext.SaveChangesAsync().ConfigureAwait(false);

            var result = await AuthorRepository.GetByIdAsync(author.Id).ConfigureAwait(false);

            Assert.NotNull(result);
            Assert.Equal(author.FirstName, result?.FirstName);
            Assert.Equal(author.LastName, result?.LastName);
        }

        [Fact]
        public async Task Should_ReturnNullOnNotExistingEntity()
        {
            var result = await AuthorRepository.GetByIdAsync(Guid.NewGuid()).ConfigureAwait(false);

            Assert.Null(result);
        }

        [Fact]
        public async Task Should_UpdateTrackedEntity()
        {
            var author = new Author { FirstName = "Jan", LastName = "Kowalski" };
            await DbContext.AddAsync(author).ConfigureAwait(false);
            await DbContext.SaveChangesAsync().ConfigureAwait(false);

            var result = await AuthorRepository.GetByIdAsync(author.Id, true).ConfigureAwait(false);

            Assert.NotNull(result);
            Assert.Equal(author.FirstName, result?.FirstName);
            Assert.Equal(author.LastName, result?.LastName);

            result!.FirstName = "Adam";
            result!.LastName = "Nowak";
            var changes = await DbContext.SaveChangesAsync().ConfigureAwait(false);

            var updatedAuthor = await AuthorRepository.GetByIdAsync(author.Id).ConfigureAwait(false);

            Assert.NotNull(updatedAuthor);
            Assert.Equal(result.FirstName, updatedAuthor?.FirstName);
            Assert.Equal(result.LastName, updatedAuthor?.LastName);
            Assert.Equal(1, changes);
        }

        [Fact]
        public async Task Should_NotUpdateNotTrackedEntity()
        {
            var author = new Author { FirstName = "Jan", LastName = "Kowalski" };
            await DbContext.AddAsync(author).ConfigureAwait(false);
            await DbContext.SaveChangesAsync().ConfigureAwait(false);

            var result = await AuthorRepository.GetByIdAsync(author.Id).ConfigureAwait(false);

            Assert.NotNull(result);

            result!.FirstName = "Adam";
            result!.LastName = "Nowak";
            var changes = await DbContext.SaveChangesAsync().ConfigureAwait(false);

            var updatedAuthor = await AuthorRepository.GetByIdAsync(author.Id).ConfigureAwait(false);

            Assert.NotNull(updatedAuthor);
            Assert.Equal(author.FirstName, updatedAuthor?.FirstName);
            Assert.Equal(author.LastName, updatedAuthor?.LastName);
            Assert.Equal(0, changes);
        }
    }

    public class ExistsByIdAsyncTest : GenericRepositoryTest
    {
        public ExistsByIdAsyncTest(DbContext dbContext, IGenericRepository<Author, HistAuthor, Guid> authorRepository) :
            base(dbContext, authorRepository)
        {
        }

        [Fact]
        public async Task Should_ReturnTrueOnExistingEntity()
        {
            var author = new Author { FirstName = "Jan", LastName = "Kowalski" };
            await DbContext.AddAsync(author).ConfigureAwait(false);
            await DbContext.SaveChangesAsync().ConfigureAwait(false);

            var result = await AuthorRepository.ExistsByIdAsync(author.Id).ConfigureAwait(false);

            Assert.True(result);
        }

        [Fact]
        public async Task Should_ReturnFalseOnNotExistingEntity()
        {
            var result = await AuthorRepository.ExistsByIdAsync(Guid.NewGuid()).ConfigureAwait(false);

            Assert.False(result);
        }
    }

    public class GetBySpecificationAsyncTest : GenericRepositoryTest
    {
        public GetBySpecificationAsyncTest(DbContext dbContext,
            IGenericRepository<Author, HistAuthor, Guid> authorRepository) : base(dbContext, authorRepository)
        {
        }

        [Fact]
        public async Task Should_ReturnEntity()
        {
            var author1 = new Author { FirstName = "Jan", LastName = "Kowalski" };
            var author2 = new Author { FirstName = "Adam", LastName = "Nowak" };
            await DbContext.AddRangeAsync(author1, author2).ConfigureAwait(false);
            await DbContext.SaveChangesAsync().ConfigureAwait(false);
            var specMock = new Mock<ISpecification<Author>>();
            specMock.Setup(x => x.Apply(It.IsAny<IQueryable<Author>>()))
                .Returns<IQueryable<Author>>(q
                    => q.Where(x => x.Id == author1.Id).AsQueryable());

            var result = await AuthorRepository.GetBySpecificationAsync(specMock.Object).ConfigureAwait(false);

            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(author1.FirstName, result.First().FirstName);
        }
    }

    public class GetBySpecificationWithPagingAsyncTest : GenericRepositoryTest
    {
        public GetBySpecificationWithPagingAsyncTest(DbContext dbContext,
            IGenericRepository<Author, HistAuthor, Guid> authorRepository) : base(dbContext, authorRepository)
        {
        }

        [Fact]
        public async Task Should_ReturnEntityWithPaging()
        {
            var author1 = new Author { FirstName = "Jan", LastName = "Kowalski" };
            var author2 = new Author { FirstName = "Adam", LastName = "Nowak" };
            await DbContext.AddRangeAsync(author1, author2).ConfigureAwait(false);
            await DbContext.SaveChangesAsync().ConfigureAwait(false);
            var specMock = new Mock<ISpecification<Author>>();
            specMock.Setup(x => x.Apply(It.IsAny<IQueryable<Author>>()))
                .Returns<IQueryable<Author>>(q
                    => q.Where(x => x.Id == author1.Id).AsQueryable());

            var result =
                await AuthorRepository.GetBySpecificationWithPagingAsync(specMock.Object, new PagedParams(1, 1))
                    .ConfigureAwait(false);

            Assert.NotNull(result);
            Assert.Single(result.Data);
            Assert.Equal(author1.FirstName, result.Data.First().FirstName);
            Assert.Equal(1, result.PageNumber);
            Assert.Equal(1, result.PageSize);
            Assert.Equal(1, result.TotalPages);
            Assert.Equal(1, result.TotalCount);
        }
    }

    public class ExistsByPredicateAsyncTest : GenericRepositoryTest
    {
        public ExistsByPredicateAsyncTest(DbContext dbContext,
            IGenericRepository<Author, HistAuthor, Guid> authorRepository) : base(dbContext, authorRepository)
        {
        }

        [Fact]
        public async Task Should_ReturnTrueOnExistingEntity()
        {
            var author = new Author { FirstName = "Jan", LastName = "Kowalski" };
            await DbContext.AddAsync(author).ConfigureAwait(false);
            await DbContext.SaveChangesAsync().ConfigureAwait(false);

            var result = await AuthorRepository.ExistsByPredicateAsync(x => x.Id == author.Id).ConfigureAwait(false);

            Assert.True(result);
        }

        [Fact]
        public async Task Should_ReturnFalseOnNotExistingEntity()
        {
            var randomGuid = Guid.NewGuid();
            var result = await AuthorRepository.ExistsByPredicateAsync(x => x.Id == randomGuid).ConfigureAwait(false);

            Assert.False(result);
        }
    }

    public class CountByPredicateAsyncTest : GenericRepositoryTest
    {
        public CountByPredicateAsyncTest(DbContext dbContext,
            IGenericRepository<Author, HistAuthor, Guid> authorRepository) : base(dbContext, authorRepository)
        {
        }

        [Fact]
        public async Task Should_ReturnCount()
        {
            var author1 = new Author { FirstName = "Jan", LastName = "Kowalski" };
            var author2 = new Author { FirstName = "Adam", LastName = "Nowak" };
            await DbContext.AddRangeAsync(author1, author2).ConfigureAwait(false);
            await DbContext.SaveChangesAsync().ConfigureAwait(false);

            var result = await AuthorRepository.CountByPredicateAsync(x => x.Id == author1.Id).ConfigureAwait(false);

            Assert.Equal(1, result);
        }

        [Fact]
        public async Task Should_ReturnZero()
        {
            var randomGuid = Guid.NewGuid();
            var result = await AuthorRepository.CountByPredicateAsync(x => x.Id == randomGuid).ConfigureAwait(false);

            Assert.Equal(0, result);
        }
    }

    public class CreateAsyncTest : GenericRepositoryTest
    {
        public CreateAsyncTest(DbContext dbContext, IGenericRepository<Author, HistAuthor, Guid> authorRepository) :
            base(dbContext, authorRepository)
        {
        }

        [Fact]
        public async Task Should_CreateEntity()
        {
            var author = new Author { FirstName = "Jan", LastName = "Kowalski" };

            await AuthorRepository.CreateAsync(author).ConfigureAwait(false);

            Assert.NotEqual(Guid.Empty, author.Id);
            Assert.Equal(EntityState.Added, DbContext.Entry(author).State);
        }
    }

    public class DeleteAsyncTest : GenericRepositoryTest
    {
        public DeleteAsyncTest(DbContext dbContext, IGenericRepository<Author, HistAuthor, Guid> authorRepository) :
            base(dbContext, authorRepository)
        {
        }

        [Fact]
        public async Task Should_DeleteEntity()
        {
            var author = new Author { FirstName = "Jan", LastName = "Kowalski" };
            await DbContext.AddAsync(author).ConfigureAwait(false);
            await DbContext.SaveChangesAsync().ConfigureAwait(false);

            await AuthorRepository.DeleteAsync(author).ConfigureAwait(false);

            Assert.Equal(EntityState.Deleted, DbContext.Entry(author).State);
        }

        [Fact]
        public async Task Should_DeleteEntityById()
        {
            var author = new Author { FirstName = "Jan", LastName = "Kowalski" };
            await DbContext.AddAsync(author).ConfigureAwait(false);
            await DbContext.SaveChangesAsync().ConfigureAwait(false);

            await AuthorRepository.DeleteAsync(author.Id).ConfigureAwait(false);

            Assert.Equal(EntityState.Deleted, DbContext.Entry(author).State);
        }
    }
}
