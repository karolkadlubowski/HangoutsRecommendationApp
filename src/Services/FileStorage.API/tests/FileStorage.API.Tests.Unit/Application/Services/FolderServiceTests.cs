using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FileStorage.API.Application.Database.PersistenceModels;
using FileStorage.API.Application.Database.Repositories;
using FileStorage.API.Application.Features.DeleteFolder;
using FileStorage.API.Application.Features.GetFolderByKey;
using FileStorage.API.Application.Services;
using FileStorage.API.Domain.Entities;
using FileStorage.API.Tests.Unit.Utilities.Factories;
using FileStorage.API.Tests.Unit.Utilities.Models;
using FluentAssertions;
using Library.Shared.Exceptions;
using Library.Shared.Logging;
using Moq;
using NUnit.Framework;
using SimpleFileSystem.Abstractions;

namespace FileStorage.API.Tests.Unit.Application.Services
{
    [TestFixture]
    public class FolderServiceTests
    {
        private Mock<IFolderRepository> _folderRepository;
        private Mock<IFileSystemConfiguration> _fileSystemConfiguration;
        private Mock<IMapper> _mapper;
        private Mock<ILogger> _logger;

        private const string Key = nameof(Key);

        private FolderService _folderService;

        [SetUp]
        public void SetUp()
        {
            _folderRepository = new Mock<IFolderRepository>();
            _fileSystemConfiguration = new Mock<IFileSystemConfiguration>();
            _mapper = new Mock<IMapper>();
            _logger = new Mock<ILogger>();

            _folderService = new FolderService(
                _folderRepository.Object,
                _fileSystemConfiguration.Object,
                _mapper.Object,
                _logger.Object);
        }

        #region GetFolderByKeyAsync

        [Test]
        public async Task GetFolderByKeyAsync_WhenFolderNotFoundInDatabase_ThrowEntityNotFoundException()
        {
            //Arrange
            _folderRepository.Setup(x => x.GetFolderByKeyAsync(Key))
                .ReturnsAsync(() => null);

            //Act
            Func<Task> act = () => _folderService.GetFolderByKeyAsync(new GetFolderByKeyQuery { FolderKey = Key });

            //Assert
            await act.Should().ThrowAsync<EntityNotFoundException>();
        }

        [Test]
        public async Task GetFolderByKeyAsync_WhenFolderFoundInDatabase_ReturnFolder()
        {
            //Arrange
            const string BaseUrl = "localhost";

            var folder = new TestFolder(Key);
            var folderPersistenceModel = new FolderPersistenceModel { Key = Key };

            _folderRepository.Setup(x => x.GetFolderByKeyAsync(Key))
                .ReturnsAsync(folderPersistenceModel);
            _mapper.Setup(x => x.Map<Folder>(folderPersistenceModel))
                .Returns(folder);
            _fileSystemConfiguration.Setup(x => x.BaseUrl)
                .Returns(BaseUrl);

            //Act
            var result = await _folderService.GetFolderByKeyAsync(new GetFolderByKeyQuery { FolderKey = Key });

            //Assert
            result.Should().BeEquivalentTo(folder);
        }

        #endregion

        #region DeleteFolderWithSubfoldersAsync

        [Test]
        public async Task DeleteFolderWithSubfoldersAsync_WhenFolderNotFoundInDatabase_ThrowEntityNotFoundException()
        {
            //Arrange
            _folderRepository.Setup(x => x.GetFolderByKeyAsync(Key))
                .ReturnsAsync(() => null);

            //Act
            Func<Task> act = () => _folderService.DeleteFolderWithSubfoldersAsync(new DeleteFolderCommand { FolderKey = Key });

            //Assert
            await act.Should().ThrowAsync<EntityNotFoundException>();
        }

        [Test]
        public async Task DeleteFolderWithSubfoldersAsync_WhenFolderFoundInDatabaseAndDeletingFromDatabaseFailed_ThrowDatabaseOperationException()
        {
            //Arrange
            var folder = new TestFolder(Key);
            var folderPersistenceModel = new FolderPersistenceModel { Key = Key };

            _folderRepository.Setup(x => x.GetFolderByKeyAsync(Key))
                .ReturnsAsync(folderPersistenceModel);
            _folderRepository.Setup(x => x.DeleteFolderAsync(Key))
                .ReturnsAsync(false);
            _mapper.Setup(x => x.Map<Folder>(folderPersistenceModel))
                .Returns(folder);

            //Act
            Func<Task> act = () => _folderService.DeleteFolderWithSubfoldersAsync(new DeleteFolderCommand { FolderKey = Key });

            //Assert
            await act.Should().ThrowAsync<DatabaseOperationException>();
        }

        [Test]
        public async Task DeleteFolderWithSubfoldersAsync_WhenFolderFoundInDatabaseAndDeleteFromDatabaseSucceeded_ReturnDeletedFolder()
        {
            //Arrange
            var folder = new TestFolder(Key);
            var folderPersistenceModel = new FolderPersistenceModel { Key = Key };

            _folderRepository.Setup(x => x.GetFolderByKeyAsync(Key))
                .ReturnsAsync(folderPersistenceModel);
            _folderRepository.Setup(x => x.DeleteFolderAsync(Key))
                .ReturnsAsync(true);
            _folderRepository.Setup(x => x.GetSubfoldersAsync(Key))
                .ReturnsAsync(ImmutableList<FolderPersistenceModel>.Empty);
            _mapper.Setup(x => x.Map<Folder>(folderPersistenceModel))
                .Returns(folder);

            //Act
            var result = await _folderService.DeleteFolderWithSubfoldersAsync(new DeleteFolderCommand { FolderKey = Key });

            //Assert
            result.Should().BeEquivalentTo(folder);
        }

        [Test]
        public async Task DeleteFolderWithSubfoldersAsync_WhenFolderFoundInDatabaseAndDeleteFromDatabaseSucceeded_ShouldDeleteSubfolders()
        {
            //Arrange
            const int SubfoldersCount = 3;
            const int TotalFoldersCount = 4;

            var folder = new TestFolder(Key);
            var folderPersistenceModel = new FolderPersistenceModel { Key = Key };

            _folderRepository.Setup(x => x.GetFolderByKeyAsync(Key))
                .ReturnsAsync(folderPersistenceModel);
            _folderRepository.Setup(x => x.DeleteFolderAsync(Key))
                .ReturnsAsync(true);
            _folderRepository.Setup(x => x.GetSubfoldersAsync(Key))
                .ReturnsAsync(FolderPersistenceModelsFactory.PrepareFolders(Key, SubfoldersCount).ToList());
            _mapper.Setup(x => x.Map<Folder>(folderPersistenceModel))
                .Returns(folder);

            //Act
            await _folderService.DeleteFolderWithSubfoldersAsync(new DeleteFolderCommand { FolderKey = Key });

            //Assert
            _folderRepository.Verify(x => x.DeleteFolderAsync(Key), Times.Exactly(TotalFoldersCount));
        }

        #endregion
    }
}