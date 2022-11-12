using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Tatuaz.Gateway.Queue.Contracts.Landing.ListArtistStats;
using Tatuaz.Shared.Domain.Dtos.Dtos.Landing.ListArtistStats;
using Tatuaz.Shared.Domain.Dtos.Fakers.Landing.ListArtistStats;
using Tatuaz.Shared.Pipeline.Factories.Results;
using Tatuaz.Shared.Pipeline.Messages;
using Tatuaz.Shared.Pipeline.Queues;

namespace Tatuaz.Landing.Queue.Consumers;

public class ListArtistStatsConsumer
    : TatuazConsumerBase<ListArtistStatsOrder, IEnumerable<ArtistStatDto>>
{
    public ListArtistStatsConsumer(ILogger<ListArtistStatsConsumer> logger) : base(logger) { }

    protected override Task<TatuazResult<IEnumerable<ArtistStatDto>>> ConsumeMessage(
        ListArtistStatsOrder message
    )
    {
        var faker = new ArtistStatDtoFaker();
        var result = faker.Generate(message.Amount);
        result = result
            .Select(
                x =>
                    x with
                    {
                        BackgroundUrl =
                            "https://akns-images.eonline.com/eol_images/Entire_Site/2022528/rs_1200x1200-220628043352-1200-margot-robbie-ryan-gosling-barbie-movie-062822.jpg?fit=around%7C1200:1200&output-quality=90&crop=1200:1200;center,top"
                    }
            )
            .ToList();

        return Task.FromResult(CommonResultFactory.Ok<IEnumerable<ArtistStatDto>>(result));
    }
}
