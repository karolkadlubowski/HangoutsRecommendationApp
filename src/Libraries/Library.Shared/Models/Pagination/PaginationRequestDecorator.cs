namespace Library.Shared.Models.Pagination
{
    public abstract record PaginationRequestDecorator
    {
        protected const int MaxPageSize = int.MaxValue;
        protected const int MinPageNumber = 1;
        protected const int DefaultPageSize = 50;

        protected int pageNumber = MinPageNumber;

        public int PageNumber
        {
            get => pageNumber;
            init => pageNumber = (value < MinPageNumber) ? MinPageNumber : value;
        }

        protected int pageSize = DefaultPageSize;

        public int PageSize
        {
            get => pageSize;
            init => pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
    }
}