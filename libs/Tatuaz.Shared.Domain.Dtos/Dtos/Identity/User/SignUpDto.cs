using Tatuaz.Shared.Domain.Dtos.Dtos.Common;

namespace Tatuaz.Shared.Domain.Dtos.Dtos.Identity.User;

public record SignUpDto(string? Username, int[]? CategoryIds) : IDto;
