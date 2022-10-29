using MediatR;
using Tatuaz.Shared.Domain.Dtos.Dtos.Identity;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Gateway.Requests.Queries.Users;

public record WhoAmIQuery(string UserId) : IRequest<TatuazResult<UserDto>>;
