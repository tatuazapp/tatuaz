using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using Tatuaz.Gateway.Handlers.Queries.Users;
using Tatuaz.Gateway.Requests.Queries.Users;
using Tatuaz.Shared.Domain.Entities.Fakers.Models.Identity;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Identity;
using Tatuaz.Shared.Domain.Entities.Models.Identity;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;
using Tatuaz.Testing.Mocks.Infrastructure;
using Xunit;

namespace Tatuaz.Gateway.Handlers.Test.Queries.Users;

public class WhoAmIQueryHandlerTest
{
    private readonly IMapper _mapper;
    private readonly TatuazUserFaker _tatuazUserFaker;
    private readonly UserContextMock _userContextMock;
    private readonly Mock<
        IGenericRepository<TatuazUser, HistTatuazUser, string>
    > _userRepositoryMock;

    public WhoAmIQueryHandlerTest(IMapper mapper)
    {
        _mapper = mapper;
        _userRepositoryMock = new Mock<IGenericRepository<TatuazUser, HistTatuazUser, string>>();
        _userContextMock = new UserContextMock();
        _tatuazUserFaker = new TatuazUserFaker();
    }

    public class Handle : WhoAmIQueryHandlerTest
    {
        public Handle(IMapper mapper) : base(mapper) { }

        [Fact]
        public async Task Should_ReturnUserWhenUserExists()
        {
            var user = _tatuazUserFaker.Generate();
            _userContextMock.ReturnUserId(user.Id);
            _userRepositoryMock
                .Setup(x => x.GetByIdAsync(user.Id, false, It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);

            var query = new WhoAmIQuery();
            var handler = new WhoAmIQueryHandler(
                _mapper,
                _userRepositoryMock.Object,
                _userContextMock.Object
            );
            var result = await handler.Handle(query, CancellationToken.None).ConfigureAwait(false);

            Assert.True(result.Successful);
            Assert.NotNull(result.Value);
            Assert.Equal(user.Email, result.Value.Email);
            Assert.Equal(user.Username, result.Value.Username);
        }

        [Fact]
        public async Task Should_ReturnErrorWhenUserDoesntExist()
        {
            var user = _tatuazUserFaker.Generate();
            _userContextMock.ReturnUserId(user.Id);
            _userRepositoryMock
                .Setup(x => x.GetByIdAsync(user.Id, false, It.IsAny<CancellationToken>()))
                .ReturnsAsync((TatuazUser?)null);

            var query = new WhoAmIQuery();
            var handler = new WhoAmIQueryHandler(
                _mapper,
                _userRepositoryMock.Object,
                _userContextMock.Object
            );
            var result = await handler.Handle(query, CancellationToken.None).ConfigureAwait(false);

            Assert.False(result.Successful);
            Assert.Null(result.Value);
            Assert.Equal("InternalError", result.Errors.First().Code);
        }

        [Fact]
        public async Task Should_ReturnErrorWhenUserContextReturnsNull()
        {
            _userContextMock.ReturnUserId(null);

            var query = new WhoAmIQuery();
            var handler = new WhoAmIQueryHandler(
                _mapper,
                _userRepositoryMock.Object,
                _userContextMock.Object
            );
            var result = await handler.Handle(query, CancellationToken.None).ConfigureAwait(false);

            Assert.False(result.Successful);
            Assert.Null(result.Value);
            Assert.Equal("InternalError", result.Errors.First().Code);
        }
    }
}
