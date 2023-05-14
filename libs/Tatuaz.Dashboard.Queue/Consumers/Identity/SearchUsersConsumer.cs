using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Tatuaz.Dashboard.Queue.Contracts.Identity;
using Tatuaz.Shared.Domain.Dtos.Dtos.Identity;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Identity;
using Tatuaz.Shared.Domain.Entities.Models.Identity;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;
using Tatuaz.Shared.Infrastructure.Abstractions.Paging;
using Tatuaz.Shared.Infrastructure.Specification;
using Tatuaz.Shared.Pipeline.Factories.Results;
using Tatuaz.Shared.Pipeline.Messages;
using Tatuaz.Shared.Pipeline.Queues;

namespace Tatuaz.Dashboard.Queue.Consumers.Identity;

public class SearchUsersConsumer : TatuazConsumerBase<SearchUsers, PagedData<BriefUserDto>>
{
    private readonly IGenericRepository<TatuazUser, HistTatuazUser, string> _userRepository;

    public SearchUsersConsumer(
        ILogger<SearchUsersConsumer> logger,
        IGenericRepository<TatuazUser, HistTatuazUser, string> userRepository
    )
        : base(logger)
    {
        _userRepository = userRepository;
    }

    protected override async Task<TatuazResult<PagedData<BriefUserDto>>> ConsumeMessage(
        ConsumeContext<SearchUsers> context
    )
    {
        var spec = new FullSpecification<TatuazUser>();
        spec.AddFilter(x => x.Username.ToLower().Contains(context.Message.Query.ToLower()));
        if (context.Message.OnlyArtists)
        {
            spec.AddFilter(x => x.UserRoles.Any(y => y.Role.Id == TatuazRole.ArtistId));
        }

        var users = await _userRepository
            .GetBySpecificationWithPagingAsync<BriefUserDto>(
                spec,
                new PagedParams(context.Message.PageNumber, context.Message.PageSize),
                context.CancellationToken
            )
            .ConfigureAwait(false);

        return CommonResultFactory.Ok(users);
    }
}
