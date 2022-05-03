using System;
using NUnit.Framework;
using UserProfile.API.Domain.ValueObjects;
using FluentAssertions;
using Library.Shared.Exceptions;

namespace UserProfile.API.Tests.Unit.Domain.ValueObjects
{
    [TestFixture]
    public class EmailAddressTests
    {
        [Test]
        [TestCase("@test.com")]
        [TestCase("test.com")]
        [TestCase("test@@test.com")]
        [TestCase("test@.com")]
        [TestCase("test@@.com")]
        public void Create_WhenEmailHasInvalidFormat_ThrowValidationException(string emailAddress)
        {
            //Arrange
            //Act
            Action act = () => new EmailAddress(emailAddress);

            //Assert
            act.Should().Throw<ValidationException>();
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Create_WhenEmailIsNullOrEmpty_ThrowValidationException(string emailAddress)
        {
            //Arrange
            //Act
            Action act = () => new EmailAddress(emailAddress);

            //Assert
            act.Should().Throw<ValidationException>();
        }
    }
}