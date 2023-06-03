using System;
using NodaTime;
using Tatuaz.Shared.Domain.Dtos.Dtos.Common;
using Tatuaz.Shared.Domain.Entities.Models.Booking;

namespace Tatuaz.Shared.Domain.Dtos.Dtos.Booking;

public record BookingRequestDto(
    int Id,
    string ArtistName,
    DateTime Start,
    DateTime End,
    string? Comment,
    BookingRequestStatus Status
) : IDto;
