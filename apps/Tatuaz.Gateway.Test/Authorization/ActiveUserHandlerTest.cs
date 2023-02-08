using System;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Moq;
using Tatuaz.Gateway.Authorization;
using Tatuaz.Gateway.Requests.Queries.Identity;
using Tatuaz.Testing.Mocks.Infrastructure;
using Xunit;

namespace Tatuaz.Gateway.Test.Authorization;

public class ActiveUserHandlerTest
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly UserContextMock _userContextMock;

    public ActiveUserHandlerTest()
    {
        _userContextMock = new UserContextMock();
        _mediatorMock = new Mock<IMediator>();
    }

    public class HandleAsync : ActiveUserHandlerTest
    {
        [Fact]
        public async Task Should_FailWithoutCallingMediatorWhenEmailIsNull()
        {
            var handler = new ActiveUserHandler(_mediatorMock.Object);
            var claimsPrincipal = new GenericPrincipal(
                new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.NameIdentifier, "1") }),
                null
            );
            var context = new AuthorizationHandlerContext(
                new[] { new ActiveUserRequirement() },
                claimsPrincipal,
                null
            );
            await handler.HandleAsync(context).ConfigureAwait(false);

            Assert.False(context.HasSucceeded);
            _mediatorMock.Verify(
                m => m.Send(It.IsAny<UserExistsQuery>(), It.IsAny<CancellationToken>()),
                Times.Never
            );
        }

        [Fact]
        public async Task Should_FailWithoutCallingMediatorWhenIdIsNull()
        {
            var handler = new ActiveUserHandler(_mediatorMock.Object);
            var claimsPrincipal = new GenericPrincipal(
                new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.Email, "1") }),
                null
            );
            var context = new AuthorizationHandlerContext(
                new[] { new ActiveUserRequirement() },
                claimsPrincipal,
                null
            );
            await handler.HandleAsync(context).ConfigureAwait(false);

            Assert.False(context.HasSucceeded);
            _mediatorMock.Verify(
                m => m.Send(It.IsAny<UserExistsQuery>(), It.IsAny<CancellationToken>()),
                Times.Never
            );
        }

        [Fact]
        public async Task Should_SucceedWhenUserExists()
        {
            var handler = new ActiveUserHandler(_mediatorMock.Object);
            var claimsPrincipal = new GenericPrincipal(
                new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Email, "1"),
                        new Claim(ClaimTypes.NameIdentifier, "1")
                    }
                ),
                null
            );
            var context = new AuthorizationHandlerContext(
                new[] { new ActiveUserRequirement() },
                claimsPrincipal,
                null
            );
            _mediatorMock
                .Setup(m => m.Send(It.IsAny<UserExistsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);
            await handler.HandleAsync(context).ConfigureAwait(false);

            Assert.True(context.HasSucceeded);
            _mediatorMock.Verify(
                m => m.Send(It.IsAny<UserExistsQuery>(), It.IsAny<CancellationToken>()),
                Times.Once
            );
        }
    }
}
