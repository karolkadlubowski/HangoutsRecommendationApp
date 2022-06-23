namespace VenueList.API.Infrastructure.Services.Requests.CategoryApi
{
    public record GetCategoriesRequest : IRestRequestEndpoint
    {
        public string Endpoint => "list";
    }
}