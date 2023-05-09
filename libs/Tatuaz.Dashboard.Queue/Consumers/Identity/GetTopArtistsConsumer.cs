using System;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Tatuaz.Dashboard.Queue.Contracts.Identity;
using Tatuaz.Shared.Domain.Dtos.Dtos.Identity;
using Tatuaz.Shared.Domain.Dtos.Dtos.Photo.Category;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Identity;
using Tatuaz.Shared.Domain.Entities.Models.Identity;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;
using Tatuaz.Shared.Infrastructure.Abstractions.Paging;
using Tatuaz.Shared.Infrastructure.Specification;
using Tatuaz.Shared.Pipeline.Factories.Results;
using Tatuaz.Shared.Pipeline.Messages;
using Tatuaz.Shared.Pipeline.Queues;

namespace Tatuaz.Dashboard.Queue.Consumers.Identity;

public class GetTopArtistsConsumer : TatuazConsumerBase<GetTopArtists, PagedData<BriefArtistDto>>
{
    private readonly IGenericRepository<TatuazUser, HistTatuazUser, string> _userRepository;

    public GetTopArtistsConsumer(
        ILogger<GetTopArtistsConsumer> logger,
        IGenericRepository<TatuazUser, HistTatuazUser, string> userRepository
    )
        : base(logger)
    {
        _userRepository = userRepository;
    }

    protected override async Task<TatuazResult<PagedData<BriefArtistDto>>> ConsumeMessage(
        ConsumeContext<GetTopArtists> context
    )
    {
        var spec = new FullSpecification<TatuazUser>();
        spec.AddOrder(x => x.Popularity, OrderDirection.Descending);
        spec.AddOrder(x => x.Id);
        spec.AddFilter(x => x.UserRoles.Any(y => y.Role.Id == TatuazRole.ArtistId));

        return CommonResultFactory.Ok(
            await _userRepository
                .GetBySpecificationWithPagingAsync<BriefArtistDto>(
                    spec,
                    new PagedParams(context.Message.PageNumber, context.Message.PageSize),
                    context.CancellationToken
                )
                .ConfigureAwait(false)
        );
    }
}
