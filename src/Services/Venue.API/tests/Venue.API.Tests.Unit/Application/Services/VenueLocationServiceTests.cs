using System;
using System.Collections.Immutable;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using FluentAssertions.Execution;
using Library.EventBus;
using Library.Shared.Exceptions;
using Library.Shared.Logging;
using Library.Shared.Models.Venue.Events;
using Moq;
using NUnit.Framework;
using Venue.API.Application.Database;
using Venue.API.Application.Database.PersistenceModels;
using Venue.API.Application.Features.CreateVenue;
using Venue.API.Application.Services;
using Venue.API.Domain.Entities.Models;
using Venue.API.Tests.Unit.Utilities.Factories;
using Venue.API.Tests.Unit.Utilities.Models;

namespace Venue.API.Tests.Unit.Application.Services
{
    public class VenueLocationServiceTests
    {
        private Mock<IUnitOfWork> _unitOfWork;
        private Mock<IMapper> _mapper;
        private Mock<ILogger> _logger;

        private VenueLocationService _venueLocationService;

        private const long VenueId = 1;

        [SetUp]
        public void SetUp()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _mapper = new Mock<IMapper>();
            _logger = new Mock<ILogger>();

            _venueLocationService = new VenueLocationService(_unitOfWork.Object,
                _mapper.Object,
                _logger.Object);
        }

        #region CreateVenueWithoutLocationAsync

        [Test]
        public async Task CreateVenueWithoutLocationAsync_WhenCreatingVenueInDatabaseFailed_ThrowDatabaseOperationException()
        {
            //Arrange
            var command = new CreateVenueCommand
            {
                Name = "Name",
                CategoryName = "Category"
            };

            const long CreatorId = 1;
            var venuePersistenceModel = new VenuePersistenceModel();

            _mapper.Setup(x => x.Map<VenuePersistenceModel>(It.IsAny<API.Domain.Entities.Venue>()))
                .Returns(venuePersistenceModel);
            _unitOfWork.Setup(x => x.VenueRepository.Add(venuePersistenceModel));
            _unitOfWork.Setup(x => x.CompleteAsync())
                .ReturnsAsync(false);

            //Act
            Func<Task> act = () => _venueLocationService.CreateVenueWithoutLocationAsync(command, Guid.NewGuid().ToString().Substring(0, 24), CreatorId);

            //Assert
            await act.Should().ThrowAsync<DatabaseOperationException>();
        }

        [Test]
        public async Task CreateVenueWithoutLocationAsync_WhenCreatingVenueInDatabaseSucceeded_ReturnVenueWithVenueCreatedWithoutLocationEvent()
        {
            //Arrange
            var command = new CreateVenueCommand
            {
                Name = "Name",
                CategoryName = "Category"
            };

            const long CreatorId = 1;
            var venuePersistenceModel = new VenuePersistenceModel();

            var venue = new StubVenue(VenueId, ImmutableList<Photo>.Empty);
            venue.AddDomainEvent(new VenueCreatedWithoutLocationEvent());

            _mapper.Setup(x => x.Map<VenuePersistenceModel>(It.IsAny<API.Domain.Entities.Venue>()))
                .Returns(venuePersistenceModel);
            _unitOfWork.Setup(x => x.VenueRepository.Add(venuePersistenceModel));
            _unitOfWork.Setup(x => x.CompleteAsync())
                .ReturnsAsync(true);
            _mapper.Setup(x => x.Map<VenuePersistenceModel, API.Domain.Entities.Venue>(venuePersistenceModel))
                .Returns(venue);

            //Act
            var result = await _venueLocationService.CreateVenueWithoutLocationAsync(command, CategoryIdFactory.CategoryId, CreatorId);

            //Assert
            using (new AssertionScope())
            {
                result.Should().BeAssignableTo<API.Domain.Entities.Venue>();
                result.Should().NotBeNull();
                result.FirstStoredEvent.EventType.Should().Be(EventType.VENUE_CREATED_WITHOUT_LOCATION);
            }
        }

        #endregion
    }
}