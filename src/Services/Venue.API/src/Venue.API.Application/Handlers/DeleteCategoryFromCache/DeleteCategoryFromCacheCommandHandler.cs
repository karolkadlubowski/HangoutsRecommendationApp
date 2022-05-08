using System.Threading;
using System.Threading.Tasks;
using Library.Shared.Logging;
using MediatR;
using Venue.API.Application.Abstractions;

namespace Venue.API.Application.Handlers.DeleteCategoryFromCache
{
    public class DeleteCategoryFromCacheCommandHandler : IRequestHandler<DeleteCategoryFromCacheCommand, DeleteCategoryFromCacheResponse>
    {
        private readonly ICategoriesCacheRepository _cacheRepository;
        private readonly ILogger _logger;

        public DeleteCategoryFromCacheCommandHandler(ICategoriesCacheRepository cacheRepository,
            ILogger logger)
        {
            _cacheRepository = cacheRepository;
            _logger = logger;
        }

        public async Task<DeleteCategoryFromCacheResponse> Handle(DeleteCategoryFromCacheCommand request, CancellationToken cancellationToken)
        {
            await _cacheRepository.DeleteCategoryAsync(request.CategoryId);

            _logger.Info($"Category #{request.CategoryId} deleted from the memory cache successfully");

            return new DeleteCategoryFromCacheResponse { CategoryId = request.CategoryId };
        }
    }
}