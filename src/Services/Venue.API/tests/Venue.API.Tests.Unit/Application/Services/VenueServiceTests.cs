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
using Venue.API.Application.Features.DeleteVenue;
using Venue.API.Application.Features.UpdateVenue;
using Venue.API.Application.Services;
using Venue.API.Domain.Entities.Models;
using Venue.API.Tests.Unit.Utilities.Factories;
using Venue.API.Tests.Unit.Utilities.Models;

namespace Venue.API.Tests.Unit.Application.Services
{
    public class VenueServiceTests
    {
        private Mock<IUnitOfWork> _unitOfWork;
        private Mock<IMapper> _mapper;
        private Mock<ILogger> _logger;

        private VenueService _venueService;

        private const long VenueId = 1;

        [SetUp]
        public void SetUp()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _mapper = new Mock<IMapper>();
            _logger = new Mock<ILogger>();

            _venueService = new VenueService(_unitOfWork.Object,
                _mapper.Object,
                _logger.Object);
        }

        #region CreateVenueAsync

        [Test]
        public async Task CreateVenueAsync_WhenCreatingVenueInDatabaseFailed_ThrowDatabaseOperationException()
        {
            //Arrange
            var command = new CreateVenueCommand
            {
                VenueName = "Name",
                CategoryName = "Category",
                Address = "Address"
            };

            const long CreatorId = 1;
            var venuePersistenceModel = new VenuePersistenceModel { Location = new LocationPersistenceModel() };

            _mapper.Setup(x => x.Map<VenuePersistenceModel>(It.IsAny<API.Domain.Entities.Venue>()))
                .Returns(venuePersistenceModel);
            _unitOfWork.Setup(x => x.VenueRepository.Add(venuePersistenceModel));
            _unitOfWork.Setup(x => x.CompleteAsync())
                .ReturnsAsync(false);

            //Act
            Func<Task> act = () => _venueService.CreateVenueAsync(command, CategoryIdFactory.CategoryId, CreatorId);

            //Assert
            await act.Should().ThrowAsync<DatabaseOperationException>();
        }

        [Test]
        public async Task CreateVenueAsync_WhenCreatingVenueInDatabaseSucceeded_ReturnVenueWithVenueCreatedWithoutLocationEvent()
        {
            //Arrange
            var command = new CreateVenueCommand
            {
                VenueName = "Name",
                CategoryName = "Category",
                Address = "Address"
            };

            const long CreatorId = 1;
            var venuePersistenceModel = new VenuePersistenceModel { Location = new LocationPersistenceModel() };

            var venue = new StubVenue(VenueId, ImmutableList<Photo>.Empty);
            venue.AddDomainEvent(new VenueCreatedEvent());

            _mapper.Setup(x => x.Map<VenuePersistenceModel>(It.IsAny<API.Domain.Entities.Venue>()))
                .Returns(venuePersistenceModel);
            _unitOfWork.Setup(x => x.VenueRepository.Add(venuePersistenceModel));
            _unitOfWork.Setup(x => x.CompleteAsync())
                .ReturnsAsync(true);
            _mapper.Setup(x => x.Map<API.Domain.Entities.Venue>(venuePersistenceModel))
                .Returns(venue);

            //Act
            var result = await _venueService.CreateVenueAsync(command, CategoryIdFactory.CategoryId, CreatorId);

            //Assert
            using (new AssertionScope())
            {
                result.Should().BeAssignableTo<API.Domain.Entities.Venue>();
                result.Should().NotBeNull();
                result.FirstStoredEvent.EventType.Should().Be(EventType.VENUE_CREATED);
            }
        }

        #endregion

        #region UpdateVenueAsync

        [Test]
        public async Task UpdateVenueAsync_WhenVenueToUpdateNotFoundInDatabase_ThrowEntityNotFoundException()
        {
            //Arrange
            var command = new UpdateVenueCommand
            {
                VenueId = VenueId,
                VenueName = "Name",
                CategoryName = "Category",
                Address = "Address"
            };

            const long CreatorId = 1;

            _unitOfWork.Setup(x => x.VenueRepository.FindVenueWithDetailsAsync(VenueId))
                .ReturnsAsync(() => null);

            //Act
            Func<Task> act = () => _venueService.UpdateVenueAsync(command, CategoryIdFactory.CategoryId, CreatorId);

            //Assert
            await act.Should().ThrowAsync<EntityNotFoundException>();
        }

        [Test]
        public async Task UpdateVenueAsync_WhenVenueUpdatingInDatabaseFailed_ThrowDatabaseOperationException()
        {
            //Arrange
            const double Coordinate = 100.0;

            var command = new UpdateVenueCommand
            {
                VenueId = VenueId,
                VenueName = "Name",
                CategoryName = "Category",
                Address = "Address"
            };

            const long CreatorId = 1;

            var venuePersistenceModel = new VenuePersistenceModel
            {
                CreatorId = CreatorId,
                Location = new LocationPersistenceModel()
            };

            var venue = new StubVenue(VenueId, ImmutableList<Photo>.Empty);
            venue.InitLocation(Coordinate, Coordinate);
            venue.CreatedBy(CreatorId);

            _unitOfWork.Setup(x => x.VenueRepository.FindVenueWithDetailsAsync(VenueId))
                .ReturnsAsync(venuePersistenceModel);
            _mapper.Setup(x => x.Map<API.Domain.Entities.Venue>(venuePersistenceModel))
                .Returns(venue);
            _mapper.Setup(x => x.Map(It.IsAny<API.Domain.Entities.Venue>(), venuePersistenceModel))
                .Returns(venuePersistenceModel);
            _unitOfWork.Setup(x => x.CompleteAsync())
                .ReturnsAsync(false);

            //Act
            Func<Task> act = () => _venueService.UpdateVenueAsync(command, CategoryIdFactory.CategoryId, CreatorId);

            //Assert
            await act.Should().ThrowAsync<DatabaseOperationException>();
        }

