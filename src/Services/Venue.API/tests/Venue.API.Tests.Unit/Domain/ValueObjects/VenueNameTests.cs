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
    public class VenueNameTests
    {
        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Create_WhenNameIsNullOrEmpty_ThrowValidationException(string name)
        {
            //Arrange
            //Act
            Action act = () => new VenueName(name);

            //Assert
            act.Should().Throw<ValidationException>();
        }

        [Test]
        public void Create_WhenNameLengthIsGreaterThanMaximumLengthFromValidationRules_ThrowValidationException()
        {
            //Arrange
            var name = StringFactory.CreateStringWithLength(ValidationRules.MaxVenueNameLength + 1, 'X');

            //Act
            Action act = () => new VenueName(name);

            //Assert
            act.Should().Throw<ValidationException>();
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void Create_WhenNameLengthIsInTheProperRange_ReturnName(int offset)
        {
            //Arrange
            var name = StringFactory.CreateStringWithLength(ValidationRules.MaxVenueNameLength + offset, 'X');

            //Act
            var result = new VenueName(name).Value;

            //Assert
            result.Should().Be(name);
        }
    }
}