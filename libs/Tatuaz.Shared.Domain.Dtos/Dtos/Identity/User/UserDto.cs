using System;
using Tatuaz.Shared.Domain.Dtos.Dtos.Common;

namespace Tatuaz.Shared.Domain.Dtos.Dtos.Identity.User;

public record UserDto(
    string Username,
    string Email,
    string Auth0Id,
    Uri? ForegroundPhotoUri,
    Uri? BackgroundPhotoUri
) : IDto;
