using System;
using NodaTime;

namespace Tatuaz.Dashboard.Queue.Contracts.Booking;

public record SendBookingRequest(string ArtistName, Instant Start, Instant End, string? Comment);
