using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using Tatuaz.Gateway.Handlers.Commands.Users;
using Tatuaz.Gateway.Requests.Commands.Users;
using Tatuaz.Shared.Domain.Dtos.Dtos.Identity;
using Tatuaz.Shared.Domain.Dtos.Fakers.Identity;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Identity;
using Tatuaz.Shared.Domain.Entities.Models.Identity;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;
using Tatuaz.Shared.Infrastructure.DataAccess;
using Tatuaz.Testing.Mocks.Infrastructure;
using Xunit;

namespace Tatuaz.Gateway.Handlers.Test.Commands.Users;

public class SignUpCommandHandlerTest
{
    private readonly IMapper _mapper;
    private readonly UserAccessorMock _userAccessorMock;
    private readonly UnitOfWorkMock _unitOfWorkMock;
    private readonly CreateUserDtoFaker _createUserDtoFaker;
    private readonly GatewayDbContextMock _dbContextMock;
    private readonly Mock<IGenericRepository<TatuazUser, HistTatuazUser, string>> _userRepositoryMock;

    public SignUpCommandHandlerTest(IMapper mapper)
    {
        _mapper = mapper;
        _userAccessorMock = new UserAccessorMock();
        _unitOfWorkMock = new UnitOfWorkMock();
        _createUserDtoFaker = new CreateUserDtoFaker();
        _dbContextMock = new GatewayDbContextMock();
        _userRepositoryMock = new Mock<IGenericRepository<TatuazUser, HistTatuazUser, string>>();
    }

    public class Handle : SignUpCommandHandlerTest
    {
        [Fact]
        public async Task Should_ReturnUserWhenCorrectCommandProvided()
        {
            var commandHandler = new SignUpCommandHandler(_userRepositoryMock.Object, _unitOfWorkMock.Object, _mapper,
                _userAccessorMock.Object);
            var createUserDto = _createUserDtoFaker.Generate();
            var command = new SignUpCommand(createUserDto);

            var result = await commandHandler.Handle(command, CancellationToken.None).ConfigureAwait(false);
            var userDto = result.Value;
            Assert.True(result.Successful);
            Assert.NotNull(userDto);
            Assert.Equal(userDto.Email, createUserDto.Email.ToLower());
            Assert.Equal(userDto.Username, createUserDto.Username);
        }

        [Fact]
        public async Task Should_ReturnErrorWhenUserAlreadyExists()
        {
            _userRepositoryMock.Setup(x => x.ExistsByIdAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(true));
            var commandHandler = new SignUpCommandHandler(_userRepositoryMock.Object, _unitOfWorkMock.Object, _mapper,
                _userAccessorMock.Object);
            var createUserDto = _createUserDtoFaker.Generate();
            var command = new SignUpCommand(createUserDto);

            var result = await commandHandler.Handle(command, CancellationToken.None).ConfigureAwait(false);
            Assert.False(result.Successful);
            Assert.Single(result.Errors);
            Assert.Equal("UserAlreadyExists", result.Errors[0].Code);
        }

        [Fact]
        public async Task Should_ReturnErrorWhenValidationErrorsOccur()
        {
            var commandHandler = new SignUpCommandHandler(_userRepositoryMock.Object, _unitOfWorkMock.Object, _mapper,
                _userAccessorMock.Object);
            var createUserDto = _createUserDtoFaker.Generate();
            createUserDto = createUserDto with { Email = "invalidEmail" };
            var command = new SignUpCommand(createUserDto);

            var result = await commandHandler.Handle(command, CancellationToken.None).ConfigureAwait(false);
            Assert.False(result.Successful);
            Assert.Single(result.Errors);
            Assert.Equal("EmailInvalid", result.Errors[0].Code);
        }

        [Fact]
        public async Task Should_CallUserRepositoryExistsByIdAsync()
        {
            var commandHandler = new SignUpCommandHandler(_userRepositoryMock.Object, _unitOfWorkMock.Object, _mapper,
                _userAccessorMock.Object);
            var createUserDto = _createUserDtoFaker.Generate();
            var command = new SignUpCommand(createUserDto);

            await commandHandler.Handle(command, CancellationToken.None).ConfigureAwait(false);
            _userRepositoryMock.Verify(x => x.ExistsByIdAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Should_CallUserRepositoryCreate()
        {
            var commandHandler = new SignUpCommandHandler(_userRepositoryMock.Object, _unitOfWorkMock.Object, _mapper,
                _userAccessorMock.Object);
            var createUserDto = _createUserDtoFaker.Generate();
            var command = new SignUpCommand(createUserDto);

            await commandHandler.Handle(command, CancellationToken.None).ConfigureAwait(false);
            _userRepositoryMock.Verify(x =>
                x.Create(It.IsAny<TatuazUser>()));
        }

        [Fact]
        public async Task Should_CallUnitOfWorkSaveChangesAsyncOnSuccess()
        {
            var commandHandler = new SignUpCommandHandler(_userRepositoryMock.Object, _unitOfWorkMock.Object, _mapper,
                _userAccessorMock.Object);
            var createUserDto = _createUserDtoFaker.Generate();
            var command = new SignUpCommand(createUserDto);

            await commandHandler.Handle(command, CancellationToken.None).ConfigureAwait(false);
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Should_NotCallUnitOfWorkSaveChangesAsyncOnFailure()
        {
            _userRepositoryMock.Setup(x => x.ExistsByIdAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(true));
            var commandHandler = new SignUpCommandHandler(_userRepositoryMock.Object, _unitOfWorkMock.Object, _mapper,
                _userAccessorMock.Object);
            var createUserDto = _createUserDtoFaker.Generate();
            var command = new SignUpCommand(createUserDto);

            await commandHandler.Handle(command, CancellationToken.None).ConfigureAwait(false);
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }

        public Handle(IMapper mapper) : base(mapper)
        {
        }
    }
}
