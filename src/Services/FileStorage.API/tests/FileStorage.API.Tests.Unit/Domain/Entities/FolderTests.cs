using System;
using System.Linq;
using FileStorage.API.Domain.Entities;
using FileStorage.API.Tests.Unit.Utilities.Models;
using FluentAssertions;
using FluentAssertions.Execution;
using Library.Shared.Exceptions;
using NUnit.Framework;

namespace FileStorage.API.Tests.Unit.Domain.Entities
{
    [TestFixture]
    public class FolderTests
    {
        private const string Key = nameof(Key);

        #region AddOrReplaceFile

        [Test]
        public void AddOrReplaceFile_WhenFileIsNull_ThrowArgumentNullException()
        {
            //Arrange
            var folder = new Folder();

            //Act
            Action act = () => folder.AddOrReplaceFile(null);

            //Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void AddOrReplaceFile_WhenFileAlreadyExists_FilesCountShouldNotBeChanged()
        {
            //Arrange
            const int ExpectedFilesCount = 1;

            var folder = new Folder();
            var file = new StubFile(Key);
            folder.Files.Add(file);

            //Act
            folder.AddOrReplaceFile(file);

            //Assert
            using (new AssertionScope())
            {
                folder.Files.Count.Should().Be(ExpectedFilesCount);
                folder.Files.First().Should().BeEquivalentTo(file);
            }
        }

        [Test]
        public void AddOrReplaceFile_WhenFileDoesNotExist_PassedFileShouldBeAddedToTheFiles()
        {
            //Arrange
            const int ExpectedFilesCount = 1;

            var folder = new Folder();
            var file = new StubFile(Key);

            //Act
            folder.AddOrReplaceFile(file);

            //Assert
            using (new AssertionScope())
            {
                folder.Files.Count.Should().Be(ExpectedFilesCount);
                folder.Files.First().Should().BeEquivalentTo(file);
            }
        }

        #endregion

        #region FindFileByName

        [Test]
        public void FindFileByName_WhenFileExists_ReturnThatFile()
        {
            //Arrange
            const string FileName = nameof(FileName);

            var expectedFile = new StubFile(Key);
            expectedFile.SetName(FileName);

            var folder = new Folder();
            folder.Files.Add(expectedFile);

            //Act
            var result = folder.FindFileByName(FileName.ToLower());

            //Assert
            result.Should().BeEquivalentTo(expectedFile);
        }

        [Test]
        public void FindFileByName_WhenFileDoesNotExist_ReturnNull()
        {
            //Arrange
            const string FileName = nameof(FileName);

            var file = new StubFile(Key);
            file.SetName(FileName);

            var folder = new Folder();
            folder.Files.Add(file);

            //Act
            var result = folder.FindFileByName("not found");

            //Assert
            result.Should().BeNull();
        }

        #endregion

        #region DeleteFileIfExists

        [Test]
        public void DeleteFileIfExists_WhenFileDoesNotExist_ThrowEntityNotFoundException()
        {
            //Arrange
            const int ExpectedFilesCount = 1;

            const string FileName = nameof(FileName);

            var expectedFile = new StubFile(Key);
            expectedFile.SetName(FileName);

            var folder = new Folder();
            folder.Files.Add(expectedFile);

            //Act
            Action act = () => folder.DeleteFileIfExists("not found");

            //Assert
            using (new AssertionScope())
            {
                act.Should().Throw<EntityNotFoundException>();
                folder.Files.Count.Should().Be(ExpectedFilesCount);
            }
        }

        [Test]
        public void DeleteFileIfExists_WhenFileExists_FileShouldBeRemovedFromTheFiles()
        {
            //Arrange
            const string FileName = nameof(FileName);

            var expectedFile = new StubFile(Key);
            expectedFile.SetName(FileName);

            var folder = new Folder();
            folder.Files.Add(expectedFile);

            //Act
            folder.DeleteFileIfExists(FileName.ToLower());

            //Assert
            folder.Files.Should().BeEmpty();
        }

        #endregion
    }
}