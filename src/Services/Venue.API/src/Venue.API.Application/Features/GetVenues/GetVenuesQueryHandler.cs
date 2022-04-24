using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Venue.API.Application.Features.GetVenues
{
    public class GetVenuesQueryHandler : IRequestHandler<GetVenuesQuery, GetVenuesResponse>
    {
        public async Task<GetVenuesResponse> Handle(GetVenuesQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}