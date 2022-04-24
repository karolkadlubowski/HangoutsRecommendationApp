using System.Collections.Generic;

namespace Library.Shared.Models.Pagination.Models
{
    public record PaginationTuple<T>
    (
        IReadOnlyList<T> List,
        PaginationResponseDecorator Pagination
    );
}