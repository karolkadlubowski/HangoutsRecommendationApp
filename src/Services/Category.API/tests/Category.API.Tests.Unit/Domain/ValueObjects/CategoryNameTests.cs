using System;
using Category.API.Domain.Validation;
using Category.API.Domain.ValueObjects;
using Category.API.Tests.Unit.Utilities.Factories;
using FluentAssertions;
using Library.Shared.Exceptions;
using NUnit.Framework;

namespace Category.API.Tests.Unit.Domain.ValueObjects
{
    [TestFixture]
    public class CategoryNameTests
    {
        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Create_WhenNameIsNullOrEmpty_ThrowValidationException(string name)
        {
            //Arrange
            //Act
            Action act = () => new CategoryName(name);

            //Assert
            act.Should().Throw<ValidationException>();
        }

        [Test]
        public void Create_WhenNameIsLongerThanMaximumNameLength_ThrowValidationException()
        {
            //Arrange
            var name = StringFactory.CreateStringWithLength(ValidationRules.MaxNameLength + 1, 'X');

            //Act
            Action act = () => new CategoryName(name);

            //Assert
            act.Should().Throw<ValidationException>();
        }

        [Test]
        public void Create_WhenNameLengthIsEqualToMaximum_ReturnName()
        {
            //Arrange
            var expectedName = StringFactory.CreateStringWithLength(ValidationRules.MaxNameLength, 'X');

            //Act
            var result = new CategoryName(expectedName).Value;

            //Assert
            result.Should().Be(expectedName);
        }

        [Test]
        [TestCase(" Category ")]
        [TestCase(" Category")]
        [TestCase("Category ")]
        [TestCase("Category")]
        public void Create_WhenCalled_ReturnTrimmedAndUpperCaseName(string name)
        {
            //Arrange
            const string ExpectedName = "CATEGORY";

            //Act
            var result = new CategoryName(name).Value;

            //Assert
            result.Should().Be(ExpectedName);
        }
    }
}