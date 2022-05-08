using System;
using System.Linq;
using FluentAssertions;
using FluentAssertions.Execution;
using Library.Shared.Exceptions;
using Library.Shared.Models.Venue.Enums;
using NUnit.Framework;
using Venue.API.Domain.Validation;
using Venue.API.Tests.Unit.Utilities.Factories;

namespace Venue.API.Tests.Unit.Domain.Entities
{
    [TestFixture]
    public class VenueTests
    {
        #region CreateDefault

        [Test]
        public void CreateDefault_WhenCalled_ShouldSetAllDefaultProperties()
        {
            //Arrange
            const string Name = nameof(Name);
            var categoryId = StringFactory.CreateStringWithLength(24, 'x');

            //Act
            var venue = API.Domain.Entities.Venue.CreateDefault(Name, categoryId);

            //Assert
            using (new AssertionScope())
            {
                venue.Name.Should().Be(Name);
                venue.CategoryId.Should().Be(categoryId);
                venue.Description.Should().BeNull();
                venue.CreatorId.Should().BeNull();
                venue.Status.Should().Be(VenueStatus.Created);
                venue.Location.Should().BeNull();
            }
        }

        #endregion

        #region Accept

        [Test]
        public void Accept_WhenCalled_ShouldSetStatusToAccepted()
        {
            //Arrange
            var venue = new API.Domain.Entities.Venue();

            //Act
            venue.Accept();

            //Assert
            venue.Status.Should().Be(VenueStatus.Accepted);
        }

        #endregion

        #region AddPhotos

        [Test]
        public void AddPhotos_WhenPhotosCountExceedMaxPhotosCount_ThrowValidationException()
        {
            //Arrange
            var venue = new API.Domain.Entities.Venue();
            var photos = PhotosFactory.Prepare(ValidationRules.MaxPhotosCount + 1);

            //Act
            Action act = () => venue.AddPhotos(photos);

            //Assert
            act.Should().Throw<ValidationException>();
        }

        [Test]
        public void AddPhotos_WhenPhotosCountIsLesserThanMaximumCount_ShouldAddPhotos()
        {
            //Arrange
            const int ExpectedCount = 3;

            var venue = new API.Domain.Entities.Venue();

            var photo = PhotosFactory.Prepare(1).First();
            var photos = PhotosFactory.Prepare(2, offset: 1);

            venue.Photos.Add(photo);

            //Act
            venue.AddPhotos(photos);

            //Assert
            venue.Photos.Should().HaveCount(ExpectedCount);
        }

        [Test]
        public void AddPhotos_WhenPhotosCountIsEqualToMaximumCount_ShouldAddPhotos()
        {
            //Arrange
            const int ExpectedCount = ValidationRules.MaxPhotosCount;

            var venue = new API.Domain.Entities.Venue();

            var photo = PhotosFactory.Prepare(1).First();
            var photos = PhotosFactory.Prepare(5, offset: 1);

            venue.Photos.Add(photo);

            //Act
            venue.AddPhotos(photos);

            //Assert
            venue.Photos.Should().HaveCount(ExpectedCount);
        }

        #endregion
    }
}