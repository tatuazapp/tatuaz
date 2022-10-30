using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;
using Tatuaz.Shared.Infrastructure.Specification;
using Tatuaz.Shared.Infrastructure.Test.Database.Simple;
using Tatuaz.Shared.Infrastructure.Test.Database.Simple.Fakers;
using Tatuaz.Shared.Infrastructure.Test.Database.Simple.HistModels;
using Tatuaz.Shared.Infrastructure.Test.Database.Simple.Models;
using Xunit;

namespace Tatuaz.Shared.Infrastructure.Test.Specification;

public class FullSpecificationTest
{
    private readonly IGenericRepository<BooksDbContext, Author, HistAuthor, Guid> _authorRepository;

    private readonly BooksDbContext _dbContext;
    private readonly IUnitOfWork<BooksDbContext> _unitOfWork;

    public FullSpecificationTest(
        BooksDbContext dbContext,
        IUnitOfWork<BooksDbContext> unitOfWork,
        IGenericRepository<BooksDbContext, Author, HistAuthor, Guid> authorRepository
    )
    {
        _dbContext = dbContext;
        _unitOfWork = unitOfWork;
        _authorRepository = authorRepository;
    }

    public class TrackingStrategyTest : FullSpecificationTest
    {
        public TrackingStrategyTest(
            BooksDbContext dbContext,
            IUnitOfWork<BooksDbContext> unitOfWork,
            IGenericRepository<BooksDbContext, Author, HistAuthor, Guid> authorRepository
        ) : base(dbContext, unitOfWork, authorRepository)
        {
        }

        [Fact]
        public async Task Should_ReturnEntityWithTracking()
        {
            var expected = AuthorFaker.Generate();

            _dbContext.Add(expected);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            var spec = new FullSpecification<Author> { TrackingStrategy = TrackingStrategy.Tracking };
            spec.AddFilter(x => x.Id == expected.Id);

            var actual = await _authorRepository.GetBySpecificationAsync(spec).ConfigureAwait(false);
            Assert.Equal(expected.FirstName, actual.First().FirstName);
        }

        [Fact]
        public async Task Should_ReturnEntityWithNoTracking()
        {
            var expected = AuthorFaker.Generate();

            _dbContext.Add(expected);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            var spec = new FullSpecification<Author> { TrackingStrategy = TrackingStrategy.NoTracking };
            spec.AddFilter(x => x.Id == expected.Id);

            var actual = await _authorRepository.GetBySpecificationAsync(spec).ConfigureAwait(false);
            Assert.Equal(expected.FirstName, actual.First().FirstName);
        }

        [Fact]
        public async Task Should_UpdateEntityWithTracking()
        {
            var expected = AuthorFaker.Generate();

            _dbContext.Add(expected);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            var spec = new FullSpecification<Author> { TrackingStrategy = TrackingStrategy.Tracking };
            spec.AddFilter(x => x.Id == expected.Id);

            var actual = await _authorRepository.GetBySpecificationAsync(spec).ConfigureAwait(false);
            actual.First().FirstName = "Johny";
            await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);

            var actual2 = await _authorRepository
                .GetBySpecificationAsync(spec)
                .ConfigureAwait(false);
            Assert.Equal("Johny", actual2.First().FirstName);
        }

        [Fact]
        public async Task Should_NotUpdateEntityWithNoTracking()
        {
            var expected = AuthorFaker.Generate();

            _dbContext.Add(expected);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            var spec = new FullSpecification<Author> { TrackingStrategy = TrackingStrategy.NoTracking };
            spec.AddFilter(x => x.Id == expected.Id);

            var actual = await _authorRepository.GetBySpecificationAsync(spec).ConfigureAwait(false);
            actual.First().FirstName = "Johny";
            await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);

