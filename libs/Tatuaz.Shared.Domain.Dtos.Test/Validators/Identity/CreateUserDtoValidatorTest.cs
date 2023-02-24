using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using Tatuaz.Shared.Domain.Dtos.Fakers.Dtos.Identity;
using Tatuaz.Shared.Domain.Dtos.Validators.Identity;
using Tatuaz.Shared.Domain.Dtos.Validators.Identity.User;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Identity;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Photo;
using Tatuaz.Shared.Domain.Entities.Models.Identity;
using Tatuaz.Shared.Domain.Entities.Models.Photo;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;
using Tatuaz.Shared.Infrastructure.DataAccess;
using Tatuaz.Testing.Mocks.Infrastructure;
using Xunit;

namespace Tatuaz.Shared.Domain.Dtos.Test.Validators.Identity;

public class CreateUserDtoValidatorTest
{
    private readonly SignUpDtoFaker _signUpDtoFaker;
    private readonly MainDbContextMock _dbContextMock;
    private readonly GenericRepository<TatuazUser, HistTatuazUser, string> _userRepository;
    private readonly Mock<
        IGenericRepository<Category, HistCategory, int>
    > _photoCategoryRepositoryMock;

    public CreateUserDtoValidatorTest()
    {
        _dbContextMock = new MainDbContextMock();
        _userRepository = new GenericRepository<TatuazUser, HistTatuazUser, string>(
            _dbContextMock.Object,
            new Mock<IMapper>().Object
        );
        _photoCategoryRepositoryMock =
            new Mock<IGenericRepository<Category, HistCategory, int>>();
        _photoCategoryRepositoryMock
            .Setup(x => x.ExistsByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(true));

        _signUpDtoFaker = new SignUpDtoFaker();
    }

    public class ValidateAsync : CreateUserDtoValidatorTest
    {
        [Fact]
        public async Task Should_ReturnValidWhenCorrectCreateUserDtoProvided()
        {
            var createUserDto = _signUpDtoFaker.Generate();
            var validator = new SignUpDtoValidator(
                _userRepository,
                _photoCategoryRepositoryMock.Object
            );

            var result = await validator.ValidateAsync(createUserDto).ConfigureAwait(false);
            Assert.True(result.IsValid);
        }

        [Fact]
        public async Task Should_ReturnInvalidWhenUserUsernameAlreadyInUse()
        {
            var createUserDto = _signUpDtoFaker.Generate();
            _dbContextMock.TatuazUsers.Add(new TatuazUser { Username = createUserDto.Username! });
            var validator = new SignUpDtoValidator(
                _userRepository,
                _photoCategoryRepositoryMock.Object
            );

            var result = await validator.ValidateAsync(createUserDto).ConfigureAwait(false);
            Assert.False(result.IsValid);
            Assert.Equal("UsernameAlreadyInUse", result.Errors.First().ErrorCode);
        }

        [Fact]
        public async Task Should_ReturnInvalidWhenUsernameTooLong()
        {
            var createUserDto = _signUpDtoFaker.Generate();
            createUserDto = createUserDto with { Username = new string('a', 33) };
            var validator = new SignUpDtoValidator(
                _userRepository,
                _photoCategoryRepositoryMock.Object
            );

            var result = await validator.ValidateAsync(createUserDto).ConfigureAwait(false);
            Assert.False(result.IsValid);
            Assert.Equal("UsernameTooLong", result.Errors.First().ErrorCode);
        }

        [Fact]
        public async Task Should_ReturnInvalidWhenUsernameTooShort()
        {
            var createUserDto = _signUpDtoFaker.Generate();
            createUserDto = createUserDto with { Username = new string('a', 3) };
            var validator = new SignUpDtoValidator(
                _userRepository,
                _photoCategoryRepositoryMock.Object
            );

            var result = await validator.ValidateAsync(createUserDto).ConfigureAwait(false);
            Assert.False(result.IsValid);
            Assert.Equal("UsernameTooShort", result.Errors.First().ErrorCode);
        }

        [Fact]
        public async Task Should_ReturnInvalidWhenUsernameContainsInvalidCharacters()
        {
            var createUserDto = _signUpDtoFaker.Generate();
            createUserDto = createUserDto with { Username = "a b as as " };
            var validator = new SignUpDtoValidator(
                _userRepository,
                _photoCategoryRepositoryMock.Object
            );

            var result = await validator.ValidateAsync(createUserDto).ConfigureAwait(false);
            Assert.False(result.IsValid);
            Assert.Equal("UsernameInvalidCharacters", result.Errors.First().ErrorCode);
        }
    }
}
