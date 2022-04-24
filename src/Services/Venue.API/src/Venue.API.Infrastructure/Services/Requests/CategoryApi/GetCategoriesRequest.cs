namespace Venue.API.Infrastructure.Services.Requests.CategoryApi
{
    public record GetCategoriesRequest : IRestRequestEndpoint
    {
        public string Endpoint => string.Empty;
    }
}