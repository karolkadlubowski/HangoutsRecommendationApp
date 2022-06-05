using System;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using FluentAssertions.Execution;
using Library.EventBus;
using Library.Shared.Events.Abstractions;
using Library.Shared.Models.VenueReview.Dtos;
using Moq;
using NUnit.Framework;
using VenueReview.API.Application.Abstractions;
using VenueReview.API.Application.Features.AddVenueReview;

namespace VenueReview.API.Tests.Unit.Application.Features.AddVenueReview
{
    public class AddVenueReviewCommandHandlerTests
    {
        private Mock<IVenueReviewService> _venueReviewService;
        private Mock<IEventSender> _eventSender;
        private Mock<IMapper> _mapper;

        private AddVenueReviewCommandHandler _addVenueReviewCommandHandler;

        private readonly static string _venueReviewId = Guid.NewGuid().ToString();
        private const long VenueId = 1;
        private const string VenueReviewContent = "CONTENT";
        private const long CreatorId = 2;
        private const double Rating = 2.5;

        [SetUp]
        public void SetUp()
        {
            _venueReviewService = new Mock<IVenueReviewService>();
            _eventSender = new Mock<IEventSender>();
            _mapper = new Mock<IMapper>();

            _addVenueReviewCommandHandler = new AddVenueReviewCommandHandler(_venueReviewService.Object,
                _eventSender.Object,
                _mapper.Object);
        }

        [Test]
        public async Task Handle_WhenCalled_ReturnAddVenueReviewResponse()
        {
            //Arrange
            var venueReview = API.Domain.Entities.VenueReview.Create(
                VenueId,
                VenueReviewContent,
                CreatorId,
                Rating);
            var expectedVenueReview = new VenueReviewDto
            {
                VenueReviewId = _venueReviewId,
                VenueId = VenueId,
                Content = VenueReviewContent,
                CreatorId = CreatorId,
                Rating = Rating,
            };

            var command = new AddVenueReviewCommand
            {
                VenueId = VenueId,
                Content = VenueReviewContent,
                CreatorId = CreatorId,
                Rating = Rating
            };

            _venueReviewService.Setup(x => x.AddVenueReviewAsync(command))
                .ReturnsAsync(venueReview);
            _mapper.Setup(x => x.Map<VenueReviewDto>(venueReview))
                .Returns(expectedVenueReview);

            var expectedResponse = new AddVenueReviewResponse {AddedVenueReview = expectedVenueReview};

            //Act
            var result = await _addVenueReviewCommandHandler.Handle(command, default);

            //Assert
            result.Should().BeEquivalentTo(expectedResponse);
        }

        [Test]
        public async Task Handle_WhenCalled_ShouldInvokeProperMethodsOnce()
        {
            //Arrange

            var venueReview = API.Domain.Entities.VenueReview.Create(VenueId, VenueReviewContent, CreatorId, Rating);

            var command = new AddVenueReviewCommand()
            {
                VenueId = VenueId,
                Content = VenueReviewContent,
                CreatorId = CreatorId,
                Rating = Rating
            };

            _venueReviewService.Setup(x => x.AddVenueReviewAsync(It.IsNotNull<AddVenueReviewCommand>()))
                .ReturnsAsync(venueReview);

            //Act
            await _addVenueReviewCommandHandler.Handle(command, default);

            //Assert
            using (new AssertionScope())
            {
                _venueReviewService.Verify(x => x.AddVenueReviewAsync(command), Times.Once);
                _eventSender.Verify(x => x.SendEventAsync(EventBusTopics.VenueReview,
                    venueReview.FirstStoredEvent,
                    default), Times.Once);
                _mapper.Verify(x => x.Map<VenueReviewDto>(venueReview), Times.Once);
            }
        }
    }
}