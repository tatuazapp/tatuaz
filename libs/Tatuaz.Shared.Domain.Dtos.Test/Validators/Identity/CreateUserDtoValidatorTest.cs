using System.Linq;
using System.Threading.Tasks;
using Tatuaz.Shared.Domain.Dtos.Fakers.Identity;
using Tatuaz.Shared.Domain.Dtos.Validators.Identity;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Identity;
using Tatuaz.Shared.Domain.Entities.Models.Identity;
using Tatuaz.Shared.Infrastructure.DataAccess;
using Tatuaz.Testing.Mocks.Infrastructure;
using Xunit;

namespace Tatuaz.Shared.Domain.Dtos.Test.Validators.Identity;

public class CreateUserDtoValidatorTest
{
    private readonly GatewayDbContextMock _dbContextMock;
    private readonly GenericRepository<TatuazUser, HistTatuazUser, string> _userRepository;
    private readonly CreateUserDtoFaker _createUserDtoFaker;

    public CreateUserDtoValidatorTest()
    {
        _dbContextMock = new GatewayDbContextMock();
        _userRepository = new GenericRepository<TatuazUser, HistTatuazUser, string>(_dbContextMock.Object);
        _createUserDtoFaker = new CreateUserDtoFaker();
    }

    public class ValidateAsync : CreateUserDtoValidatorTest
    {
        [Fact]
        public async Task Should_ReturnValidWhenCorrectCreateUserDtoProvided()
        {
            var createUserDto = _createUserDtoFaker.Generate();
            var validator = new CreateUserDtoValidator(_userRepository);

            var result = await validator.ValidateAsync(createUserDto).ConfigureAwait(false);
            Assert.True(result.IsValid);
        }

        [Fact]
        public async Task Should_ReturnInvalidWhenUserEmailAlreadyExists()
        {
            var createUserDto = _createUserDtoFaker.Generate();
            _dbContextMock.TatuazUsers.Add(new TatuazUser()
            {
                Email = createUserDto.Email
            });
            var validator = new CreateUserDtoValidator(_userRepository);

            var result = await validator.ValidateAsync(createUserDto).ConfigureAwait(false);
            Assert.False(result.IsValid);
            Assert.Equal("EmailAlreadyExists", result.Errors.First().ErrorCode);
        }

        [Fact]
        public async Task Should_ReturnInvalidWhenUserUsernameAlreadyExists()
        {
            var createUserDto = _createUserDtoFaker.Generate();
            _dbContextMock.TatuazUsers.Add(new TatuazUser()
            {
                Username = createUserDto.Username
            });
            var validator = new CreateUserDtoValidator(_userRepository);

            var result = await validator.ValidateAsync(createUserDto).ConfigureAwait(false);
            Assert.False(result.IsValid);
            Assert.Equal("UsernameAlreadyExists", result.Errors.First().ErrorCode);
        }

        [Fact]
        public async Task Should_ReturnInvalidWhenEmailIsEmpty()
        {
            var createUserDto = _createUserDtoFaker.Generate();
            createUserDto = createUserDto with { Email = string.Empty };
            var validator = new CreateUserDtoValidator(_userRepository);

            var result = await validator.ValidateAsync(createUserDto).ConfigureAwait(false);
            Assert.False(result.IsValid);
            Assert.Equal("EmailEmpty", result.Errors.First().ErrorCode);
        }

        [Fact]
        public async Task Should_ReturnInvalidWhenEmailIsInvalid()
        {
            var createUserDto = _createUserDtoFaker.Generate();
            createUserDto = createUserDto with { Email = "invalidEmail" };
            var validator = new CreateUserDtoValidator(_userRepository);

            var result = await validator.ValidateAsync(createUserDto).ConfigureAwait(false);
            Assert.False(result.IsValid);
            Assert.Equal("EmailInvalid", result.Errors.First().ErrorCode);
        }

        [Fact]
        public async Task Should_ReturnInvalidWhenEmailTooLong()
        {
            var createUserDto = _createUserDtoFaker.Generate();
            createUserDto = createUserDto with { Email = new string('a', 247) + "@gmail.com" };
            var validator = new CreateUserDtoValidator(_userRepository);

            var result = await validator.ValidateAsync(createUserDto).ConfigureAwait(false);
            Assert.False(result.IsValid);
            Assert.Equal("EmailTooLong", result.Errors.First().ErrorCode);
        }

        [Fact]
        public async Task Should_ReturnInvalidWhenUsernameTooLong()
        {
            var createUserDto = _createUserDtoFaker.Generate();
            createUserDto = createUserDto with { Username = new string('a', 33) };
            var validator = new CreateUserDtoValidator(_userRepository);

            var result = await validator.ValidateAsync(createUserDto).ConfigureAwait(false);
            Assert.False(result.IsValid);
            Assert.Equal("UsernameTooLong", result.Errors.First().ErrorCode);
        }

        [Fact]
        public async Task Should_ReturnInvalidWhenUsernameTooShort()
        {
            var createUserDto = _createUserDtoFaker.Generate();
            createUserDto = createUserDto with { Username = new string('a', 3) };
            var validator = new CreateUserDtoValidator(_userRepository);

            var result = await validator.ValidateAsync(createUserDto).ConfigureAwait(false);
            Assert.False(result.IsValid);
            Assert.Equal("UsernameTooShort", result.Errors.First().ErrorCode);
        }

        [Fact]
        public async Task Should_ReturnInvalidWhenPhoneNumberInvalid()
        {
            var createUserDto = _createUserDtoFaker.Generate();
            createUserDto = createUserDto with { PhoneNumber = "invalidPhoneNumber" };
            var validator = new CreateUserDtoValidator(_userRepository);

            var result = await validator.ValidateAsync(createUserDto).ConfigureAwait(false);
            Assert.False(result.IsValid);
            Assert.Equal("PhoneNumberInvalid", result.Errors.First().ErrorCode);
        }
    }
}
