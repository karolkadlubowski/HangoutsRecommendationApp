using System;
using System.Threading.Tasks;
using AutoMapper;
using FileStorage.API.Application.Abstractions;
using FileStorage.API.Application.Features.PutFile;
using FileStorage.API.Tests.Unit.Utilities.Factories;
using FileStorage.API.Tests.Unit.Utilities.Models;
using FluentAssertions;
using FluentAssertions.Execution;
using Library.Shared.Exceptions;
using Library.Shared.Logging;
using Library.Shared.Models.FileStorage.Dtos;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using SimpleFileSystem.Models;

namespace FileStorage.API.Tests.Unit.Application.Features.PutFile
{
    [TestFixture]
    public class PutFileCommandHandlerTests
    {
        private Mock<IFileService> _fileService;
        private Mock<IFileSystemAdapter> _fileSystemAdapter;
        private Mock<IMapper> _mapper;
        private Mock<ILogger> _logger;

        private const string Key = nameof(Key);
        private const string Name = nameof(Name);
        private const string Url = "localhost";

        private StubFile _file;
        private FileDto _fileToReturn;
        private PutFileCommand _command;
        private PutFileResponse _expectedResponse;

        private PutFileCommandHandler _putFileCommandHandler;

        [SetUp]
        public void SetUp()
        {
            _fileService = new Mock<IFileService>();
            _fileSystemAdapter = new Mock<IFileSystemAdapter>();
            _mapper = new Mock<IMapper>();
            _logger = new Mock<ILogger>();

            _file = new StubFile(Key);
            _fileToReturn = new FileDto
            {
                FileId = _file.FileId,
                Key = _file.Key,
                FolderKey = _file.FolderKey,
                Name = _file.Name,
                FileUrl = Url
            };
            _command = new PutFileCommand
            {
                FolderKey = Key,
                File = FormFileFactory.CreateFormFileWithName(Name)
            };
            _expectedResponse = new PutFileResponse { File = _fileToReturn };

            _putFileCommandHandler = new PutFileCommandHandler(
                _fileService.Object,
                _fileSystemAdapter.Object,
                _mapper.Object,
                _logger.Object);
        }

        [Test]
        public async Task Handle_WhenFileUploadingToFileStorageFailed_ThrowServerException()
        {
            //Arrange
            _fileSystemAdapter.Setup(x => x.UploadAsync(It.IsAny<IFormFile>(), It.IsAny<string>()))
                .ReturnsAsync(() => null);

            //Act
            Func<Task> act = () => _putFileCommandHandler.Handle(_command, default);

            //Assert
            await act.Should().ThrowAsync<ServerException>();
        }

        [Test]
        public async Task Handle_WhenFileUploadedToFileStorageAndUpsertingFileIntoDatabaseFailed_ThrowDatabaseOperationExceptionAndInvokeDeleteFileAsyncOnce()
        {
            //Arrange
            var fileModel = new FileModel(Key, Url);

            _fileSystemAdapter.Setup(x => x.UploadAsync(It.IsAny<IFormFile>(), It.IsAny<string>()))
                .ReturnsAsync(fileModel);
            _fileService.Setup(x => x.PutFileAsync(_command))
                .ReturnsAsync(() => null);

            //Act
            Func<Task> act = () => _putFileCommandHandler.Handle(_command, default);

            //Assert
            using (new AssertionScope())
            {
                await act.Should().ThrowAsync<DatabaseOperationException>();
                _fileSystemAdapter.Verify(x => x.DeleteFileAsync(fileModel.Path), Times.Once);
            }
        }

        [Test]
        public async Task Handle_WhenFileUploadedToFileStorageAndUpsertedIntoDatabase_ReturnPutFileResponse()
        {
            //Arrange
            _fileSystemAdapter.Setup(x => x.UploadAsync(It.IsAny<IFormFile>(), It.IsAny<string>()))
                .ReturnsAsync(new FileModel(Key, Url));
            _fileService.Setup(x => x.PutFileAsync(_command))
                .ReturnsAsync(_file);
            _mapper.Setup(x => x.Map<FileDto>(_file))
                .Returns(_fileToReturn);

            //Act
            var result = await _putFileCommandHandler.Handle(_command, default);

            //Assert
            result.Should().BeEquivalentTo(_expectedResponse);
        }
    }
}