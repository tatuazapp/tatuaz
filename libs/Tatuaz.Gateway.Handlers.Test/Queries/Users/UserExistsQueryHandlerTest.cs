using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Tatuaz.Gateway.Handlers.Queries.Users;
using Tatuaz.Gateway.Requests.Queries.Users;
using Tatuaz.Shared.Domain.Entities.Fakers.Models.Identity;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Identity;
using Tatuaz.Shared.Domain.Entities.Models.Identity;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;
using Tatuaz.Shared.Infrastructure.DataAccess;
using Xunit;

namespace Tatuaz.Gateway.Handlers.Test.Queries.Users;

public class UserExistsQueryHandlerTest
{
    private readonly DbContext _dbContext;
    private readonly Mock<IServiceScopeFactory> _serviceScopeFactoryMock;
    private readonly TatuazUserFaker _tatuazUserFaker;

    public UserExistsQueryHandlerTest(DbContext dbContext)
    {
        _dbContext = dbContext;
        var serviceScopeMock = new Mock<IServiceScope>();
        _serviceScopeFactoryMock = new Mock<IServiceScopeFactory>();
        serviceScopeMock
            .Setup(
                x =>
                    x.ServiceProvider.GetService(
                        typeof(IGenericRepository<TatuazUser, HistTatuazUser, string>)
                    )
            )
            .Returns(
                new GenericRepository<TatuazUser, HistTatuazUser, string>(
                    _dbContext,
                    new Mock<IMapper>().Object
                )
            );
        _serviceScopeFactoryMock.Setup(x => x.CreateScope()).Returns(serviceScopeMock.Object);
        _tatuazUserFaker = new TatuazUserFaker();
    }

    public class Handle : UserExistsQueryHandlerTest
    {
        public Handle(DbContext dbContext) : base(dbContext) { }

        [Fact]
        public async Task Should_Return_True_When_User_Exists()
        {
            var user = _tatuazUserFaker.Generate();
            _dbContext.Add(user);

            var query = new UserExistsQuery(user.Id);
            var handler = new UserExistsQueryHandler(_serviceScopeFactoryMock.Object);

            var result = await handler.Handle(query, CancellationToken.None).ConfigureAwait(false);

            Assert.True(result);
        }

        [Fact]
        public async Task Should_Return_False_When_User_Does_Not_Exist()
        {
            var query = new UserExistsQuery(Guid.NewGuid().ToString());
            var handler = new UserExistsQueryHandler(_serviceScopeFactoryMock.Object);

            var result = await handler.Handle(query, CancellationToken.None).ConfigureAwait(false);

            Assert.False(result);
        }
    }
}
