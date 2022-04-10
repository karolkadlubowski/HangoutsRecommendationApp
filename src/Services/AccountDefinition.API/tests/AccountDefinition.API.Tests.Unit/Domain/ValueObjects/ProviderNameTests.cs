using System;
using AccountDefinition.API.Domain.ValueObjects;
using FluentAssertions;
using Library.Shared.Exceptions;
using NUnit.Framework;

namespace AccountDefinition.API.Tests.Unit.Domain.ValueObjects
{
    [TestFixture]
    public class ProviderNameTests
    {
        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Create_WhenProviderIsNullOrEmpty_ThrowValidationException(string provider)
        {
            //Arrange
            //Act
            Action result = () => new ProviderName(provider);

            //Assert
            result.Should().Throw<ValidationException>();
        }

        [Test]
        [TestCase("Provider ")]
        [TestCase(" Provider")]
        [TestCase(" Provider ")]
        public void Create_WhenProviderIsNotEmpty_ReturnProviderWithTrimmedWhiteCharactersAndInUpperCase(string provider)
        {
            //Arrange
            const string ExpectedResult = "PROVIDER";

            //Act
            var result = new ProviderName(provider).Value;

            //Assert
            result.Should().BeEquivalentTo(ExpectedResult);
        }
    }
}