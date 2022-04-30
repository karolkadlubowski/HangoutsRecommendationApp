using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Library.EventBus.Abstractions;
using Library.Shared.Models.UserProfile;
using Moq;
using NUnit.Framework;
using UserProfile.API.Application.Abstractions;
using UserProfile.API.Application.Handlers.UpdateEmailAddress;

namespace UserProfile.API.Tests.Unit.Application.Handlers
{
    public class UpdateEmailAddressCommandHandlerTests
    {
        private Mock<IUserProfileService> _userProfileService;

        private UpdateEmailAddressCommandHandler _updateEmailAddressCommandHandler;

        [SetUp]
        public void SetUp()
        {
            _userProfileService = new Mock<IUserProfileService>();
            _updateEmailAddressCommandHandler = new UpdateEmailAddressCommandHandler(_userProfileService.Object);
        }

        [Test]
        public async Task Handle_WhenCalled_ReturnUpdatedEmailAdressResponse()
        {
            //Arrange
            const long ExpectedId = 1337;
            const string ExpectedEmailAddress = "FilipToKoozak@gmail.com";
            var userProfile = API.Domain.Entities.UserProfile.Create(ExpectedId, ExpectedEmailAddress);
            var command = new UpdateEmailAddressCommand(ExpectedId, ExpectedEmailAddress);
            _userProfileService.Setup(x => x.UpdateUserProfileAsync(command))
                .ReturnsAsync(userProfile);
            var expectedResponse = new UpdateEmailAddressResponse
            {
                UserId = ExpectedId,
                CurrentEmailAddress = ExpectedEmailAddress
            };

            //Act
            var result = await _updateEmailAddressCommandHandler.Handle(command, default);
            
            //Assert
            result.Should().BeEquivalentTo(expectedResponse);
        }

        [Test]
        public async Task Handle_WhenCalled_ShouldInvokeProperMethodsOnce()
        {
            //Arrange
            const long userId = 1337;
            const string emailAddress = "FilipToKoozak@gmail.com";

            var userProfile = API.Domain.Entities.UserProfile.Create(userId,emailAddress );
            var command = new UpdateEmailAddressCommand(userId, emailAddress);

            _userProfileService.Setup(x => x.UpdateUserProfileAsync(It.IsNotNull<UpdateEmailAddressCommand>()))
                .ReturnsAsync(userProfile);
            
            //Act
            await _updateEmailAddressCommandHandler.Handle(command, default);
            
            //Assert
            _userProfileService.Verify(x => x.UpdateUserProfileAsync(command),Times.Once);
        }
    }
}