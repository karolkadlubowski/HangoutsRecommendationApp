using System.Threading;
using System.Threading.Tasks;
using Library.Shared.Logging;
using Library.Shared.Models.Category.Dtos;
using MediatR;
using Venue.API.Application.Abstractions;

namespace Venue.API.Application.Handlers.AddCategoryToCache
{
    public class AddCategoryToCacheCommandHandler : IRequestHandler<AddCategoryToCacheCommand, AddCategoryToCacheResponse>
    {
        private readonly ICategoriesCacheRepository _cacheRepository;
        private readonly ILogger _logger;

        public AddCategoryToCacheCommandHandler(ICategoriesCacheRepository cacheRepository,
            ILogger logger)
        {
            _cacheRepository = cacheRepository;
            _logger = logger;
        }

        public async Task<AddCategoryToCacheResponse> Handle(AddCategoryToCacheCommand request, CancellationToken cancellationToken)
        {
            await _cacheRepository.StoreCategoryAsync(new CategoryDto
            {
                CategoryId = request.CategoryId,
                Name = request.CategoryName
            });

            _logger.Info($"Category #{request.CategoryId} with name '{request.CategoryName}' stored in the memory cache successfully");

            return new AddCategoryToCacheResponse { CategoryId = request.CategoryId };
        }
    }
}