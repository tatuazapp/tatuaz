using Microsoft.Extensions.Logging;
using Tatuaz.History.DataAccess.Services;
using Tatuaz.History.Queue.Consumers.Common;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;

namespace Tatuaz.History.Queue.Consumers;

/// <summary>
///     Przykładowy Consumer żeby wiedzieć jak zarejestrować
///     do usunięcia gdy powstanie pierwszy prawdziwy
///     TODO: usunąć
/// </summary>
public class Test1Consumer : HistGetByIdConsumer<HistEntity<Guid>, Guid>
{
    public Test1Consumer(
        IHistorySearcherService<HistEntity<Guid>, Guid> historySearcherService,
        ILogger<Test1Consumer> logger
    ) : base(historySearcherService, logger)
    {
    }
}
