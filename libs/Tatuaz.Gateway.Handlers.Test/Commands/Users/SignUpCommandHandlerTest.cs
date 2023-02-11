using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using Tatuaz.Gateway.Handlers.Commands.Identity;
using Tatuaz.Gateway.Requests.Commands.Identity;
using Tatuaz.Shared.Domain.Dtos.Fakers.Dtos.Identity;
using Tatuaz.Shared.Domain.Dtos.Validators.Identity;
using Tatuaz.Shared.Domain.Dtos.Validators.Identity.User;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Identity;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Photo;
using Tatuaz.Shared.Domain.Entities.Models.Identity;
using Tatuaz.Shared.Domain.Entities.Models.Photo;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;
using Tatuaz.Testing.Mocks.Infrastructure;
using Xunit;

namespace Tatuaz.Gateway.Handlers.Test.Commands.Users;

public class SignUpCommandHandlerTest
{
    private readonly SignUpDtoFaker _signUpDtoFaker;
    private readonly IMapper _mapper;
    private readonly UnitOfWorkMock _unitOfWorkMock;
    private readonly UserContextMock _userContextMock;
    private readonly Mock<
        IGenericRepository<TatuazUser, HistTatuazUser, string>
    > _userRepositoryMock;
    private readonly Mock<
        IGenericRepository<PhotoCategory, HistPhotoCategory, int>
    > _photoCategoryRepositoryMock;
    private readonly Mock<
        IGenericRepository<UserPhotoCategory, HistUserPhotoCategory, Guid>
    > _userPhotoCategoryRepositoryMock;

    public SignUpCommandHandlerTest(IMapper mapper)
    {
        _mapper = mapper;
        _userContextMock = new UserContextMock();
        _unitOfWorkMock = new UnitOfWorkMock();
        _signUpDtoFaker = new SignUpDtoFaker();
        _userRepositoryMock = new Mock<IGenericRepository<TatuazUser, HistTatuazUser, string>>();
        _photoCategoryRepositoryMock =
            new Mock<IGenericRepository<PhotoCategory, HistPhotoCategory, int>>();
        _userPhotoCategoryRepositoryMock =
            new Mock<IGenericRepository<UserPhotoCategory, HistUserPhotoCategory, Guid>>();
    }

    public class Handle : SignUpCommandHandlerTest
    {
        public Handle(IMapper mapper)
            : base(mapper) { }

        [Fact]
        public async Task Should_ReturnUserWhenCorrectCommandProvided()
        {
            _userRepositoryMock
                .Setup(x => x.ExistsByIdAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(false));

            _photoCategoryRepositoryMock
                .Setup(x => x.ExistsByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(true));

            var commandHandler = new SignUpCommandHandler(
                _userRepositoryMock.Object,
                _userPhotoCategoryRepositoryMock.Object,
                _photoCategoryRepositoryMock.Object,
                _unitOfWorkMock.Object,
                _mapper,
                _userContextMock.Object,
                new SignUpDtoValidator(
                    _userRepositoryMock.Object,
                    _photoCategoryRepositoryMock.Object
                )
            );
            var createUserDto = _signUpDtoFaker.Generate();
            var command = new SignUpCommand(createUserDto);

            var result = await commandHandler
                .Handle(command, CancellationToken.None)
                .ConfigureAwait(false);
            var userDto = result.Value;
            Assert.True(result.Successful);
            Assert.NotNull(userDto);
            Assert.Equal(userDto.Username, createUserDto.Username);
        }

        [Fact]
        public async Task Should_ReturnErrorWhenUserAlreadyExists()
        {
            _userRepositoryMock
                .Setup(x => x.ExistsByIdAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(true));

            _photoCategoryRepositoryMock
                .Setup(x => x.ExistsByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(true));

            var commandHandler = new SignUpCommandHandler(
                _userRepositoryMock.Object,
                _userPhotoCategoryRepositoryMock.Object,
                _photoCategoryRepositoryMock.Object,
                _unitOfWorkMock.Object,
                _mapper,
                _userContextMock.Object,
                new SignUpDtoValidator(
                    _userRepositoryMock.Object,
                    _photoCategoryRepositoryMock.Object
                )
            );
            var createUserDto = _signUpDtoFaker.Generate();
            var command = new SignUpCommand(createUserDto);

            var result = await commandHandler
                .Handle(command, CancellationToken.None)
                .ConfigureAwait(false);
            Assert.False(result.Successful);
            Assert.Single(result.Errors);
            Assert.Equal("UserAlreadyExists", result.Errors[0].Code);
        }

