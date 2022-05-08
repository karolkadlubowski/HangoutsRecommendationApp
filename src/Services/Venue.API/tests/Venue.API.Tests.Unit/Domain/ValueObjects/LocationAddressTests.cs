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
    public class LocationAddressTests
    {
        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Create_WhenAddressIsNullOrEmpty_ThrowValidationException(string address)
        {
            //Arrange
            //Act
            Action act = () => new LocationAddress(address);

            //Assert
            act.Should().Throw<ValidationException>();
        }

        [Test]
        public void Create_WhenAddressLengthIsGreaterThanMaximumLengthFromValidationRules_ThrowValidationException()
        {
            //Arrange
            var address = StringFactory.CreateStringWithLength(ValidationRules.MaxAddressLength + 1, 'X');

            //Act
            Action act = () => new LocationAddress(address);

            //Assert
            act.Should().Throw<ValidationException>();
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void Create_WhenAddressLengthIsInTheProperRange_ReturnName(int offset)
        {
            //Arrange
            var address = StringFactory.CreateStringWithLength(ValidationRules.MaxAddressLength + offset, 'X');

            //Act
            var result = new LocationAddress(address).Value;

            //Assert
            result.Should().Be(address);
        }
    }
}