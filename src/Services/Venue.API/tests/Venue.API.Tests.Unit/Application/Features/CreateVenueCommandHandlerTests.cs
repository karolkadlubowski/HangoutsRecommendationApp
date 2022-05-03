using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using AutoMapper;
using FluentAssertions;
using FluentAssertions.Execution;
using Library.Database.Transaction;
using Library.Database.Transaction.Abstractions;
using Library.EventBus;
using Library.Shared.Events.Abstractions;
using Library.Shared.Exceptions;
using Library.Shared.HttpAccessor;
using Library.Shared.Logging;
using Library.Shared.Models.Category.Dtos;
using Library.Shared.Models.FileStorage.Dtos;
using Library.Shared.Models.Venue.Dtos;
using Library.Shared.Models.Venue.Events;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using Venue.API.Application.Abstractions;
using Venue.API.Application.Features.CreateVenue;
using Venue.API.Tests.Unit.Utilities.Factories;

namespace Venue.API.Tests.Unit.Application.Features
{
    [TestFixture]
    public class CreateVenueCommandHandlerTests
    {
        private Mock<IVenueService> _venueService;
        private Mock<ITransactionManager> _transactionManager;
        private Mock<ICategoriesCacheRepository> _cacheRepository;
        private Mock<IFileStorageDataService> _fileStorageDataService;
        private Mock<IEventSender> _eventSender;
        private Mock<IMapper> _mapper;
        private Mock<IReadOnlyHttpAccessor> _httpAccessor;
        private Mock<ILogger> _logger;

        private const string Name = nameof(Name);
        private const string CategoryName = nameof(CategoryName);
        private const string CategoryDescription = nameof(CategoryDescription);

        private CreateVenueCommand _command;

        private CreateVenueCommandHandler _createVenueCommandHandler;

        [SetUp]
        public void SetUp()
        {
            _venueService = new Mock<IVenueService>();
            _transactionManager = new Mock<ITransactionManager>();
            _cacheRepository = new Mock<ICategoriesCacheRepository>();
            _fileStorageDataService = new Mock<IFileStorageDataService>();
            _eventSender = new Mock<IEventSender>();
            _mapper = new Mock<IMapper>();
            _httpAccessor = new Mock<IReadOnlyHttpAccessor>();
            _logger = new Mock<ILogger>();

            _command = new CreateVenueCommand
            {
                Name = Name,
                CategoryName = CategoryName,
                Description = CategoryDescription
            };

            _createVenueCommandHandler = new CreateVenueCommandHandler(_venueService.Object, _transactionManager.Object, _cacheRepository.Object, _fileStorageDataService.Object, _eventSender.Object,
                _mapper.Object, _httpAccessor.Object, _logger.Object
            );
        }

        [Test]
        public async Task Handle_WhenCategoryNotFoundInCache_ThrowEntityNotFoundException()
        {
            //Arrange
            _transactionManager.Setup(x => x.CreateScope(TransactionScopeOption.Required))
                .Returns(new DefaultTransactionScope(new TransactionScope()));
            _cacheRepository.Setup(x => x.FindCategoryByNameAsync(CategoryName))
                .ReturnsAsync(() => null);

            //Act
            Func<Task> act = () => _createVenueCommandHandler.Handle(_command, default);

            //Assert
            await act.Should().ThrowAsync<EntityNotFoundException>();
        }

        [Test]
        public async Task Handle_WhenCalled_ShouldInvokeProperMethods()
        {
            //Arrange
            var categoryId = CategoryIdFactory.CategoryId;
            const long CreatorId = 1;
            const long LocationId = 10;

            _transactionManager.Setup(x => x.CreateScope(TransactionScopeOption.Required))
                .Returns(new DefaultTransactionScope(new TransactionScope()));
            _cacheRepository.Setup(x => x.FindCategoryByNameAsync(CategoryName))
                .ReturnsAsync(() => new CategoryDto
                {
                    CategoryId = categoryId,
                    Name = CategoryName
                });
            _venueService.Setup(x => x.CreateVenueAsync(_command, categoryId, CreatorId))
                .ReturnsAsync(API.Domain.Entities.Venue.CreateDefault(Name, LocationId, categoryId));
            _httpAccessor.Setup(x => x.CurrentUserId)
                .Returns(CreatorId);
            _mapper.Setup(x => x.Map<VenueDto>(It.IsAny<API.Domain.Entities.Venue>()))
                .Returns(new VenueDto());
            _fileStorageDataService.Setup(x => x.UploadPhotosAsync(new List<IFormFile>(), It.IsAny<long>()))
                .ReturnsAsync(new List<FileDto>());

            //Act
            var result = await _createVenueCommandHandler.Handle(_command, default);

            //Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<CreateVenueResponse>();
                result.Should().NotBeNull();

                _transactionManager.Verify(x => x.CreateScope(TransactionScopeOption.Required), Times.Once);
                _cacheRepository.Verify(x => x.FindCategoryByNameAsync(It.IsAny<string>()), Times.Once);
                _mapper.Verify(x => x.Map<VenueDto>(It.IsAny<API.Domain.Entities.Venue>()), Times.Once);
                _fileStorageDataService.Verify(x => x.UploadPhotosAsync(It.IsAny<List<IFormFile>>(), It.IsAny<long>()), Times.Once);
                _eventSender.Verify(x => x.SendEventAsync(EventBusTopics.Venue, It.IsAny<VenueCreatedWithoutLocationEvent>(), It.IsAny<CancellationToken>()), Times.Once);
            }
        }
    }
}