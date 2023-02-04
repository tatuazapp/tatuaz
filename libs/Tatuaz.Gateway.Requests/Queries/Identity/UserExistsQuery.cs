using MediatR;

namespace Tatuaz.Gateway.Requests.Queries.Identity;

public record UserExistsQuery(string Email, string Auth0Id) : IRequest<bool>;
