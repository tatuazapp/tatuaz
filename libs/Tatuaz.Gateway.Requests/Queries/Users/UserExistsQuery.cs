using MediatR;

namespace Tatuaz.Gateway.Requests.Queries.Users;

public record UserExistsQuery(string UserId) : IRequest<bool>;
