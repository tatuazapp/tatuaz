using System;
using Tatuaz.Shared.Domain.Dtos.Dtos.Common;

namespace Tatuaz.Shared.Domain.Dtos.Dtos.Identity;

public record BriefArtistDto(string Username, Uri? ForegroundPhotoUri, string? Bio, string? City)
    : IDto;
