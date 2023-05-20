using System.Collections.Generic;

namespace Tatuaz.Shared.Infrastructure.Abstractions.Paging;

public class PagedData<T>
{
    public PagedData(
        IEnumerable<T> data,
        int pageNumber,
        int pageSize,
        int totalPages,
        int totalCount
    )
    {
        Data = data;
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalPages = totalPages;
        TotalCount = totalCount;
    }

    public IEnumerable<T> Data { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; }
    public int TotalPages { get; set; }
    public int TotalCount { get; set; }
}
