﻿namespace Tatuaz.Shared.Infrastructure.Abstractions.Paging;

public class PagedData<T>
{
    public PagedData(IEnumerable<T> data, int pageNumber, int pageSize, int totalPages, int totalCount)
    {
        Data = data;
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalPages = totalPages;
        TotalCount = totalCount;
    }

    public IEnumerable<T> Data { get; }
    public int PageNumber { get; }
    public int PageSize { get; }
    public int TotalPages { get; }
    public int TotalCount { get; }
}