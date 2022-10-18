using Microsoft.EntityFrameworkCore;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;
using Tatuaz.Shared.Infrastructure.Specification;
using Tatuaz.Shared.Infrastructure.Test.Database.Simple.HistModels;
using Tatuaz.Shared.Infrastructure.Test.Database.Simple.Models;

namespace Tatuaz.Shared.Infrastructure.Test.Specification;

public class FullSpecificationTest
{
    public FullSpecificationTest(DbContext dbContext, IUnitOfWork unitOfWork,
        IGenericRepository<Author, HistAuthor, Guid> authorRepository)
    {
        DbContext = dbContext;
        UnitOfWork = unitOfWork;
        AuthorRepository = authorRepository;
    }

    protected DbContext DbContext { get; }
    protected IUnitOfWork UnitOfWork { get; }
    protected IGenericRepository<Author, HistAuthor, Guid> AuthorRepository { get; }

    public class TrackingStrategyTest : FullSpecificationTest
    {
        public TrackingStrategyTest(DbContext dbContext, IUnitOfWork unitOfWork,
            IGenericRepository<Author, HistAuthor, Guid> authorRepository) : base(dbContext, unitOfWork,
            authorRepository)
        {
        }

        [Fact]
        public async Task Should_ReturnEntityWithTracking()
        {
            var expected = new Author { FirstName = "John", LastName = "Doe" };

            DbContext.Add(expected);
            await DbContext.SaveChangesAsync().ConfigureAwait(false);

            var spec = new FullSpecification<Author>();
            spec.TrackingStrategy = TrackingStrategy.Tracking;
            spec.AddFilter(x => x.Id == expected.Id);

            var actual = await AuthorRepository.GetBySpecificationAsync(spec).ConfigureAwait(false);
            Assert.Equal(expected.FirstName, actual.First().FirstName);
        }

        [Fact]
        public async Task Should_ReturnEntityWithNoTracking()
        {
            var expected = new Author { FirstName = "John", LastName = "Doe" };

            DbContext.Add(expected);
            await DbContext.SaveChangesAsync().ConfigureAwait(false);

            var spec = new FullSpecification<Author>();
            spec.TrackingStrategy = TrackingStrategy.NoTracking;
            spec.AddFilter(x => x.Id == expected.Id);

            var actual = await AuthorRepository.GetBySpecificationAsync(spec).ConfigureAwait(false);
            Assert.Equal(expected.FirstName, actual.First().FirstName);
        }

        [Fact]
        public async Task Should_UpdateEntityWithTracking()
        {
            var expected = new Author { FirstName = "John", LastName = "Doe" };

            DbContext.Add(expected);
            await DbContext.SaveChangesAsync().ConfigureAwait(false);

            var spec = new FullSpecification<Author> { TrackingStrategy = TrackingStrategy.Tracking };
            spec.AddFilter(x => x.Id == expected.Id);

            var actual = await AuthorRepository.GetBySpecificationAsync(spec).ConfigureAwait(false);
            actual.First().FirstName = "Johny";
            await UnitOfWork.SaveChangesAsync().ConfigureAwait(false);

            var actual2 = await AuthorRepository.GetBySpecificationAsync(spec).ConfigureAwait(false);
            Assert.Equal("Johny", actual2.First().FirstName);
        }

        [Fact]
        public async Task Should_NotUpdateEntityWithNoTracking()
        {
            var expected = new Author { FirstName = "John", LastName = "Doe" };

            DbContext.Add(expected);
            await DbContext.SaveChangesAsync().ConfigureAwait(false);

            var spec = new FullSpecification<Author> { TrackingStrategy = TrackingStrategy.NoTracking };
            spec.AddFilter(x => x.Id == expected.Id);

            var actual = await AuthorRepository.GetBySpecificationAsync(spec).ConfigureAwait(false);
            actual.First().FirstName = "Johny";
            await UnitOfWork.SaveChangesAsync().ConfigureAwait(false);

            var actual2 = await AuthorRepository.GetBySpecificationAsync(spec).ConfigureAwait(false);
            Assert.Equal("John", actual2.First().FirstName);
        }
    }

    public class AddFilterTest : FullSpecificationTest
    {
        public AddFilterTest(DbContext dbContext, IUnitOfWork unitOfWork,
            IGenericRepository<Author, HistAuthor, Guid> authorRepository) : base(dbContext, unitOfWork,
            authorRepository)
        {
        }

