using MediatR;
using Tatuaz.Shared.Domain.Dtos.Dtos.Post;
using Tatuaz.Shared.Infrastructure.Abstractions.Paging;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Gateway.Requests.Queries.Posts;

public record SearchPostsQuery(SearchPostsDto SearchPostsDto)
    : IRequest<TatuazResult<PagedData<BriefPostDto>>>;
