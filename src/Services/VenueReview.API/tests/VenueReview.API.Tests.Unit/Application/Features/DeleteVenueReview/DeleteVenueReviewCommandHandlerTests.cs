using System;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Execution;
using Library.EventBus;
using Library.Shared.Events.Abstractions;
using Library.Shared.Models.VenueReview.Events;
using Moq;
using NUnit.Framework;
using VenueReview.API.Application.Abstractions;
using VenueReview.API.Application.Features.DeleteVenueReview;

namespace VenueReview.API.Tests.Unit.Application.Features.DeleteVenueReview
{
    [TestFixture]
    public class DeleteVenueReviewCommandHandlerTests
    {
        private Mock<IVenueReviewService> _venueReviewService;
        private Mock<IEventSender> _eventSender;

        private DeleteVenueReviewCommandHandler _deleteVenueReviewCommandHandler;

        [SetUp]
        public void SetUp()
        {
            _venueReviewService = new Mock<IVenueReviewService>();
            _eventSender = new Mock<IEventSender>();

            _deleteVenueReviewCommandHandler = new DeleteVenueReviewCommandHandler(_venueReviewService.Object,
                _eventSender.Object);
        }

        [Test]
        public async Task Handle_WhenCalled_ReturnVenueReviewCategoryResponse()
        {
            //Arrange
            var venueReviewId = Guid.NewGuid().ToString();

            var command = new DeleteVenueReviewCommand {VenueReviewId = venueReviewId};

            _venueReviewService.Setup(x => x.DeleteVenueReviewAsync(command))
                .ReturnsAsync(venueReviewId);

            var expectedResponse = new DeleteVenueReviewResponse {DeletedVenueReviewId = venueReviewId};

            //Act
            var result = await _deleteVenueReviewCommandHandler.Handle(command, default);

            //Assert
            result.Should().BeEquivalentTo(expectedResponse);
        }

        [Test]
        public async Task Handle_WhenCalled_ShouldInvokeProperMethodsOnce()
        {
            //Arrange
            var venueReviewId = Guid.NewGuid().ToString();

            var command = new DeleteVenueReviewCommand {VenueReviewId = venueReviewId};

            _venueReviewService.Setup(x => x.DeleteVenueReviewAsync(command))
                .ReturnsAsync(venueReviewId);

            //Act
            await _deleteVenueReviewCommandHandler.Handle(command, default);

            //Assert
            using (new AssertionScope())
            {
                _venueReviewService.Verify(x => x.DeleteVenueReviewAsync(command), Times.Once);
                _eventSender.Verify(x => x.SendEventAsync(EventBusTopics.VenueReview,
                    It.IsNotNull<VenueReviewDeletedEvent>(),
                    default), Times.Once);
            }
        }
    }
}