        [Fact]
        public async Task Should_ReturnEntityWithFilter()
        {
            var expected = new Author { FirstName = "John", LastName = "Doe" };

            DbContext.Add(expected);
            await DbContext.SaveChangesAsync().ConfigureAwait(false);

            var spec = new FullSpecification<Author>();
            spec.AddFilter(x => x.Id == expected.Id);

            var actual = await AuthorRepository.GetBySpecificationAsync(spec).ConfigureAwait(false);
            Assert.Equal(expected.FirstName, actual.First().FirstName);
        }
    }

    public class AddOrderTest : FullSpecificationTest
    {
        public AddOrderTest(DbContext dbContext, IUnitOfWork unitOfWork,
            IGenericRepository<Author, HistAuthor, Guid> authorRepository) : base(dbContext, unitOfWork,
            authorRepository)
        {
        }

        [Fact]
        public async Task Should_ReturnEntityWithAscendingOrder()
        {
            var author1 = new Author { FirstName = "John", LastName = "Doe" };

            var author2 = new Author { FirstName = "Anna", LastName = "Doe" };

            DbContext.AddRange(author1, author2);
            await DbContext.SaveChangesAsync().ConfigureAwait(false);

            var spec = new FullSpecification<Author>();
            spec.OrderDirection = OrderDirection.Ascending;
            spec.AddOrder(x => x.FirstName);
            spec.AddFilter(x => x.Id == author1.Id || x.Id == author2.Id);

            var actual = await AuthorRepository.GetBySpecificationAsync(spec).ConfigureAwait(false);

            // ReSharper disable PossibleMultipleEnumeration
            Assert.Equal(author2.FirstName, actual.First().FirstName);
            Assert.Equal(author1.FirstName, actual.Last().FirstName);
            // ReSharper restore PossibleMultipleEnumeration
        }

        [Fact]
        public async Task Should_ReturnEntityWithDescendingOrder()
        {
            var author1 = new Author { FirstName = "John", LastName = "Doe" };

            var author2 = new Author { FirstName = "Anna", LastName = "Doe" };

            DbContext.AddRange(author1, author2);
            await DbContext.SaveChangesAsync().ConfigureAwait(false);

            var spec = new FullSpecification<Author>();
            spec.OrderDirection = OrderDirection.Descending;
            spec.AddOrder(x => x.FirstName);
            spec.AddFilter(x => x.Id == author1.Id || x.Id == author2.Id);

            var actual = await AuthorRepository.GetBySpecificationAsync(spec).ConfigureAwait(false);

            // ReSharper disable PossibleMultipleEnumeration
            Assert.Equal(author1.FirstName, actual.First().FirstName);
            Assert.Equal(author2.FirstName, actual.Last().FirstName);
            // ReSharper restore PossibleMultipleEnumeration
        }
    }

    public class AddIncludeTest : FullSpecificationTest
    {
        public AddIncludeTest(DbContext dbContext, IUnitOfWork unitOfWork,
            IGenericRepository<Author, HistAuthor, Guid> authorRepository) : base(dbContext, unitOfWork,
            authorRepository)
        {
        }

        [Fact]
        public async Task Should_ReturnEntityWithInclude()
        {
            var author = new Author { FirstName = "John", LastName = "Doe" };
            var book = new Book { Title = "Book", Author = author };

            DbContext.AddRange(author, book);
            await DbContext.SaveChangesAsync().ConfigureAwait(false);

            var spec = new FullSpecification<Author>();
            spec.UseInclude(x => x.Include(y => y.Books));
            spec.AddFilter(x => x.Id == author.Id);

            var actual = await AuthorRepository.GetBySpecificationAsync(spec).ConfigureAwait(false);
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
            var author = new Author { FirstName = "John", LastName = "Doe" };
            var book = new Book { Title = "Book", Author = author };
            var award = new Award { Name = "Guinness", Book = book };

            DbContext.AddRange(author, book, award);
            await DbContext.SaveChangesAsync().ConfigureAwait(false);

            var spec = new FullSpecification<Author>();
            spec.UseInclude(x => x.Include(y => y.Books).ThenInclude(z => z.Awards));
            spec.AddFilter(x => x.Id == author.Id);

            // ReSharper disable PossibleMultipleEnumeration
            var actual = await AuthorRepository.GetBySpecificationAsync(spec).ConfigureAwait(false);
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
}
