using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Tatuaz.Gateway.Queue.Contracts;
using Tatuaz.Gateway.Queue.Contracts.Landing.ListSummaryStats;
using Tatuaz.Shared.Domain.Dtos.Dtos.Landing.ListSummaryStats;
using Tatuaz.Shared.Pipeline.Factories.Results;
using Tatuaz.Shared.Pipeline.Messages;
using Tatuaz.Shared.Pipeline.Queues;

namespace Tatuaz.Landing.Queue.Consumers;

public class ListSummaryStatsConsumer
    : TatuazConsumerBase<ListSummaryStatsOrder, IEnumerable<SummaryStatDto>>
{
    public ListSummaryStatsConsumer(ILogger<ListSummaryStatsConsumer> logger) : base(logger) { }

    protected override Task<TatuazResult<IEnumerable<SummaryStatDto>>> ConsumeMessage(
        ListSummaryStatsOrder message
    )
    {
        var stats = new List<SummaryStatDto>
        {
            new(
                "Nowy rekord szczupaka w polsce",
                "190 cm",
                "https://dziendobry.tvn.pl/cdn-zdjecie-tqwcdx-fot-dieter-meryl-getty-images-5227704/alternates/FOUR_THREE_1280"
            ),
            new("Ryby stracone przez wędkarzy", "1 000 000", string.Empty),
            new(
                "Największy wędkarz na świecie",
                "1,90 m",
                "https://wedkarskiswiat.pl/wp-content/uploads/2019/09/najwiekszy-karp-swiata-730x530.jpg"
            ),
            new("Kłótnie na łowisku", "69000", string.Empty),
            new("Najlepsze przynęty", "robaki, krewetki", string.Empty),
            new("Wędkarze zjedzeni przez sumy", "12", string.Empty),
            new(
                "Minuty walki z rybami",
                "1 000 000",
                "https://www.rtw.org.pl/images/gallery/photos/bytyn2_ccfec497.jpg"
            ),
            new(
                "Rocznica pierwszego złowionego rekina",
                DateTime.Now.ToShortDateString(),
                "https://ocdn.eu/pulscms-transforms/1/mwik9kpTURBXy8xM2M0NWUwN2Q4YTkzZjdjNjE0ZjRlNDlkOGI0Nzg5Yy5qcGealQLNAxQAwsOVAgDNAvjCw5QGzP_M_8z_lAbM_8z_zP-UBsz_zP_M_5QGzP_M_8z_lAbM_8z_zP-UBsz_zP_M_5QGzP_M_8z_lAbM_8z_zP_eAAGhMAE"
            ),
            new(
                "Nowe rybne pary młode",
                "30",
                "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRvWekaZcXBAG5cEeFbaS1UFDVJNRHJVopxCg&usqp=CAU"
            ),
            new(
                "Rekordowe akwaria",
                "1",
                "https://demotywatory.pl/uploads/201811/1542733227_huzjlt_fb_plus.jpg"
            )
        };

        return Task.FromResult(
            CommonResultFactory.Ok(stats.OrderBy(_ => Guid.NewGuid()).Take(message.Amount))
        );
    }
}
