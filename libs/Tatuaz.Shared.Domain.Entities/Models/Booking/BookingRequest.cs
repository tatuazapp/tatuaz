using System;
using NodaTime;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Booking;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;
using Tatuaz.Shared.Domain.Entities.Models.Common;
using Tatuaz.Shared.Domain.Entities.Models.Identity;

namespace Tatuaz.Shared.Domain.Entities.Models.Booking;

public class BookingRequest : Entity<HistBookingRequest, int>
{
    public BookingRequestStatus Status { get; set; }
    public Instant Start { get; set; }
    public Instant End { get; set; }
    public string? Comment { get; set; }
    public string ClientEmail { get; set; } = default!;
    public TatuazUser Client { get; set; } = default!;
    public string ArtistEmail { get; set; } = default!;
    public TatuazUser Artist { get; set; } = default!;

    public override HistEntity ToHistEntity(IClock clock, HistState state)
    {
        var histEntity = (HistBookingRequest)base.ToHistEntity(clock, state);
        histEntity.Status = (HistBookingRequestStatus)Status;
        histEntity.Start = Start;
        histEntity.End = End;
        histEntity.Comment = Comment;
        return histEntity;
    }
}
