using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using FluentAssertions.Execution;
using Library.EventBus;
using Library.Shared.Exceptions;
using Library.Shared.Logging;
using Library.Shared.Models.FileStorage.Dtos;
using Library.Shared.Models.Venue.Events;
using Moq;
using NUnit.Framework;
using Venue.API.Application.Abstractions;
using Venue.API.Application.Database;
using Venue.API.Application.Database.PersistenceModels;
using Venue.API.Application.Features.CreateVenue;
using Venue.API.Application.Features.GetVenue;
using Venue.API.Application.Services;
using Venue.API.Domain.Entities.Models;
using Venue.API.Domain.Validation;
using Venue.API.Tests.Unit.Utilities.Factories;
using Venue.API.Tests.Unit.Utilities.Stubs;

namespace Venue.API.Tests.Unit.Application.Services
{
    [TestFixture]
    public class VenueServiceTests
    {
        private Mock<IUnitOfWork> _unitOfWork;
        private Mock<IFileStorageDataService> _fileStorageDataService;
        private Mock<IMapper> _mapper;
        private Mock<ILogger> _logger;

        private GetVenueQuery _query;

        private VenueService _venueService;

        private const long VenueId = 1;

        [SetUp]
        public void SetUp()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _fileStorageDataService = new Mock<IFileStorageDataService>();
            _mapper = new Mock<IMapper>();
            _logger = new Mock<ILogger>();

            _query = new GetVenueQuery { VenueId = VenueId };

            _venueService = new VenueService(_unitOfWork.Object,
                _fileStorageDataService.Object,
                _mapper.Object,
                _logger.Object);
        }

        #region GetVenueWithPhotosAsync

        [Test]
        public async Task GetVenueWithPhotosAsync_WhenVenueNotFoundInDatabase_ThrowEntityNotFoundException()
        {
            //Arrange
            _unitOfWork.Setup(x => x.VenueRepository.FindByIdAsync(VenueId))
                .ReturnsAsync(() => null);

            //Act
            Func<Task> act = () => _venueService.GetVenueWithPhotosAsync(_query);

            //Assert
            await act.Should().ThrowAsync<EntityNotFoundException>();
        }

        [Test]
        public async Task GetVenueWithPhotosAsync_WhenVenueFoundInDatabase_ReturnVenueWithPhotosFetchedFromFileStorageApi()
        {
            //Arrange
            var venuePersistenceModel = new VenuePersistenceModel();
            var photos = PhotosFactory.Prepare(ValidationRules.MaxPhotosCount - 1).ToList();

            var expectedVenue = new StubVenue(VenueId, ImmutableList<Photo>.Empty);

            _unitOfWork.Setup(x => x.VenueRepository.FindByIdAsync(VenueId))
                .ReturnsAsync(venuePersistenceModel);
            _fileStorageDataService.Setup(x => x.GetPhotosFromFolderAsync(VenueId))
                .ReturnsAsync(new List<FileDto>());
            _mapper.Setup(x => x.Map<API.Domain.Entities.Venue>(venuePersistenceModel))
                .Returns(expectedVenue);
            _mapper.Setup(x => x.Map<IReadOnlyList<Photo>>(It.IsAny<IEnumerable<FileDto>>()))
                .Returns(photos);

            //Act
            var result = await _venueService.GetVenueWithPhotosAsync(_query);

            //Assert
            result.Should().BeEquivalentTo(expectedVenue);
        }

        #endregion

        #region CreateVenueAsync

        [Test]
        public async Task CreateVenueAsync_WhenCreatingVenueInDatabaseFailed_ThrowDatabaseOperationException()
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
            Func<Task> act = () => _venueService.CreateVenueAsync(command, Guid.NewGuid().ToString().Substring(0, 24), CreatorId);

            //Assert
            await act.Should().ThrowAsync<DatabaseOperationException>();
        }

        [Test]
        public async Task CreateVenueAsync_WhenCreatingVenueInDatabaseSucceeded_ReturnVenueWithVenueCreatedWithoutLocationEvent()
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
            var result = await _venueService.CreateVenueAsync(command, CategoryIdFactory.CategoryId, CreatorId);

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