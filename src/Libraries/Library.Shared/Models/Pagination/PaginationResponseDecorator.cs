namespace Library.Shared.Models.Pagination
{
    public record PaginationResponseDecorator<T>
    {
        public int CurrentPage { get; init; }
        public int PageSize { get; init; }
        public int TotalCount { get; init; }
        public int TotalPages { get; init; }

        public PaginationResponseDecorator(int currentPage,
            int pageSize,
            int totalCount,
            int totalPages)
        {
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalCount = totalCount;
            TotalPages = totalPages;
        }

        public static PaginationResponseDecorator<T> Create(IPagedList<T> pagedList)
            => new PaginationResponseDecorator<T>(pagedList.CurrentPage,
                pagedList.PageSize,
                pagedList.TotalCount,
                pagedList.TotalPages
            );
    }
}