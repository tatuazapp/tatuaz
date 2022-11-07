using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Gateway.Queue.Contracts.Landing.ListArtistStats;

public record ListArtistStatsOrder(int Amount) : TatuazMessage((string?)null);
