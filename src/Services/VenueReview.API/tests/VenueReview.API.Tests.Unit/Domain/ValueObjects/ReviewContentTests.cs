using System;
using FluentAssertions;
using Library.Shared.Exceptions;
using NUnit.Framework;
using VenueReview.API.Domain.ValueObjects;

namespace VenueReview.API.Tests.Unit.Domain.ValueObjects
{
    [TestFixture]
    public class ReviewContentTests
    {
        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Create_WhenContentIsNullOrEmpty_ThrowValidationException(string content)
        {
            //Arrange
            //Act
            Action act = () => new ReviewContent(content);

            //Assert
            act.Should().Throw<ValidationException>();
        }
    }
}