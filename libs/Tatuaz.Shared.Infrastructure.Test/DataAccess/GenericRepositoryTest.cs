using Microsoft.EntityFrameworkCore;

using Moq;

using Tatuaz.Shared.Infrastructure.Abstractions;
using Tatuaz.Shared.Infrastructure.Abstractions.Paging;
using Tatuaz.Shared.Infrastructure.Abstractions.Specification;
using Tatuaz.Shared.Infrastructure.Test.Database.Simple.HistModels;
using Tatuaz.Shared.Infrastructure.Test.Database.Simple.Models;

namespace Tatuaz.Shared.Infrastructure.Test.DataAccess;

public class GenericRepositoryTest
{
    public GenericRepositoryTest(DbContext dbContext, IGenericRepository<Author, HistAuthor, Guid> authorRepository)
    {
        DbContext = dbContext;
        AuthorRepository = authorRepository;
    }

    protected DbContext DbContext { get; }
    protected IGenericRepository<Author, HistAuthor, Guid> AuthorRepository { get; }

    public class GetByIdAsyncTest : GenericRepositoryTest
    {
        public GetByIdAsyncTest(DbContext dbContext, IGenericRepository<Author, HistAuthor, Guid> authorRepository) :
            base(dbContext, authorRepository)
        {
        }

        [Fact]
        public async Task Should_ReturnSavedEntity()
        {
            var author = new Author {
                FirstName = "Jan",
                LastName = "Kowalski"
            };
            await DbContext.AddAsync(author);
            await DbContext.SaveChangesAsync();

            var result = await AuthorRepository.GetByIdAsync(author.Id);

            Assert.NotNull(result);
            Assert.Equal(author.FirstName, result?.FirstName);
            Assert.Equal(author.LastName, result?.LastName);
        }

        [Fact]
        public async Task Should_ReturnNullOnNotExistingEntity()
        {
            var result = await AuthorRepository.GetByIdAsync(Guid.NewGuid());

            Assert.Null(result);
        }

        [Fact]
        public async Task Should_UpdateTrackedEntity()
        {
            var author = new Author {
                FirstName = "Jan",
                LastName = "Kowalski"
            };
            await DbContext.AddAsync(author);
            await DbContext.SaveChangesAsync();

            var result = await AuthorRepository.GetByIdAsync(author.Id, true);

            Assert.NotNull(result);
            Assert.Equal(author.FirstName, result?.FirstName);
            Assert.Equal(author.LastName, result?.LastName);

            result!.FirstName = "Adam";
            result!.LastName = "Nowak";
            var changes = await DbContext.SaveChangesAsync();

            var updatedAuthor = await AuthorRepository.GetByIdAsync(author.Id);

            Assert.NotNull(updatedAuthor);
            Assert.Equal(result.FirstName, updatedAuthor?.FirstName);
            Assert.Equal(result.LastName, updatedAuthor?.LastName);
            Assert.Equal(1, changes);
        }

        [Fact]
        public async Task Should_NotUpdateNotTrackedEntity()
        {
            var author = new Author {
                FirstName = "Jan",
                LastName = "Kowalski"
            };
            await DbContext.AddAsync(author);
            await DbContext.SaveChangesAsync();

            var result = await AuthorRepository.GetByIdAsync(author.Id);

            Assert.NotNull(result);

            result!.FirstName = "Adam";
            result!.LastName = "Nowak";
            var changes = await DbContext.SaveChangesAsync();

            var updatedAuthor = await AuthorRepository.GetByIdAsync(author.Id);

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
            var author = new Author {
                FirstName = "Jan",
                LastName = "Kowalski"
            };
            await DbContext.AddAsync(author);
            await DbContext.SaveChangesAsync();

            var result = await AuthorRepository.ExistsByIdAsync(author.Id);

            Assert.True(result);
        }

        [Fact]
        public async Task Should_ReturnFalseOnNotExistingEntity()
        {
            var result = await AuthorRepository.ExistsByIdAsync(Guid.NewGuid());

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
            var author1 = new Author {
                FirstName = "Jan",
                LastName = "Kowalski"
            };
            var author2 = new Author {
                FirstName = "Adam",
                LastName = "Nowak"
            };
            await DbContext.AddRangeAsync(author1, author2);
            await DbContext.SaveChangesAsync();
            var specMock = new Mock<ISpecification<Author>>();
            specMock.Setup(x => x.Apply(It.IsAny<IQueryable<Author>>()))
                .Returns<IQueryable<Author>>(q
                    => q.Where(x => x.Id == author1.Id).AsQueryable());

            var result = await AuthorRepository.GetBySpecificationAsync(specMock.Object);

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
            var author1 = new Author {
                FirstName = "Jan",
                LastName = "Kowalski"
            };
            var author2 = new Author {
                FirstName = "Adam",
                LastName = "Nowak"
            };
            await DbContext.AddRangeAsync(author1, author2);
            await DbContext.SaveChangesAsync();
            var specMock = new Mock<ISpecification<Author>>();
            specMock.Setup(x => x.Apply(It.IsAny<IQueryable<Author>>()))
                .Returns<IQueryable<Author>>(q
                    => q.Where(x => x.Id == author1.Id).AsQueryable());

            var result =
                await AuthorRepository.GetBySpecificationWithPagingAsync(specMock.Object, new PagedParams(1, 1));

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
            var author = new Author {
                FirstName = "Jan",
                LastName = "Kowalski"
            };
            await DbContext.AddAsync(author);
            await DbContext.SaveChangesAsync();

            var result = await AuthorRepository.ExistsByPredicateAsync(x => x.Id == author.Id);

            Assert.True(result);
        }

        [Fact]
        public async Task Should_ReturnFalseOnNotExistingEntity()
        {
            var randomGuid = Guid.NewGuid();
            var result = await AuthorRepository.ExistsByPredicateAsync(x => x.Id == randomGuid);

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
            var author1 = new Author {
                FirstName = "Jan",
                LastName = "Kowalski"
            };
            var author2 = new Author {
                FirstName = "Adam",
                LastName = "Nowak"
            };
            await DbContext.AddRangeAsync(author1, author2);
            await DbContext.SaveChangesAsync();

            var result = await AuthorRepository.CountByPredicateAsync(x => x.Id == author1.Id);

            Assert.Equal(1, result);
        }

        [Fact]
        public async Task Should_ReturnZero()
        {
            var randomGuid = Guid.NewGuid();
            var result = await AuthorRepository.CountByPredicateAsync(x => x.Id == randomGuid);

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
            var author = new Author {
                FirstName = "Jan",
                LastName = "Kowalski"
            };

            await AuthorRepository.CreateAsync(author);

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
            var author = new Author {
                FirstName = "Jan",
                LastName = "Kowalski"
            };
            await DbContext.AddAsync(author);
            await DbContext.SaveChangesAsync();

            await AuthorRepository.DeleteAsync(author);

            Assert.Equal(EntityState.Deleted, DbContext.Entry(author).State);
        }

        [Fact]
        public async Task Should_DeleteEntityById()
        {
            var author = new Author {
                FirstName = "Jan",
                LastName = "Kowalski"
            };
            await DbContext.AddAsync(author);
            await DbContext.SaveChangesAsync();

            await AuthorRepository.DeleteAsync(author.Id);

            Assert.Equal(EntityState.Deleted, DbContext.Entry(author).State);
        }
    }
}