        [Fact]
        public async Task Should_ReturnErrorWhenValidationErrorsOccur()
        {
            _photoCategoryRepositoryMock
                .Setup(x => x.ExistsByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(true));

            var commandHandler = new SignUpCommandHandler(
                _userRepositoryMock.Object,
                _userPhotoCategoryRepositoryMock.Object,
                _photoCategoryRepositoryMock.Object,
                _unitOfWorkMock.Object,
                _mapper,
                _userContextMock.Object,
                new SignUpDtoValidator(
                    _userRepositoryMock.Object,
                    _photoCategoryRepositoryMock.Object
                )
            );
            var createUserDto = _signUpDtoFaker.Generate();
            createUserDto = createUserDto with
            {
                Username =
                    "Very very long invalid email. You may ask why I have made it so long but it's just for testing purposes so chill."
            };
            var command = new SignUpCommand(createUserDto);

            var result = await commandHandler
                .Handle(command, CancellationToken.None)
                .ConfigureAwait(false);
            Assert.False(result.Successful);
            Assert.Equal(2, result.Errors.Length);
            Assert.Equal("UsernameTooLong", result.Errors[0].Code);
        }

        [Fact]
        public async Task Should_CallUserRepositoryExistsByIdAsync()
        {
            _photoCategoryRepositoryMock
                .Setup(x => x.ExistsByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(true));

            var commandHandler = new SignUpCommandHandler(
                _userRepositoryMock.Object,
                _userPhotoCategoryRepositoryMock.Object,
                _photoCategoryRepositoryMock.Object,
                _unitOfWorkMock.Object,
                _mapper,
                _userContextMock.Object,
                new SignUpDtoValidator(
                    _userRepositoryMock.Object,
                    _photoCategoryRepositoryMock.Object
                )
            );
            var createUserDto = _signUpDtoFaker.Generate();
            var command = new SignUpCommand(createUserDto);

            await commandHandler.Handle(command, CancellationToken.None).ConfigureAwait(false);
            _userRepositoryMock.Verify(
                x => x.ExistsByIdAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()),
                Times.Once
            );
        }

        [Fact]
        public async Task Should_CallUnitOfWorkRunInTransactionAsyncOnSuccess()
        {
            _userRepositoryMock
                .Setup(x => x.ExistsByIdAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(false));

            _photoCategoryRepositoryMock
                .Setup(x => x.ExistsByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(true));

            var commandHandler = new SignUpCommandHandler(
                _userRepositoryMock.Object,
                _userPhotoCategoryRepositoryMock.Object,
                _photoCategoryRepositoryMock.Object,
                _unitOfWorkMock.Object,
                _mapper,
                _userContextMock.Object,
                new SignUpDtoValidator(
                    _userRepositoryMock.Object,
                    _photoCategoryRepositoryMock.Object
                )
            );
            var createUserDto = _signUpDtoFaker.Generate();
            var command = new SignUpCommand(createUserDto);

            await commandHandler.Handle(command, CancellationToken.None).ConfigureAwait(false);
            _unitOfWorkMock.Verify(
                x =>
                    x.RunInTransactionAsync(
                        It.IsAny<Func<CancellationToken, Task>>(),
                        It.IsAny<Action<Exception>>(),
                        It.IsAny<CancellationToken>()
                    ),
                Times.Once
            );
        }

        [Fact]
        public async Task Should_NotCallUnitOfWorkSaveChangesAsyncOnFailure()
        {
            _userRepositoryMock
                .Setup(x => x.ExistsByIdAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(true));
            var commandHandler = new SignUpCommandHandler(
                _userRepositoryMock.Object,
                _userPhotoCategoryRepositoryMock.Object,
                _photoCategoryRepositoryMock.Object,
                _unitOfWorkMock.Object,
                _mapper,
                _userContextMock.Object,
                new SignUpDtoValidator(
                    _userRepositoryMock.Object,
                    _photoCategoryRepositoryMock.Object
                )
            );
            var createUserDto = _signUpDtoFaker.Generate();
            var command = new SignUpCommand(createUserDto);

            await commandHandler.Handle(command, CancellationToken.None).ConfigureAwait(false);
            _unitOfWorkMock.Verify(
                x => x.SaveChangesAsync(It.IsAny<CancellationToken>()),
                Times.Never
            );
        }
    }
}
