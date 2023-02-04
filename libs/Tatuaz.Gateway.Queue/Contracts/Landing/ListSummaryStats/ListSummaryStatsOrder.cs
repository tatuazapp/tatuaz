using NodaTime;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Gateway.Queue.Contracts.Landing.ListSummaryStats;

public record ListSummaryStatsOrder(Instant From, Instant To, int Amount);
