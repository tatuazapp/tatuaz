using MediatR;
using Tatuaz.Shared.Domain.Dtos.Dtos.Identity.User;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Gateway.Requests.Commands.Identity;

public record SignUpCommand(SignUpDto SignUpDto) : IRequest<TatuazResult<UserDto>>;
