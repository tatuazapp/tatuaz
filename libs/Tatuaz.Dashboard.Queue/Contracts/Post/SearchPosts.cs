using Tatuaz.Shared.Domain.Dtos.Dtos.Common;
using Tatuaz.Shared.Domain.Dtos.Dtos.Post;

namespace Tatuaz.Dashboard.Queue.Contracts.Post;

public record SearchPosts(string Query, int PageNumber, int PageSize, bool Posts, bool Photos)
    : IDto;
