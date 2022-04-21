using System;
using FileStorage.API.Domain.ValueObjects;
using FluentAssertions;
using Library.Shared.Exceptions;
using NUnit.Framework;

namespace FileStorage.API.Tests.Unit.Domain.ValueObjects
{
    [TestFixture]
    public class FolderKeyTests
    {
        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Create_WhenFolderKeyIsNullOrEmpty_ThrowValidationException(string folderKey)
        {
            //Arrange
            //Act
            Action act = () => new FolderKey(folderKey);

            //Assert
            act.Should().Throw<ValidationException>();
        }

        [Test]
        [TestCase("key/test")]
        [TestCase("key/test ")]
        [TestCase("key//test")]
        [TestCase("key//test ")]
        [TestCase("key//test/")]
        [TestCase("key//test//")]
        public void Create_WhenCalled_ReturnTrimmedEndAndWithoutDoubledSlashesInUpperCaseFolderKey(string folderKey)
        {
            //Arrange
            const string ExpectedResult = "KEY/TEST";

            //Act
            var result = new FolderKey(folderKey).Value;

            //Assert
            result.Should().Be(ExpectedResult);
        }
    }
}