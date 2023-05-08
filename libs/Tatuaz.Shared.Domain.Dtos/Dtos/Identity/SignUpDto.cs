using Tatuaz.Shared.Domain.Dtos.Dtos.Common;

namespace Tatuaz.Shared.Domain.Dtos.Dtos.Identity;

public record SignUpDto(string? Username, int[]? CategoryIds) : IDto;
