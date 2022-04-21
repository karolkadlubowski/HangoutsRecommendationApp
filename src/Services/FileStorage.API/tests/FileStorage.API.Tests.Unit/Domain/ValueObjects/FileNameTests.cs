using System;
using FileStorage.API.Domain.ValueObjects;
using FluentAssertions;
using Library.Shared.Exceptions;
using NUnit.Framework;

namespace FileStorage.API.Tests.Unit.Domain.ValueObjects
{
    [TestFixture]
    public class FileNameTests
    {
        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Create_WhenFileNameIsNullOrEmpty_ThrowValidationException(string fileName)
        {
            //Arrange
            //Act
            Action act = () => new FileName(fileName);

            //Assert
            act.Should().Throw<ValidationException>();
        }

        [Test]
        [TestCase(" Name ")]
        [TestCase(" Name")]
        [TestCase("Name ")]
        [TestCase("Name")]
        public void Create_WhenCalled_ReturnTrimmedFileName(string fileName)
        {
            //Arrange
            const string ExpectedResult = "Name";

            //Act
            var result = new FileName(fileName).Value;

            //Assert
            result.Should().Be(ExpectedResult);
        }
    }
}