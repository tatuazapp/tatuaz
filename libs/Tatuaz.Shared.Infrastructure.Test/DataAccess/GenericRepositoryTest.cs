using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;
using Tatuaz.Shared.Infrastructure.Abstractions.Paging;
using Tatuaz.Shared.Infrastructure.Abstractions.Specification;
using Tatuaz.Shared.Infrastructure.Test.Database.Simple.Dtos;
using Tatuaz.Shared.Infrastructure.Test.Database.Simple.Fakers;
using Tatuaz.Shared.Infrastructure.Test.Database.Simple.HistModels;
using Tatuaz.Shared.Infrastructure.Test.Database.Simple.Models;
using Xunit;

namespace Tatuaz.Shared.Infrastructure.Test.DataAccess;

public class GenericRepositoryTest
{
    private readonly IGenericRepository<Author, HistAuthor, Guid> _authorRepository;

    private readonly DbContext _dbContext;

    public GenericRepositoryTest(
        DbContext dbContext,
        IGenericRepository<Author, HistAuthor, Guid> authorRepository
    )
    {
        _dbContext = dbContext;
        _authorRepository = authorRepository;
    }

    public class GetByIdAsyncTest : GenericRepositoryTest
    {
        public GetByIdAsyncTest(
            DbContext dbContext,
            IGenericRepository<Author, HistAuthor, Guid> authorRepository
        )
            : base(dbContext, authorRepository) { }

        [Fact]
        public async Task Should_ReturnSavedEntity()
        {
            var author = AuthorFaker.Generate();
            await _dbContext.AddAsync(author).ConfigureAwait(false);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            var result = await _authorRepository.GetByIdAsync(author.Id).ConfigureAwait(false);

            Assert.NotNull(result);
            Assert.Equal(author.FirstName, result.FirstName);
            Assert.Equal(author.LastName, result.LastName);
        }

        [Fact]
        public async Task Should_ReturnNullOnNotExistingEntity()
        {
            var result = await _authorRepository.GetByIdAsync(Guid.NewGuid()).ConfigureAwait(false);

            Assert.Null(result);
        }

        [Fact]
        public async Task Should_UpdateTrackedEntity()
        {
            var author = AuthorFaker.Generate();
            await _dbContext.AddAsync(author).ConfigureAwait(false);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            var result = await _authorRepository
                .GetByIdAsync(author.Id, true)
                .ConfigureAwait(false);

            Assert.NotNull(result);
            Assert.Equal(author.FirstName, result.FirstName);
            Assert.Equal(author.LastName, result.LastName);

#pragma warning disable CS8602
            result.FirstName = "Adam";
            result.LastName = "Nowak";
#pragma warning restore CS8602
            var changes = await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            var updatedAuthor = await _authorRepository
                .GetByIdAsync(author.Id)
                .ConfigureAwait(false);

            Assert.NotNull(updatedAuthor);
            Assert.Equal(result.FirstName, updatedAuthor.FirstName);
            Assert.Equal(result.LastName, updatedAuthor.LastName);
            Assert.Equal(1, changes);
        }

        [Fact]
        public async Task Should_NotUpdateNotTrackedEntity()
        {
            var author = AuthorFaker.Generate();
            await _dbContext.AddAsync(author).ConfigureAwait(false);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            var result = await _authorRepository.GetByIdAsync(author.Id).ConfigureAwait(false);

            Assert.NotNull(result);

            result.FirstName = "Adam";
            result.LastName = "Nowak";
            var changes = await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            var updatedAuthor = await _authorRepository
                .GetByIdAsync(author.Id)
                .ConfigureAwait(false);

            Assert.NotNull(updatedAuthor);
            Assert.Equal(author.FirstName, updatedAuthor.FirstName);
            Assert.Equal(author.LastName, updatedAuthor.LastName);
            Assert.Equal(0, changes);
        }

        [Fact]
        public async Task Should_ReturnSavedEntityWithMapping()
        {
            var author = AuthorFaker.Generate();
            await _dbContext.AddAsync(author).ConfigureAwait(false);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            var result = await _authorRepository
                .GetByIdAsync<AuthorDto>(author.Id)
                .ConfigureAwait(false);

            Assert.NotNull(result);
            Assert.Equal(author.FirstName, result.FirstName);
            Assert.Equal(author.LastName, result.LastName);
        }
    }