        [Test]
        public async Task UpdateVenueAsync_WhenVenueUpdatedInDatabase_ReturnVenueWithProperDomainEvent()
        {
            //Arrange
            const double Coordinate = 100.0;

            var command = new UpdateVenueCommand
            {
                VenueId = VenueId,
                VenueName = "Name",
                CategoryName = "Category",
                Address = "Address"
            };

            const long CreatorId = 1;

            var venuePersistenceModel = new VenuePersistenceModel
            {
                CreatorId = CreatorId,
                Location = new LocationPersistenceModel()
            };

            var venue = new StubVenue(VenueId, ImmutableList<Photo>.Empty);
            venue.InitLocation(Coordinate, Coordinate);
            venue.CreatedBy(CreatorId);
            venue.AddDomainEvent(new VenueUpdatedEvent());

            _unitOfWork.Setup(x => x.VenueRepository.FindVenueWithDetailsAsync(VenueId))
                .ReturnsAsync(venuePersistenceModel);
            _mapper.Setup(x => x.Map<API.Domain.Entities.Venue>(venuePersistenceModel))
                .Returns(venue);
            _mapper.Setup(x => x.Map(It.IsAny<API.Domain.Entities.Venue>(), venuePersistenceModel))
                .Returns(venuePersistenceModel);
            _unitOfWork.Setup(x => x.CompleteAsync())
                .ReturnsAsync(true);

            //Act
            var result = await _venueService.UpdateVenueAsync(command, CategoryIdFactory.CategoryId, CreatorId);

            //Assert
            using (new AssertionScope())
            {
                result.Should().BeAssignableTo<API.Domain.Entities.Venue>();
                result.Should().NotBeNull();
                result.FirstStoredEvent.EventType.Should().Be(EventType.VENUE_UPDATED);
            }
        }

        #endregion

        #region DeleteVenueAsync

        [Test]
        public async Task DeleteVenueAsync_WhenVenueToUpdateNotFoundInDatabase_ThrowEntityNotFoundException()
        {
            //Arrange
            var command = new DeleteVenueCommand
            {
                VenueId = VenueId
            };

            _unitOfWork.Setup(x => x.VenueRepository.FindVenueWithDetailsAsync(VenueId))
                .ReturnsAsync(() => null);

            //Act
            Func<Task> act = () => _venueService.DeleteVenueAsync(command);

            //Assert
            await act.Should().ThrowAsync<EntityNotFoundException>();
        }

        [Test]
        public async Task DeleteVenueAsync_WhenVenueDeletingFromDatabaseFailed_ThrowDatabaseOperationException()
        {
            //Arrange
            const double Coordinate = 100.0;

            var command = new DeleteVenueCommand
            {
                VenueId = VenueId
            };

            var venuePersistenceModel = new VenuePersistenceModel
            {
                Location = new LocationPersistenceModel()
            };

            var venue = new StubVenue(VenueId, ImmutableList<Photo>.Empty);
            venue.InitLocation(Coordinate, Coordinate);

            _unitOfWork.Setup(x => x.VenueRepository.FindVenueWithDetailsAsync(VenueId))
                .ReturnsAsync(venuePersistenceModel);
            _mapper.Setup(x => x.Map<API.Domain.Entities.Venue>(venuePersistenceModel))
                .Returns(venue);
            _mapper.Setup(x => x.Map(It.IsAny<API.Domain.Entities.Venue>(), venuePersistenceModel))
                .Returns(venuePersistenceModel);
            _unitOfWork.Setup(x => x.CompleteAsync())
                .ReturnsAsync(false);

            //Act
            Func<Task> act = () => _venueService.DeleteVenueAsync(command);

            //Assert
            await act.Should().ThrowAsync<DatabaseOperationException>();
        }

        [Test]
        public async Task DeleteVenueAsync_WhenVenueDeletedFromDatabase_ReturnVenueWithProperDomainEvent()
        {
            //Arrange
            const double Coordinate = 100.0;

            var command = new DeleteVenueCommand
            {
                VenueId = VenueId
            };

            var venuePersistenceModel = new VenuePersistenceModel
            {
                Location = new LocationPersistenceModel()
            };

            var venue = new StubVenue(VenueId, ImmutableList<Photo>.Empty);
            venue.InitLocation(Coordinate, Coordinate);
            venue.AddDomainEvent(new VenueDeletedEvent());

            _unitOfWork.Setup(x => x.VenueRepository.FindVenueWithDetailsAsync(VenueId))
                .ReturnsAsync(venuePersistenceModel);
            _mapper.Setup(x => x.Map<API.Domain.Entities.Venue>(venuePersistenceModel))
                .Returns(venue);
            _unitOfWork.Setup(x => x.CompleteAsync())
                .ReturnsAsync(true);

            //Act
            var result = await _venueService.DeleteVenueAsync(command);

            //Assert
            using (new AssertionScope())
            {
                result.Should().BeAssignableTo<API.Domain.Entities.Venue>();
                result.Should().NotBeNull();
                result.FirstStoredEvent.EventType.Should().Be(EventType.VENUE_DELETED);
            }
        }

        #endregion
    }
}