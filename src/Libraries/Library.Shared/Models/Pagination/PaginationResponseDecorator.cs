namespace Library.Shared.Models.Pagination
{
    public record PaginationResponseDecorator
    {
        public int CurrentPage { get; init; }
        public int CurrentCount { get; init; }
        public int PageSize { get; init; }
        public int TotalCount { get; init; }
        public int TotalPages { get; init; }

        public PaginationResponseDecorator(int currentPage,
            int currentCount,
            int pageSize,
            int totalCount,
            int totalPages)
        {
            CurrentPage = currentPage;
            CurrentCount = currentCount;
            PageSize = pageSize;
            TotalCount = totalCount;
            TotalPages = totalPages;
        }

        public static PaginationResponseDecorator Create<T>(IPagedList<T> pagedList)
            => new PaginationResponseDecorator(pagedList.CurrentPage,
                pagedList.CurrentCount,
                pagedList.PageSize,
                pagedList.TotalCount,
                pagedList.TotalPages
            );
    }
}