using System;
using System.Threading.Tasks;
using AutoMapper;
using FileStorage.API.Application.Database.PersistenceModels;
using FileStorage.API.Application.Database.Repositories;
using FileStorage.API.Application.Features.DeleteFile;
using FileStorage.API.Application.Features.GetFileByName;
using FileStorage.API.Application.Features.PutFile;
using FileStorage.API.Application.Services;
using FileStorage.API.Domain.Entities;
using FileStorage.API.Tests.Unit.Utilities.Factories;
using FileStorage.API.Tests.Unit.Utilities.Models;
using FluentAssertions;
using FluentAssertions.Execution;
using Library.Shared.Exceptions;
using Library.Shared.Logging;
using Moq;
using NUnit.Framework;
using SimpleFileSystem.Abstractions;
using File = FileStorage.API.Domain.Entities.File;

namespace FileStorage.API.Tests.Unit.Application.Services
{
    [TestFixture]
    public class FileServiceTests
    {
        private Mock<IFolderRepository> _folderRepository;
        private Mock<IFileSystemConfiguration> _fileSystemConfiguration;
        private Mock<IMapper> _mapper;
        private Mock<ILogger> _logger;

        private const string Key = nameof(Key);

        private FileService _fileService;

        [SetUp]
        public void SetUp()
        {
            _folderRepository = new Mock<IFolderRepository>();
            _fileSystemConfiguration = new Mock<IFileSystemConfiguration>();
            _mapper = new Mock<IMapper>();
            _logger = new Mock<ILogger>();

            _fileService = new FileService(
                _folderRepository.Object,
                _fileSystemConfiguration.Object,
                _mapper.Object,
                _logger.Object);
        }

        #region GetFileByNameAsync

        [Test]
        public async Task GetFileByNameAsync_WhenFolderNotFoundInDatabase_ThrowEntityNotFoundException()
        {
            //Arrange
            _folderRepository.Setup(x => x.GetFolderByKeyAsync(It.IsAny<string>()))
                .ReturnsAsync(() => null);

            //Act
            Func<Task> act = () => _fileService.GetFileByNameAsync(new GetFileByNameQuery());

            //Assert
            await act.Should().ThrowAsync<EntityNotFoundException>();
        }

        [Test]
        public async Task GetFileByNameAsync_WhenFileIsNotFoundInFolder_ThrowEntityNotFoundException()
        {
            //Arrange
            _folderRepository.Setup(x => x.GetFolderByKeyAsync(Key))
                .ReturnsAsync(new FolderPersistenceModel { Key = Key });

            //Act
            Func<Task> act = () => _fileService.GetFileByNameAsync(new GetFileByNameQuery());

            //Assert
            await act.Should().ThrowAsync<EntityNotFoundException>();
        }

        [Test]
        public async Task GetFileByNameAsync_WhenFileIsFoundInFolderReturnFile()
        {
            //Arrange
            const string BaseUrl = "localhost";

            var folder = new StubFolder(Key);
            var file = new StubFile(Key);
            file.SetName(Key);
            folder.Files.Add(file);

            var folderPersistenceModel = new FolderPersistenceModel();

            _folderRepository.Setup(x => x.GetFolderByKeyAsync(Key))
                .ReturnsAsync(folderPersistenceModel);
            _mapper.Setup(x => x.Map<Folder>(folderPersistenceModel))
                .Returns(folder);
            _fileSystemConfiguration.Setup(x => x.BaseUrl)
                .Returns(BaseUrl);

            //Act
            var result = await _fileService.GetFileByNameAsync(new GetFileByNameQuery { FileName = Key, FolderKey = Key });

            //Assert
            result.Should().BeEquivalentTo(file);
        }

        #endregion

        #region PutFileAsync

        [Test]
        public async Task PutFileAsync_WhenFolderUpsertIntoDatabaseFailed_ThrowDatabaseOperationException()
        {
            //Arrange
            const string BaseUrl = "localhost";
            const string Name = nameof(Name);

            _folderRepository.Setup(x => x.UpsertFolderAsync(It.IsAny<FolderPersistenceModel>()))
                .ReturnsAsync(false);
            _mapper.Setup(x => x.Map<Folder>(It.IsAny<FolderPersistenceModel>()))
                .Returns(new StubFolder(Key));
            _fileSystemConfiguration.Setup(x => x.BaseUrl)
                .Returns(BaseUrl);

            //Act
            Func<Task> act = () => _fileService.PutFileAsync(new PutFileCommand()
            {
                FolderKey = Key,
                File = FormFileFactory.CreateFormFileWithName(Name)
            });

            //Assert
            await act.Should().ThrowAsync<DatabaseOperationException>();
        }