            var actual2 = await _authorRepository
                .GetBySpecificationAsync(spec)
                .ConfigureAwait(false);
            Assert.Equal(expected.FirstName, actual2.First().FirstName);
        }
    }

    public class AddFilterTest : FullSpecificationTest
    {
        public AddFilterTest(
            BooksDbContext dbContext,
            IUnitOfWork<BooksDbContext> unitOfWork,
            IGenericRepository<BooksDbContext, Author, HistAuthor, Guid> authorRepository
        ) : base(dbContext, unitOfWork, authorRepository)
        {
        }

        [Fact]
        public async Task Should_ReturnEntityWithFilter()
        {
            var expected = AuthorFaker.Generate();

            _dbContext.Add(expected);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            var spec = new FullSpecification<Author>();
            spec.AddFilter(x => x.Id == expected.Id);

            var actual = await _authorRepository.GetBySpecificationAsync(spec).ConfigureAwait(false);
            Assert.Equal(expected.FirstName, actual.First().FirstName);
        }
    }

    public class AddOrderTest : FullSpecificationTest
    {
        public AddOrderTest(
            BooksDbContext dbContext,
            IUnitOfWork<BooksDbContext> unitOfWork,
            IGenericRepository<BooksDbContext, Author, HistAuthor, Guid> authorRepository
        ) : base(dbContext, unitOfWork, authorRepository)
        {
        }

        [Fact]
        public async Task Should_ReturnEntityWithAscendingOrder()
        {
            var authors = AuthorFaker.Generate(2).OrderByDescending(x => x.FirstName).ToList();

            _dbContext.AddRange(authors);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            var spec = new FullSpecification<Author> { OrderDirection = OrderDirection.Ascending };
            spec.AddOrder(x => x.FirstName);
            spec.AddFilter(x => authors.Contains(x));

            var actual = await _authorRepository.GetBySpecificationAsync(spec).ConfigureAwait(false);

            // ReSharper disable PossibleMultipleEnumeration
            Assert.Equal(authors.Last().FirstName, actual.First().FirstName);
            Assert.Equal(authors.First().FirstName, actual.Last().FirstName);
            // ReSharper restore PossibleMultipleEnumeration
        }

        [Fact]
        public async Task Should_ReturnEntityWithDescendingOrder()
        {
            var authors = AuthorFaker.Generate(2).OrderByDescending(x => x.FirstName).ToList();

            _dbContext.AddRange(authors);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            var spec = new FullSpecification<Author> { OrderDirection = OrderDirection.Descending };
            spec.AddOrder(x => x.FirstName);
            spec.AddFilter(x => authors.Contains(x));

            var actual = await _authorRepository.GetBySpecificationAsync(spec).ConfigureAwait(false);

            // ReSharper disable PossibleMultipleEnumeration
            Assert.Equal(authors.First().FirstName, actual.First().FirstName);
            Assert.Equal(authors.Last().FirstName, actual.Last().FirstName);
            // ReSharper restore PossibleMultipleEnumeration
        }
    }

    public class AddIncludeTest : FullSpecificationTest
    {
        public AddIncludeTest(
            BooksDbContext dbContext,
            IUnitOfWork<BooksDbContext> unitOfWork,
            IGenericRepository<BooksDbContext, Author, HistAuthor, Guid> authorRepository
        ) : base(dbContext, unitOfWork, authorRepository)
        {
        }

        [Fact]
        public async Task Should_ReturnEntityWithInclude()
        {
            var author = AuthorFaker.Generate();
            var book = BookFaker.FromAuthorId(author.Id);

            _dbContext.AddRange(author, book);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            var spec = new FullSpecification<Author>();
            spec.UseInclude(x => x.Include(y => y.Books));
            spec.AddFilter(x => x.Id == author.Id);

            var actual = await _authorRepository.GetBySpecificationAsync(spec).ConfigureAwait(false);
            // ReSharper disable PossibleMultipleEnumeration
            Assert.Equal(author.FirstName, actual.First().FirstName);
            Assert.NotNull(actual.First().Books);
            Assert.NotEmpty(actual.First().Books);
            Assert.Equal(book.Title, actual.First().Books.First().Title);
            // ReSharper restore PossibleMultipleEnumeration
        }

        [Fact]
        public async Task Should_ReturnEntityWithIncludeAndThenInclude()
        {
            var author = AuthorFaker.Generate();
            var book = BookFaker.FromAuthorId(author.Id);
            var award = AwardFaker.FromBookId(book.Id);

            _dbContext.AddRange(author, book, award);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            var spec = new FullSpecification<Author>();
            spec.UseInclude(x => x.Include(y => y.Books).ThenInclude(z => z.Awards));
            spec.AddFilter(x => x.Id == author.Id);

            // ReSharper disable PossibleMultipleEnumeration
            var actual = await _authorRepository.GetBySpecificationAsync(spec).ConfigureAwait(false);
            Assert.Equal(author.FirstName, actual.First().FirstName);
            Assert.NotNull(actual.First().Books);
            Assert.NotEmpty(actual.First().Books);
            Assert.Equal(book.Title, actual.First().Books.First().Title);
            Assert.NotNull(actual.First().Books.First().Awards);
            Assert.NotEmpty(actual.First().Books.First().Awards);
            Assert.Equal(award.Name, actual.First().Books.First().Awards.First().Name);
            // ReSharper restore PossibleMultipleEnumeration
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
