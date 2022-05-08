using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Framework;
using Venue.API.Domain.Entities;

namespace Venue.API.Tests.Unit.Domain.Entities
{
    [TestFixture]
    public class LocationCoordinateTests
    {
        #region Create

        [Test]
        public void Create_WhenCalled_ShouldSetAllDefaultProperties()
        {
            //Arrange
            const long LocationId = 1;
            const double Latitude = 100.0;
            const double Longitude = 200.5;

            //Act
            var locationCoordinate = LocationCoordinate.Create(LocationId, Latitude, Longitude);

            //Assert
            using (new AssertionScope())
            {
                locationCoordinate.LocationId.Should().Be(LocationId);
                locationCoordinate.Latitude.Should().Be(Latitude);
                locationCoordinate.Longitude.Should().Be(Longitude);
            }
        }

        #endregion
    }
}