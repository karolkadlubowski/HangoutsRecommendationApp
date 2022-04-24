using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Library.Shared.AppConfigs;
using Library.Shared.Clients.Abstractions;
using Library.Shared.Clients.Factories;
using Library.Shared.DI;
using Library.Shared.Extensions;
using Library.Shared.Logging;
using Library.Shared.Models.Category.Dtos;
using Library.Shared.Options;
using Venue.API.Application.Abstractions;
using Venue.API.Application.Providers;
using Venue.API.Domain.Configuration;
using Venue.API.Infrastructure.Clients.Factories;
using Venue.API.Infrastructure.Services.Requests.CategoryApi;
using Venue.API.Infrastructure.Services.Requests.Factories;
using Venue.API.Infrastructure.Services.Responses.CategoryApi;

namespace Venue.API.Infrastructure.Services
{
    public class CategoryDataService : ICategoryDataService
    {
        private readonly IRestClient _restClient;
        private readonly ICategoriesCacheRepository _cacheRepository;
        private readonly ILogger _logger;

        private readonly RestClientConfig _restClientConfig;

        public CategoryDataService(IDIProvider diProvider,
            ICategoriesCacheRepository cacheRepository,
            IConfigurationProvider configurationProvider,
            ILogger logger)
        {
            _restClientConfig = configurationProvider.GetConfiguration().RestClientsConfig.CategoryApi;
            _restClient = diProvider.ResolveServiceWhere<IRestClientFactory, CategoryRestClientFactory>()
                .CreateRestClient(_restClientConfig.BaseApiUrl);
            _cacheRepository = cacheRepository;
            _logger = logger;
        }

        public async Task<IList<CategoryDto>> GetCategoriesAsync()
        {
            _logger.Info($">> Sending request to the Category API: '{_restClientConfig.BaseApiUrl}'. Request: {nameof(GetCategoriesRequest)}");

            var response = await _restClient.ExecuteAsync<GetCategoriesResponse>(
                RestRequestAbstractFactory.GetCategoriesRequest(new GetCategoriesRequest()));
            var getCategoriesResponse = response?.Content?.FromJSON<GetCategoriesResponse>(JsonOptions.JsonSerializerOptions);

            _logger.Trace($"Response from the Category API: {getCategoriesResponse?.ToJSON()}");

            if (getCategoriesResponse is not null && getCategoriesResponse.IsSucceeded)
            {
                _logger.Info($"<< Response from the Category API is successful. Categories loaded count: {getCategoriesResponse.Categories.Count}");
                return getCategoriesResponse.Categories.ToList();
            }

            _logger.Warning($"Fetching data from the Category API failed. Message: {getCategoriesResponse.Error?.Message}");

            return ImmutableList<CategoryDto>.Empty;
        }

        public async Task StoreCategoriesInCacheAsync(IList<CategoryDto> categories)
        {
            await _cacheRepository.SetValueAsync(CacheKeys.Categories, categories);
            _logger.Info($"{categories.Count} categories stored in the memory cache under the key '{CacheKeys.Categories}'");
        }
    }
}