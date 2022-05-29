using System;
using AutoMapper;
using Library.Shared.Logging;
using Moq;
using VenueReview.API.Application.Database.Repositories;
using VenueReview.API.Application.Services;

namespace VenueReview.API.Tests.Unit.Application.Services
{
    public class VenueReviewServiceTests
    {
        private Mock<IVenueReviewRepository> _categoryRepository;
        private Mock<IMapper> _mapper;
        private Mock<ILogger> _logger;

        private readonly static string _venueReviewId = Guid.NewGuid().ToString();
        private const string VenueReviewContent = "CONTENT";

        // private AddVeCommand _addCategoryCommand;
        // private DeleteCategoryCommand _deleteCategoryCommand;

        private VenueReviewService _venueReviewService;
    }
}