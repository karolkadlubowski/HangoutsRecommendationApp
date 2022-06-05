using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Shared.Logging;
using VenueReview.API.Application.Database.Repositories;
using AutoMapper;
using Library.EventBus;
using Library.Shared.Exceptions;
using Library.Shared.Models.VenueReview.Events;
using Library.Shared.Models.VenueReview.Events.DataModels;
using VenueReview.API.Application.Abstractions;
using VenueReview.API.Application.Database.PersistenceModels;
using VenueReview.API.Application.Features;
using VenueReview.API.Application.Features.AddVenueReview;
using VenueReview.API.Application.Features.DeleteVenueReview;

namespace VenueReview.API.Application.Services
{
    public class VenueReviewService : IVenueReviewService
    {
        private readonly IVenueReviewRepository _venueReviewRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public VenueReviewService(IVenueReviewRepository venueReviewRepository,
            IMapper mapper,
            ILogger logger
        )
        {
            _venueReviewRepository = venueReviewRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IReadOnlyList<Domain.Entities.VenueReview>> GetVenueReviewsAsync(GetVenueReviewsQuery query)
        {
            var venueReviewsPersistenceModel = await _venueReviewRepository.GetVenueReviewsAsync(query.VenueId)
                                               ?? throw new EntityNotFoundException($"VenueReviews with id:  '{query.VenueId} not found in the database");

            var venueReviews = _mapper.Map<IReadOnlyList<Domain.Entities.VenueReview>>(venueReviewsPersistenceModel);

            _logger.Info($"{venueReviews.Count} venueReviews read from the database");

            return venueReviews;
        }

        public async Task<Domain.Entities.VenueReview> AddVenueReviewAsync(AddVenueReviewCommand command)
        {
            var venueReview = Domain.Entities.VenueReview.Create(command.VenueId, command.Content, command.CreatorId, command.Rating);

            if (await _venueReviewRepository.AnyVenueReviewExistAsync(command.CreatorId, command.VenueId))
                throw new DuplicateExistsException($"Review created by user with id '{venueReview.CreatorId}' for place with id '{venueReview.VenueId}' already exists in the database");

            var venueReviewPersistenceModel = await _venueReviewRepository.InsertVenueReviewAsync(venueReview)
                                              ?? throw new DatabaseOperationException($"Inserting venue review with VenueId '{venueReview.VenueId}' to the database failed");

            venueReview = _mapper.Map<VenueReviewPersistenceModel, Domain.Entities.VenueReview>(venueReviewPersistenceModel);

            _logger.Info($"Venue review with rating '{venueReview.Rating}' added to the database successfully");

            venueReview.AddDomainEvent(EventFactory<VenueReviewAddedEvent>.CreateEvent(venueReview.VenueReviewId,
                new VenueReviewAddedEventDataModel
                {
                    VenueReviewId = venueReview.VenueReviewId,
                    VenueId = venueReview.VenueId,
                    Content = venueReview.Content,
                    CreatorId = venueReview.CreatorId,
                    Rating = venueReview.Rating,
                    CreatedOn = venueReview.CreatedOn,
                    ModifiedOn = venueReview.ModifiedOn
                }));

            return venueReview;
        }

        public async Task<string> DeleteVenueReviewAsync(DeleteVenueReviewCommand command)
        {
            try
            {
                if (!await _venueReviewRepository.DeleteVenueReviewAsync(command.VenueReviewId))
                    throw new EntityNotFoundException($"Venue review #{command.VenueReviewId} was not deleted from the database because it was not found");

                _logger.Info($"Venue review #{command.VenueReviewId} deleted from the database successfully");

                return command.VenueReviewId;
            }
            catch (Exception e) when (e is not EntityNotFoundException)
            {
                throw new DatabaseOperationException($"Deleting VenueReview #{command.VenueReviewId} from the database failed. Exception: {e.Message}");
            }
        }
    }
}