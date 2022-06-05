using System;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Library.Shared.Exceptions;
using Library.Shared.Logging;
using Moq;
using NUnit.Framework;
using VenueReview.API.Application.Database.PersistenceModels;
using VenueReview.API.Application.Database.Repositories;
using VenueReview.API.Application.Features.AddVenueReview;
using VenueReview.API.Application.Features.DeleteVenueReview;
using VenueReview.API.Application.Services;
using VenueReview.API.Tests.Unit.Utilities.Models;

namespace VenueReview.API.Tests.Unit.Application.Services
{
    public class VenueReviewServiceTests
    {
        private Mock<IVenueReviewRepository> _venueReviewRepository;
        private Mock<IMapper> _mapper;
        private Mock<ILogger> _logger;

        private readonly static string _venueReviewId = Guid.NewGuid().ToString();
        private const long VenueId = 1;
        private const string VenueReviewContent = "CONTENT";
        private const long CreatorId = 2;
        private const double Rating = 1.11;

        private AddVenueReviewCommand _addVenueReviewCommand;
        private DeleteVenueReviewCommand _deleteVenueReviewCommand;

        private VenueReviewService _venueReviewService;

        [SetUp]
        public void SetUp()
        {
            _venueReviewRepository = new Mock<IVenueReviewRepository>();
            _mapper = new Mock<IMapper>();
            _logger = new Mock<ILogger>();

            _addVenueReviewCommand = new AddVenueReviewCommand {VenueId = VenueId, Content = VenueReviewContent, CreatorId = CreatorId, Rating = Rating};
            _deleteVenueReviewCommand = new DeleteVenueReviewCommand {VenueReviewId = _venueReviewId};

            _venueReviewService = new VenueReviewService(_venueReviewRepository.Object,
                _mapper.Object,
                _logger.Object);
        }

        #region AddVenueReviewAsync

        [Test]
        public async Task AddVenueReviewAsync_WhenVenueReviewCreatedByCertainUserAlreadyExistsInDatabase_ThrowDuplicateExistsException()
        {
            //Arrange
            _venueReviewRepository.Setup(x => x.AnyVenueReviewExistAsync(CreatorId, VenueId))
                .ReturnsAsync(true);

            //Act
            Func<Task> act = () => _venueReviewService.AddVenueReviewAsync(_addVenueReviewCommand);

            //Assert
            await act.Should().ThrowAsync<DuplicateExistsException>();
        }

        [Test]
        public async Task AddVenueReviewAsync_WhenInsertingCategoryToDatabaseFailed_ThrowDatabaseOperationException()
        {
            //Arrange
            _venueReviewRepository.Setup(x => x.AnyVenueReviewExistAsync(CreatorId, VenueId))
                .ReturnsAsync(false);
            _venueReviewRepository.Setup(x => x.InsertVenueReviewAsync(It.IsAny<API.Domain.Entities.VenueReview>()))
                .ReturnsAsync(() => null);

            //Act
            Func<Task> act = () => _venueReviewService.AddVenueReviewAsync(_addVenueReviewCommand);

            //Assert
            await act.Should().ThrowAsync<DatabaseOperationException>();
        }

        [Test]
        public async Task AddVenueReviewAsync_WhenVenueReviewInsertedToDatabaseSuccessfully_ReturnAddedVenueReview()
        {
            //Arrange
            var now = DateTime.Now;

            var venueReviewPersistenceModel = new VenueReviewPersistenceModel
            {
                VenueReviewId = _venueReviewId,
                VenueId = VenueId,
                Content = VenueReviewContent,
                CreatorId = CreatorId,
                Rating = Rating,
                CreatedOn = now
            };

            var expectedVenueReview = new StubVenueReview(
                venueReviewPersistenceModel.VenueReviewId,
                venueReviewPersistenceModel.VenueId,
                venueReviewPersistenceModel.Content,
                venueReviewPersistenceModel.CreatorId,
                venueReviewPersistenceModel.Rating,
                venueReviewPersistenceModel.CreatedOn
            );

            _venueReviewRepository.Setup(x => x.AnyVenueReviewExistAsync(CreatorId, VenueId))
                .ReturnsAsync(false);

            _venueReviewRepository.Setup(x => x.InsertVenueReviewAsync(It.IsAny<API.Domain.Entities.VenueReview>()))
                .ReturnsAsync(venueReviewPersistenceModel);

            _mapper.Setup(x => x.Map<VenueReviewPersistenceModel, API.Domain.Entities.VenueReview>(venueReviewPersistenceModel))
                .Returns(expectedVenueReview);

            //Act
            var result = await _venueReviewService.AddVenueReviewAsync(_addVenueReviewCommand);

            //Assert
            result.Should().BeEquivalentTo(expectedVenueReview);
        }

        #endregion

        #region DeleteVenueReviewAsync

        [Test]
        public async Task DeleteVenueReviewAsync_WhenVenueReviewNotFoundInDatabase_ThrowEntityNotFoundException()
        {
            //Arrange

            _venueReviewRepository.Setup(x => x.DeleteVenueReviewAsync(_venueReviewId))
                .ReturnsAsync(false);

            //Act
            Func<Task> act = () => _venueReviewService.DeleteVenueReviewAsync(_deleteVenueReviewCommand);

            //Assert
            await act.Should().ThrowAsync<EntityNotFoundException>();
        }

        [Test]
        public async Task DeleteVenueReviewAsync_WhenOtherExceptionThanEntityNotFoundExceptionOccurredDuringDeletingVenueReviewFromDatabase_ThrowDatabaseOperationException()
        {
            //Arrange
            _venueReviewRepository.Setup(x => x.DeleteVenueReviewAsync(_venueReviewId))
                .Throws<Exception>();

            //Act
            Func<Task> act = () => _venueReviewService.DeleteVenueReviewAsync(_deleteVenueReviewCommand);

            //Assert
            await act.Should().ThrowAsync<DatabaseOperationException>();
        }

        [Test]
        public async Task DeleteVenueReviewAsync_WhenVenueReviewDeletedFromDatabaseSuccessfully_ReturnDeletedVenueReviewId()
        {
            //Arrange
            _venueReviewRepository.Setup(x => x.DeleteVenueReviewAsync(_venueReviewId))
                .ReturnsAsync(true);

            //Act
            var result = await _venueReviewService.DeleteVenueReviewAsync(_deleteVenueReviewCommand);

            //Assert
            result.Should().Be(_venueReviewId);
        }

        #endregion
    }
}