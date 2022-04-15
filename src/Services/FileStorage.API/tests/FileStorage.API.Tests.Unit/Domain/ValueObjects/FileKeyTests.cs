using System;
using FileStorage.API.Domain.Entities;
using FileStorage.API.Domain.ValueObjects;
using FileStorage.API.Tests.Unit.Utilities.Models;
using FluentAssertions;
using Library.Shared.Exceptions;
using NUnit.Framework;

namespace FileStorage.API.Tests.Unit.Domain.ValueObjects
{
    [TestFixture]
    public class FileKeyTests
    {
        [Test]
        public void Create_WhenFolderIsNull_ThrowValidationException()
        {
            //Arrange
            Folder folder = null;

            //Act
            Action act = () => new FileKey(string.Empty, folder);

            //Assert
            act.Should().Throw<ValidationException>();
        }

        [Test]
        public void Create_WhenCalled_ReturnFileKeyWithSingleSlashSeparator()
        {
            //Arrange
            const string Key = "Key//Test";
            const string Name = "name.json";

            var folder = new TestFolder(Key);

            var expectedResult = $"Key/Test/{Name}";

            //Act
            var result = new FileKey(Name, folder).Value;

            //Assert
            result.Should().Be(expectedResult);
        }
    }
}