    public class ExistsByIdAsyncTest : GenericRepositoryTest
    {
        public ExistsByIdAsyncTest(
            DbContext dbContext,
            IGenericRepository<Author, HistAuthor, Guid> authorRepository
        )
            : base(dbContext, authorRepository) { }

        [Fact]
        public async Task Should_ReturnTrueOnExistingEntity()
        {
            var author = AuthorFaker.Generate();
            await _dbContext.AddAsync(author).ConfigureAwait(false);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            var result = await _authorRepository.ExistsByIdAsync(author.Id).ConfigureAwait(false);

            Assert.True(result);
        }

        [Fact]
        public async Task Should_ReturnFalseOnNotExistingEntity()
        {
            var result = await _authorRepository
                .ExistsByIdAsync(Guid.NewGuid())
                .ConfigureAwait(false);

            Assert.False(result);
        }
    }

    public class GetBySpecificationAsyncTest : GenericRepositoryTest
    {
        public GetBySpecificationAsyncTest(
            DbContext dbContext,
            IGenericRepository<Author, HistAuthor, Guid> authorRepository
        )
            : base(dbContext, authorRepository) { }

        [Fact]
        public async Task Should_ReturnEntity()
        {
            var author1 = AuthorFaker.Generate();
            var author2 = AuthorFaker.Generate();
            await _dbContext.AddRangeAsync(author1, author2).ConfigureAwait(false);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            var specMock = new Mock<ISpecification<Author>>();
            specMock
                .Setup(x => x.Apply(It.IsAny<IQueryable<Author>>()))
                .Returns<IQueryable<Author>>(q => q.Where(x => x.Id == author1.Id));

            var result = (
                await _authorRepository
                    .GetBySpecificationAsync(specMock.Object)
                    .ConfigureAwait(false)
            ).ToList();

            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(author1.FirstName, result.First().FirstName);
        }

        [Fact]
        public async Task Should_ReturnEntityWithMapping()
        {
            var author1 = AuthorFaker.Generate();
            var author2 = AuthorFaker.Generate();
            await _dbContext.AddRangeAsync(author1, author2).ConfigureAwait(false);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            var specMock = new Mock<ISpecification<Author>>();
            specMock
                .Setup(x => x.Apply(It.IsAny<IQueryable<Author>>()))
                .Returns<IQueryable<Author>>(q => q.Where(x => x.Id == author1.Id));

            var result = (
                await _authorRepository
                    .GetBySpecificationAsync<AuthorDto>(specMock.Object)
                    .ConfigureAwait(false)
            ).ToList();

            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(author1.FirstName, result.First().FirstName);
        }
    }

    public class GetBySpecificationWithPagingAsyncTest : GenericRepositoryTest
    {
        public GetBySpecificationWithPagingAsyncTest(
            DbContext dbContext,
            IGenericRepository<Author, HistAuthor, Guid> authorRepository
        )
            : base(dbContext, authorRepository) { }

        [Fact]
        public async Task Should_ReturnEntityWithPaging()
        {
            var author1 = AuthorFaker.Generate();
            var author2 = AuthorFaker.Generate();
            await _dbContext.AddRangeAsync(author1, author2).ConfigureAwait(false);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            var specMock = new Mock<ISpecification<Author>>();
            specMock
                .Setup(x => x.Apply(It.IsAny<IQueryable<Author>>()))
                .Returns<IQueryable<Author>>(q => q.Where(x => x.Id == author1.Id));

            var result = await _authorRepository
                .GetBySpecificationWithPagingAsync(specMock.Object, new PagedParams(1, 1))
                .ConfigureAwait(false);

            Assert.NotNull(result);
            Assert.Single(result.Data);
            Assert.Equal(author1.FirstName, result.Data.First().FirstName);
            Assert.Equal(1, result.PageNumber);
            Assert.Equal(1, result.PageSize);
            Assert.Equal(1, result.TotalPages);
            Assert.Equal(1, result.TotalCount);
        }

