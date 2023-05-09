using MediatR;
using Tatuaz.Shared.Domain.Dtos.Dtos.Identity;
using Tatuaz.Shared.Infrastructure.Abstractions.Paging;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Gateway.Requests.Queries.Identity;

public record GetTopArtistsQuery(GetTopArtistsDto GetTopArtistsDto)
    : IRequest<TatuazResult<PagedData<BriefArtistDto>>>;
