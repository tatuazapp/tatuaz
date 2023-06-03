using MediatR;
using Tatuaz.Shared.Domain.Dtos.Dtos.Post;
using Tatuaz.Shared.Domain.Dtos.Dtos.Post.GetPostDetails;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Gateway.Requests.Queries.Posts;

public record GetPostDetailsQuery(GetPostDetailsDto GetPostDetailsDto)
    : IRequest<TatuazResult<PostDetailsDto>>;
