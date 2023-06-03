using System;
using NodaTime;
using Tatuaz.Shared.Domain.Dtos.Dtos.Common;

namespace Tatuaz.Shared.Domain.Dtos.Dtos.Booking;

public record SendBookingRequestDto(
    string? ArtistName,
    DateTime? Start,
    DateTime? End,
    string? Comment
) : IDto;
