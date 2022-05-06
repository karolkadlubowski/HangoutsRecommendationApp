using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Library.Database.Transaction.Abstractions;
using Library.EventBus;
using Library.Shared.Events.Abstractions;
using Library.Shared.Exceptions;
using Library.Shared.HttpAccessor;
using Library.Shared.Logging;
using Library.Shared.Models.Venue.Dtos;
using MediatR;
using Venue.API.Application.Abstractions;
using Venue.API.Domain.Entities.Models;

namespace Venue.API.Application.Features.CreateVenue
{
    public class CreateVenueCommandHandler : IRequestHandler<CreateVenueCommand, CreateVenueResponse>
    {
        private readonly IVenueService _venueService;
        private readonly ITransactionManager _transactionManager;
        private readonly ICategoriesCacheRepository _cacheRepository;
        private readonly IFileStorageDataService _fileStorageDataService;
        private readonly IEventSender _eventSender;
        private readonly IMapper _mapper;
        private readonly IReadOnlyHttpAccessor _httpAccessor;
        private readonly ILogger _logger;

        public CreateVenueCommandHandler(IVenueService venueService,
            ITransactionManager transactionManager,
            ICategoriesCacheRepository cacheRepository,
            IFileStorageDataService fileStorageDataService,
            IEventSender eventSender,
            IMapper mapper,
            IReadOnlyHttpAccessor httpAccessor,
            ILogger logger)
        {
            _venueService = venueService;
            _transactionManager = transactionManager;
            _cacheRepository = cacheRepository;
            _fileStorageDataService = fileStorageDataService;
            _eventSender = eventSender;
            _mapper = mapper;
            _httpAccessor = httpAccessor;
            _logger = logger;
        }

        public async Task<CreateVenueResponse> Handle(CreateVenueCommand request, CancellationToken cancellationToken)
        {
            using (var scope = _transactionManager.CreateScope())
            {
                _logger.Trace("> Database transaction began");

                var category = await _cacheRepository.FindCategoryByNameAsync(request.CategoryName)
                               ?? throw new EntityNotFoundException($"Category with name '{request.CategoryName}' does not exist");
                _logger.Trace($"Category #{category.CategoryId} with name '{category.Name}' found in the memory cache");

                var createdVenue = await _venueService.CreateVenueAsync(request,
                    category.CategoryId,
                    _httpAccessor.CurrentUserId);

                var createdVenueToReturn = _mapper.Map<VenueDto>(createdVenue);

                var uploadedPhotos = await _fileStorageDataService.UploadPhotosAsync(request.Photos, createdVenue.VenueId);
                createdVenue.AddPhotos(_mapper.Map<IEnumerable<Photo>>(uploadedPhotos));
                _logger.Info($"{uploadedPhotos.Count} photos uploaded to the storage successfully");

                await _eventSender.SendEventAsync(EventBusTopics.Venue, createdVenue.FirstStoredEvent,
                    cancellationToken);

                scope.Complete();

                _logger.Trace("< Database transaction committed");

                return new CreateVenueResponse { CreatedVenue = createdVenueToReturn };
            }
        }
    }
}