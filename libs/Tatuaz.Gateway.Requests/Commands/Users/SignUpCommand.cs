using MediatR;
using Tatuaz.Shared.Domain.Dtos.Dtos.Identity;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Gateway.Requests.Commands.Users;

public record SignUpCommand(CreateUserDto CreateUserDto) : IRequest<TatuazResult<UserDto>>;