using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Library.Shared.Exceptions;
using Library.Shared.Logging;
using Library.Shared.Models.FileStorage.Dtos;
using Moq;
using NUnit.Framework;
using Venue.API.Application.Abstractions;
using Venue.API.Application.Database;
using Venue.API.Application.Database.PersistenceModels;
using Venue.API.Application.Features.GetVenue;
using Venue.API.Application.Services;
using Venue.API.Domain.Entities.Models;
using Venue.API.Domain.Validation;
using Venue.API.Tests.Unit.Utilities.Factories;
using Venue.API.Tests.Unit.Utilities.Models;

namespace Venue.API.Tests.Unit.Application.Services
{
    [TestFixture]
    public class ReadOnlyVenueServiceTests
    {
        private Mock<IUnitOfWork> _unitOfWork;
        private Mock<IFileStorageDataService> _fileStorageDataService;
        private Mock<IMapper> _mapper;
        private Mock<ILogger> _logger;

        private GetVenueQuery _query;

        private ReadOnlyVenueService _readOnlyVenueService;

        private const long VenueId = 1;

        [SetUp]
        public void SetUp()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _fileStorageDataService = new Mock<IFileStorageDataService>();
            _mapper = new Mock<IMapper>();
            _logger = new Mock<ILogger>();

            _query = new GetVenueQuery { VenueId = VenueId };

            _readOnlyVenueService = new ReadOnlyVenueService(_unitOfWork.Object,
                _fileStorageDataService.Object,
                _mapper.Object,
                _logger.Object);
        }

        #region GetVenueWithPhotosAsync

        [Test]
        public async Task GetVenueWithPhotosAsync_WhenVenueNotFoundInDatabase_ThrowEntityNotFoundException()
        {
            //Arrange
            _unitOfWork.Setup(x => x.VenueRepository.FindVenueWithDetailsAsync(VenueId))
                .ReturnsAsync(() => null);

            //Act
            Func<Task> act = () => _readOnlyVenueService.GetVenueWithPhotosAsync(_query);

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

            _unitOfWork.Setup(x => x.VenueRepository.FindVenueWithDetailsAsync(VenueId))
                .ReturnsAsync(venuePersistenceModel);
            _fileStorageDataService.Setup(x => x.GetPhotosFromFolderAsync(VenueId))
                .ReturnsAsync(new List<FileDto>());
            _mapper.Setup(x => x.Map<API.Domain.Entities.Venue>(venuePersistenceModel))
                .Returns(expectedVenue);
            _mapper.Setup(x => x.Map<IReadOnlyList<Photo>>(It.IsAny<IEnumerable<FileDto>>()))
                .Returns(photos);

            //Act
            var result = await _readOnlyVenueService.GetVenueWithPhotosAsync(_query);

            //Assert
            result.Should().BeEquivalentTo(expectedVenue);
        }

        #endregion
    }
}