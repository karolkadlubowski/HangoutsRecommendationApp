using RestSharp;
using VenueList.API.Infrastructure.Services.Requests.CategoryApi;

namespace VenueList.API.Infrastructure.Services.Requests.Factories
{
    public abstract class RestRequestAbstractFactory
    {
        public static IRestRequest GetCategoriesRequest(GetCategoriesRequest request)
            => new RestRequest(request.Endpoint, Method.GET);
    }
}