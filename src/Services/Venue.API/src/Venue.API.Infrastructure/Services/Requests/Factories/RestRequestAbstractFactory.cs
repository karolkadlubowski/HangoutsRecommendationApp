using RestSharp;
using Venue.API.Infrastructure.Services.Requests.CategoryApi;

namespace Venue.API.Infrastructure.Services.Requests.Factories
{
    public abstract class RestRequestAbstractFactory
    {
        public static RestRequest GetCategoriesRequest(GetCategoriesRequest request)
            => new RestRequest(request.Endpoint, Method.GET);
    }
}