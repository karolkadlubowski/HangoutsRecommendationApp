using Library.Shared.Models.Pagination;
using MediatR;

namespace Venue.API.Application.Features.GetVenues
{
    public record GetVenuesQuery : PaginationRequestDecorator, IRequest<GetVenuesResponse>;
}