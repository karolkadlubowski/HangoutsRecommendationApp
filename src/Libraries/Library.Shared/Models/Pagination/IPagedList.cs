using System.Collections.Generic;

namespace Library.Shared.Models.Pagination
{
    public interface IPagedList<T> : IEnumerable<T>
    {
        int CurrentPage { get; }
        int PageSize { get; }
        int TotalCount { get; }
        int TotalPages { get; }
    }
}