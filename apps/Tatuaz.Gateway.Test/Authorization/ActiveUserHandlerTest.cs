using System;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Moq;
using Tatuaz.Gateway.Authorization;
using Tatuaz.Gateway.Requests.Queries.Users;
using Tatuaz.Testing.Mocks.Infrastructure;
using Xunit;

namespace Tatuaz.Gateway.Test.Authorization;

public class ActiveUserHandlerTest
{
    private readonly UserAccessorMock _userAccessorMock;
    private readonly Mock<IMediator> _mediatorMock;

    public ActiveUserHandlerTest()
    {
        _userAccessorMock = new UserAccessorMock();
        _mediatorMock = new Mock<IMediator>();
    }

    public class HandleAsync : ActiveUserHandlerTest
    {
        [Fact]
        public async Task Should_FailWithoutCallingMediatorWhenUserIdIsNull()
        {
            _userAccessorMock.ReturnUserId(null);
            var handler = new ActiveUserHandler(_mediatorMock.Object, _userAccessorMock.Object);
            var claimsPrincipal =
                new GenericPrincipal(new ClaimsIdentity(Array.Empty<Claim>()), null);
            var context = new AuthorizationHandlerContext(new[] { new ActiveUserRequirement() },
                claimsPrincipal, null);
            await handler.HandleAsync(context).ConfigureAwait(false);

            Assert.False(context.HasSucceeded);
            _mediatorMock.Verify(m =>
                m.Send(It.IsAny<UserExistsQuery>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task Should_FailWhenUserDoesntExist()
        {
            _userAccessorMock.ReturnUserId("1");
            _mediatorMock.Setup(m => m.Send(It.IsAny<UserExistsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);
            var handler = new ActiveUserHandler(_mediatorMock.Object, _userAccessorMock.Object);
            var claimsPrincipal =
                new GenericPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, "1") }), null);
            var context = new AuthorizationHandlerContext(new[] { new ActiveUserRequirement() },
                claimsPrincipal, null);
            await handler.HandleAsync(context).ConfigureAwait(false);

            Assert.False(context.HasSucceeded);
            _mediatorMock.Verify(m =>
                m.Send(It.IsAny<UserExistsQuery>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Should_SucceedWhenUserExists()
        {
            _userAccessorMock.ReturnUserId("1");
            _mediatorMock.Setup(m => m.Send(It.IsAny<UserExistsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);
            var handler = new ActiveUserHandler(_mediatorMock.Object, _userAccessorMock.Object);
            var claimsPrincipal =
                new GenericPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, "1") }), null);
            var context = new AuthorizationHandlerContext(new[] { new ActiveUserRequirement() },
                claimsPrincipal, null);
            await handler.HandleAsync(context).ConfigureAwait(false);

            Assert.True(context.HasSucceeded);
            _mediatorMock.Verify(m =>
                m.Send(It.IsAny<UserExistsQuery>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