        [Test]
        public async Task PutFileAsync_WhenFolderHasBeenUpsertedSuccessfully_ReturnUpsertedFile()
        {
            //Arrange
            const string BaseUrl = "localhost";
            const string Name = nameof(Name);

            _folderRepository.Setup(x => x.UpsertFolderAsync(It.IsAny<FolderPersistenceModel>()))
                .ReturnsAsync(true);
            _fileSystemConfiguration.Setup(x => x.BaseUrl)
                .Returns(BaseUrl);

            //Act
            var result = await _fileService.PutFileAsync(new PutFileCommand
            {
                FolderKey = Key,
                File = FormFileFactory.CreateFormFileWithName(Name)
            });

            //Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<File>();
                result.Should().NotBeNull();
            }
        }

        #endregion

        #region DeleteFileAndUpdateFolderAsync

        [Test]
        public async Task DeleteFileAndUpdateFolderAsync_WhenFolderNotFoundInDatabase_ThrowEntityNotFoundException()
        {
            //Arrange
            _folderRepository.Setup(x => x.GetFolderByKeyAsync(Key))
                .ReturnsAsync(() => null);

            //Act
            Func<Task> act = () => _fileService.DeleteFileAndUpdateFolderAsync(new DeleteFileCommand { FileName = Key, FolderKey = Key });

            //Assert
            await act.Should().ThrowAsync<EntityNotFoundException>();
        }

        [Test]
        public async Task DeleteFileAndUpdateFolderAsync_WhenFileNotFoundInFolder_ThrowEntityNotFoundException()
        {
            //Arrange
            var folder = new StubFolder(Key);

            var folderPersistenceModel = new FolderPersistenceModel { Key = Key };

            _folderRepository.Setup(x => x.GetFolderByKeyAsync(Key))
                .ReturnsAsync(folderPersistenceModel);
            _mapper.Setup(x => x.Map<Folder>(folderPersistenceModel))
                .Returns(folder);

            //Act
            Func<Task> act = () => _fileService.DeleteFileAndUpdateFolderAsync(new DeleteFileCommand { FileName = Key, FolderKey = Key });

            //Assert
            await act.Should().ThrowAsync<EntityNotFoundException>();
        }

        [Test]
        public async Task DeleteFileAndUpdateFolderAsync_WhenFileFoundInFolderAndUpdatingFolderInDatabaseFailed_ThrowDatabaseOperationException()
        {
            //Arrange
            var folder = new StubFolder(Key);
            var file = new StubFile(Key);
            file.SetName(Key);
            folder.Files.Add(file);

            var folderPersistenceModel = new FolderPersistenceModel { Key = Key };

            _folderRepository.Setup(x => x.GetFolderByKeyAsync(Key))
                .ReturnsAsync(folderPersistenceModel);
            _folderRepository.Setup(x => x.UpdateFolderAsync(folderPersistenceModel))
                .ReturnsAsync(false);
            _mapper.Setup(x => x.Map<Folder>(folderPersistenceModel))
                .Returns(folder);
            _mapper.Setup(x => x.Map<FolderPersistenceModel>(folder))
                .Returns(folderPersistenceModel);

            //Act
            Func<Task> act = () => _fileService.DeleteFileAndUpdateFolderAsync(new DeleteFileCommand { FileName = Key, FolderKey = Key });

            //Assert
            await act.Should().ThrowAsync<DatabaseOperationException>();
        }

        [Test]
        public async Task DeleteFileAndUpdateFolderAsync_WhenFileFoundInFolderAndUpdatedInDatabase_ReturnDeletedFile()
        {
            //Arrange
            var folder = new StubFolder(Key);
            var file = new StubFile(Key);
            file.SetName(Key);
            folder.Files.Add(file);

            var folderPersistenceModel = new FolderPersistenceModel { Key = Key };

            _folderRepository.Setup(x => x.GetFolderByKeyAsync(Key))
                .ReturnsAsync(folderPersistenceModel);
            _folderRepository.Setup(x => x.UpdateFolderAsync(folderPersistenceModel))
                .ReturnsAsync(true);
            _mapper.Setup(x => x.Map<Folder>(folderPersistenceModel))
                .Returns(folder);
            _mapper.Setup(x => x.Map<FolderPersistenceModel>(folder))
                .Returns(folderPersistenceModel);

            //Act
            var result = await _fileService.DeleteFileAndUpdateFolderAsync(new DeleteFileCommand { FileName = Key, FolderKey = Key });

            //Assert
            result.Should().BeEquivalentTo(file);
        }

        #endregion
    }
}