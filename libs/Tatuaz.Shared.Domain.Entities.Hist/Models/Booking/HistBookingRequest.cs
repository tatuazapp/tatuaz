using System;
using NodaTime;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;

namespace Tatuaz.Shared.Domain.Entities.Hist.Models.Booking;

public class HistBookingRequest : HistEntity<int>
{
    public HistBookingRequestStatus Status { get; set; }
    public Instant Start { get; set; }
    public Instant End { get; set; }
    public string? Comment { get; set; }
    public string CustomerEmail { get; set; } = default!;
    public string ArtistEmail { get; set; } = default!;
}
