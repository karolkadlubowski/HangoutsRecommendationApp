using System.Threading.Tasks;
using AutoMapper;
using Library.Shared.Models.UserProfile;
using Moq;
using NUnit.Framework;
using UserProfile.API.Application.Abstractions;
using UserProfile.API.Application.Features.GetUserProfileQuery;

namespace UserProfile.API.Tests.Unit.Application.Features
{
    public class GetUserProfileCommandHandlerTests
    {
        private Mock<IUserProfileService> _userProfileService;
        private Mock<IMapper> _mapper;

        private GetUserProfileQueryHandler _getUserProfileQueryHandler;

        [SetUp]
        public void SetUp()
        {
            _userProfileService = new Mock<IUserProfileService>();
            _mapper = new Mock<IMapper>();
            
            _getUserProfileQueryHandler = new GetUserProfileQueryHandler(_userProfileService.Object, _mapper.Object);
        }

        [Test]
        public async Task Handle_WhenCalled_ShouldInvokeProperMethodsOnce()
        {
            const long ExpectedId = 1;
            const string ExpectedEmailAddress = "test@test.com";
            
            var userProfile = API.Domain.Entities.UserProfile.Create(ExpectedId, ExpectedEmailAddress);

            var query = new GetUserProfileQuery
            {
                UserId = ExpectedId
            };

            _userProfileService.Setup(x => x.GetUserProfileAsync(It.IsNotNull<GetUserProfileQuery>()))
                .ReturnsAsync(userProfile);

            //Act
            await _getUserProfileQueryHandler.Handle(query, default);

            //Assert
            _userProfileService.Verify(x => x.GetUserProfileAsync(query), Times.Once);
            _mapper.Verify(x => x.Map<UserProfileDto>(userProfile), Times.Once);
        }
    }
}