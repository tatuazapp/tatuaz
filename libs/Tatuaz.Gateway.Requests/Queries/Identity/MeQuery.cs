using MediatR;
using Tatuaz.Shared.Domain.Dtos.Dtos.Identity.User;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Gateway.Requests.Queries.Identity;

public record MeQuery : IRequest<TatuazResult<UserDto>>;
