using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using Tatuaz.Shared.Domain.Dtos.Fakers.Dtos.Identity;
using Tatuaz.Shared.Domain.Dtos.Validators.Identity;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Identity;
using Tatuaz.Shared.Domain.Entities.Models.Identity;
using Tatuaz.Shared.Infrastructure.DataAccess;
using Tatuaz.Testing.Mocks.Infrastructure;
using Xunit;

namespace Tatuaz.Shared.Domain.Dtos.Test.Validators.Identity;

public class CreateUserDtoValidatorTest
{
    private readonly CreateUserDtoFaker _createUserDtoFaker;
    private readonly GatewayDbContextMock _dbContextMock;
    private readonly GenericRepository<TatuazUser, HistTatuazUser, string> _userRepository;

    public CreateUserDtoValidatorTest()
    {
        _dbContextMock = new GatewayDbContextMock();
        _userRepository = new GenericRepository<TatuazUser, HistTatuazUser, string>(
            _dbContextMock.Object,
            new Mock<IMapper>().Object
        );
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
        public async Task Should_ReturnInvalidWhenUserUsernameAlreadyInUse()
        {
            var createUserDto = _createUserDtoFaker.Generate();
            _dbContextMock.TatuazUsers.Add(new TatuazUser { Username = createUserDto.Username! });
            var validator = new CreateUserDtoValidator(_userRepository);

            var result = await validator.ValidateAsync(createUserDto).ConfigureAwait(false);
            Assert.False(result.IsValid);
            Assert.Equal("UsernameAlreadyInUse", result.Errors.First().ErrorCode);
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
    }
}
