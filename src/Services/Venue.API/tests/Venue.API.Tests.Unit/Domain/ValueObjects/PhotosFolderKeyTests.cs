using FluentAssertions;
using NUnit.Framework;
using Venue.API.Domain.ValueObjects;

namespace Venue.API.Tests.Unit.Domain.ValueObjects
{
    [TestFixture]
    public class PhotosFolderKeyTests
    {
        [Test]
        public void Create_WhenCalled_ReturnProperPhotosFolderKey()
        {
            //Arrange
            const long VenueId = 1;

            var expectedFolderKey = $"VENUES/{VenueId}";

            //Act
            var result = new PhotosFolderKey(VenueId).Value;

            //Assert
            result.Should().Be(expectedFolderKey);
        }
    }
}