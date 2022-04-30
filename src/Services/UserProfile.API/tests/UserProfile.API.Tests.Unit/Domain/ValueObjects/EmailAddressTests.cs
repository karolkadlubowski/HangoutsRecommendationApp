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
        [TestCase("@gmail.com")]
        [TestCase("hotmail.com")]
        [TestCase("gierat@@interia.com")]
        [TestCase("ala@.com")]
        [TestCase("mola@@.com")]
        public void Create_WhenEmailIsIncorrect_ThrowValidationException(string emailAddress)
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
        public void Create_WhenEmailIsNull_ThrowValidationException(string emailAddress)
        {
            //Arrange
            //Act
            Action act = () => new EmailAddress(emailAddress);
            
            //Assert
            act.Should().Throw<ValidationException>();
        }
    }
}