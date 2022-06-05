using System;
using FluentAssertions;
using Library.Shared.Exceptions;
using NUnit.Framework;
using VenueReview.API.Domain.ValueObjects;

namespace VenueReview.API.Tests.Unit.Domain.ValueObjects
{
    [TestFixture]
    public class ReviewRatingTests
    {
        [Test]
        [TestCase(-1)]
        [TestCase(-0.1)]
        [TestCase(5.1)]
        [TestCase(6)]
        public void Create_WhenRatingIsOutOfRange_ThrowValidationException(double rating)
        {
            //Arrange
            //Act
            Action act = () => new ReviewRating(rating);
            
            //Assert
            act.Should().Throw<ValidationException>();
        }

        [Test]
        [TestCase(0.01)]
        [TestCase(1.1)]
        [TestCase(4.99)]
        public void Create_WhenRatingIsInProperRange_ReturnReviewRating(double rating)
        {
            //Arrange
            //Act
            var result = new ReviewRating(rating).Value;
            
            //Assert
            result.Should().Be(rating);
        }
    }
}