        [Fact]
        public async Task Should_ReturnEntityWithPagingAndMapping()
        {
            var author1 = AuthorFaker.Generate();
            var author2 = AuthorFaker.Generate();
            await _dbContext.AddRangeAsync(author1, author2).ConfigureAwait(false);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            var specMock = new Mock<ISpecification<Author>>();
            specMock
                .Setup(x => x.Apply(It.IsAny<IQueryable<Author>>()))
                .Returns<IQueryable<Author>>(q => q.Where(x => x.Id == author1.Id));

            var result = await _authorRepository
                .GetBySpecificationWithPagingAsync<AuthorDto>(
                    specMock.Object,
                    new PagedParams(1, 1)
                )
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
        public ExistsByPredicateAsyncTest(
            DbContext dbContext,
            IGenericRepository<Author, HistAuthor, Guid> authorRepository
        )
            : base(dbContext, authorRepository) { }

        [Fact]
        public async Task Should_ReturnTrueOnExistingEntity()
        {
            var author = AuthorFaker.Generate();
            await _dbContext.AddAsync(author).ConfigureAwait(false);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            var result = await _authorRepository
                .ExistsByPredicateAsync(x => x.Id == author.Id)
                .ConfigureAwait(false);

            Assert.True(result);
        }

        [Fact]
        public async Task Should_ReturnFalseOnNotExistingEntity()
        {
            var randomGuid = Guid.NewGuid();
            var result = await _authorRepository
                .ExistsByPredicateAsync(x => x.Id == randomGuid)
                .ConfigureAwait(false);

            Assert.False(result);
        }
    }

    public class CountByPredicateAsyncTest : GenericRepositoryTest
    {
        public CountByPredicateAsyncTest(
            DbContext dbContext,
            IGenericRepository<Author, HistAuthor, Guid> authorRepository
        )
            : base(dbContext, authorRepository) { }

        [Fact]
        public async Task Should_ReturnCount()
        {
            var author1 = AuthorFaker.Generate();
            var author2 = AuthorFaker.Generate();
            await _dbContext.AddRangeAsync(author1, author2).ConfigureAwait(false);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            var result = await _authorRepository
                .CountByPredicateAsync(x => x.Id == author1.Id)
                .ConfigureAwait(false);

            Assert.Equal(1, result);
        }

        [Fact]
        public async Task Should_ReturnZero()
        {
            var randomGuid = Guid.NewGuid();
            var result = await _authorRepository
                .CountByPredicateAsync(x => x.Id == randomGuid)
                .ConfigureAwait(false);

            Assert.Equal(0, result);
        }
    }

    public class CreateAsyncTest : GenericRepositoryTest
    {
        public CreateAsyncTest(
            DbContext dbContext,
            IGenericRepository<Author, HistAuthor, Guid> authorRepository
        )
            : base(dbContext, authorRepository) { }

        [Fact]
        public void Should_CreateEntity()
        {
            var author = AuthorFaker.Generate();

            _authorRepository.Create(author);

            Assert.NotEqual(Guid.Empty, author.Id);
            Assert.Equal(EntityState.Added, _dbContext.Entry(author).State);
        }
    }

    public class DeleteAsyncTest : GenericRepositoryTest
    {
        public DeleteAsyncTest(
            DbContext dbContext,
            IGenericRepository<Author, HistAuthor, Guid> authorRepository
        )
            : base(dbContext, authorRepository) { }

        [Fact]
        public async Task Should_DeleteEntity()
        {
            var author = AuthorFaker.Generate();
            await _dbContext.AddAsync(author).ConfigureAwait(false);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            _authorRepository.Delete(author);

            Assert.Equal(EntityState.Deleted, _dbContext.Entry(author).State);
        }

        [Fact]
        public async Task Should_DeleteEntityById()
        {
            var author = AuthorFaker.Generate();
            await _dbContext.AddAsync(author).ConfigureAwait(false);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            await _authorRepository.DeleteAsync(author.Id).ConfigureAwait(false);

            Assert.Equal(EntityState.Deleted, _dbContext.Entry(author).State);
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
