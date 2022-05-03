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
    public class CategoryIdTests
    {
        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Create_WhenCategoryIdIsNullOrEmpty_ThrowValidationException(string categoryId)
        {
            //Arrange
            //Act
            Action act = () => new CategoryId(categoryId);

            //Assert
            act.Should().Throw<ValidationException>();
        }

        [Test]
        [TestCase(-1)]
        [TestCase(1)]
        public void Create_WhenCategoryIdLengthIsDifferentThanLengthFromValidationRules_ThrowValidationException(int offset)
        {
            //Arrange
            var categoryId = StringFactory.CreateStringWithLength(ValidationRules.CategoryIdLength + offset, 'X');

            //Act
            Action act = () => new CategoryId(categoryId);

            //Assert
            act.Should().Throw<ValidationException>();
        }

        [Test]
        public void Create_WhenCategoryIdLengthIsEqualToCategoryIdLengthFromValidationRules_ReturnCategoryId()
        {
            //Arrange
            var categoryId = StringFactory.CreateStringWithLength(ValidationRules.CategoryIdLength, 'X');

            //Act
            var result = new CategoryId(categoryId).Value;

            //Assert
            result.Should().Be(categoryId);
        }
    }
}