using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;
using Library.Shared.Clients.Abstractions;
using Library.Shared.Clients.Factories;
using Library.Shared.Extensions;
using Library.Shared.Logging;
using Library.Shared.Models.Category.Dtos;
using Venue.API.Application.Abstractions;
using Venue.API.Application.Providers;
using Venue.API.Domain.Configuration;
using Venue.API.Infrastructure.Services.Requests.CategoryApi;
using Venue.API.Infrastructure.Services.Requests.Factories;
using Venue.API.Infrastructure.Services.Responses.CategoryApi;

namespace Venue.API.Infrastructure.Services
{
    public class CategoryDataService : ICategoryDataService
    {
        private readonly IRestClient _restClient;
        private readonly ILogger _logger;

        private readonly RestClientConfig _restClientConfig;

        public CategoryDataService(IRestClientFactory restClientFactory,
            IConfigurationProvider configurationProvider,
            ILogger logger)
        {
            _logger = logger;
            _restClientConfig = configurationProvider.GetConfiguration().RestClientsConfig.CategoryApi;
            _restClient = restClientFactory.CreateRestClient(_restClientConfig.BaseApiUrl);
        }

        public async Task<IReadOnlyList<CategoryDto>> GetCategoriesAsync()
        {
            _logger.Info($">> Sending request to the Category API: '{_restClientConfig.BaseApiUrl}'. Request: {nameof(GetCategoriesRequest)}");

            var response = await _restClient.ExecuteAsync<GetCategoriesResponse>(
                RestRequestAbstractFactory.GetCategoriesRequest(new GetCategoriesRequest()));
            var getCategoriesResponse = response.Content.FromJSON<GetCategoriesResponse>();

            _logger.Trace($"Response from the Category API: {response.ToJSON()}");

            if (getCategoriesResponse.IsSucceeded)
            {
                _logger.Info("<< Response from the Category API is successful");
                return getCategoriesResponse.Categories;
            }

            _logger.Warning("Fetching data from the Category API failed");

            return ImmutableList<CategoryDto>.Empty;
        }
    }
}