using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Framework;
using Venue.API.Domain.Entities;

namespace Venue.API.Tests.Unit.Domain.Entities
{
    [TestFixture]
    public class LocationTests
    {
        #region Create

        [Test]
        public void Create_WhenCalled_ShouldSetAllDefaultProperties()
        {
            //Arrange
            const string Address = "Street 10";

            //Act
            var location = Location.Create(Address);

            //Assert
            using (new AssertionScope())
            {
                location.Address.Should().Be(Address);
                location.VenueId.Should().Be(default);
                location.LocationCoordinate.Should().BeNull();
            }
        }

        #endregion
    }
}