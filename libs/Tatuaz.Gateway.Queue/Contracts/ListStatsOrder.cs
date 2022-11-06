using NodaTime;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Gateway.Queue.Contracts;

public record ListStatsOrder
(
    Instant From,
    Instant To,
    int Amount
) : TatuazMessage((string?)null);
