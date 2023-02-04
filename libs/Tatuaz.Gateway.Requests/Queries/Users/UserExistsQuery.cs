using MediatR;

namespace Tatuaz.Gateway.Requests.Queries.Users;

public record UserExistsQuery(string Email, string Auth0Id) : IRequest<bool>;
