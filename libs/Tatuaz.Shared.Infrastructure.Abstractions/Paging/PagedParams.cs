namespace Tatuaz.Shared.Infrastructure.Abstractions.Paging;

// PageNumber starts from 1
public record PagedParams(int PageNumber, int PageSize);
