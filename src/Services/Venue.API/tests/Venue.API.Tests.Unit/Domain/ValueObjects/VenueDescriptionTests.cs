using System;
using FluentAssertions;
using Library.Shared.Exceptions;
using NUnit.Framework;
using Venue.API.Domain.Validation;
using Venue.API.Domain.ValueObjects;
using Venue.API.Tests.Unit.Utilities.Factories;

namespace Venue.API.Tests.Unit.Domain.ValueObjects
{
    [TestFixture]
    public class VenueDescriptionTests
    {
        [Test]
        public void Create_WhenDescriptionIsNull_ReturnNull()
        {
            //Arrange
            string description = null;

            //Act
            var result = new VenueDescription(description).Value;

            //Assert
            result.Should().BeNull();
        }

        [Test]
        public void Create_WhenDescriptionIsNotNullAndLengthIsGreaterThanMaximumOne_ThrowValidationException()
        {
            //Arrange
            var description = StringFactory.CreateStringWithLength(ValidationRules.MaxVenueDescriptionLength + 1, 'X');

            //Act
            Action act = () => new VenueDescription(description);

            //Assert
            act.Should().Throw<ValidationException>();
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-ValidationRules.CategoryIdLength)]
        public void Create_WhenDescriptionIsNotNullAndLengthIsInTheProperRange_ReturnDescription(int offset)
        {
            //Arrange
            var description = StringFactory.CreateStringWithLength(ValidationRules.MaxVenueDescriptionLength + offset, 'X');

            //Act
            var result = new VenueDescription(description).Value;

            //Assert
            result.Should().Be(description);
        }
    }
}