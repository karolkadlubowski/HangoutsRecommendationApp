using System.Threading.Tasks;
using FileStorage.API.Application.Abstractions;
using FileStorage.API.Application.Features.DeleteFile;
using FileStorage.API.Tests.Unit.Utilities.Models;
using FluentAssertions;
using Library.Shared.Logging;
using Moq;
using NUnit.Framework;

namespace FileStorage.API.Tests.Unit.Application.Features.DeleteFile
{
    [TestFixture]
    public class DeleteFileCommandHandlerTests
    {
        private Mock<IFileService> _fileService;
        private Mock<IFileSystemAdapter> _fileSystemAdapter;
        private Mock<ILogger> _logger;

        private const string Key = nameof(Key);

        private StubFile _deletedFile;
        private DeleteFileCommand _command;
        private DeleteFileResponse _expectedResponse;

        private DeleteFileCommandHandler _deleteFileCommandHandler;

        [SetUp]
        public void SetUp()
        {
            _fileService = new Mock<IFileService>();
            _fileSystemAdapter = new Mock<IFileSystemAdapter>();
            _logger = new Mock<ILogger>();

            _deletedFile = new StubFile(Key);
            _command = new DeleteFileCommand { FolderKey = Key, FileName = Key };
            _expectedResponse = new DeleteFileResponse { DeletedFileId = _deletedFile.FileId };

            _fileService.Setup(x => x.DeleteFileAndUpdateFolderAsync(_command))
                .ReturnsAsync(_deletedFile);

            _deleteFileCommandHandler = new DeleteFileCommandHandler(
                _fileService.Object,
                _fileSystemAdapter.Object,
                _logger.Object);
        }

        [Test]
        public async Task Handle_WhenFileDeletedFromDatabaseAndFileStorage_ReturnDeleteFileResponse()
        {
            //Arrange
            _fileSystemAdapter.Setup(x => x.DeleteFileAsync(Key))
                .ReturnsAsync(true);

            //Act
            var result = await _deleteFileCommandHandler.Handle(_command, default);

            //Assert
            result.Should().BeEquivalentTo(_expectedResponse);
        }

        [Test]
        public async Task Handle_WhenFileDeletedFromDatabaseAndDeletingFileFromFileStorageFailed_ReturnDeleteFileResponse()
        {
            //Arrange
            _fileSystemAdapter.Setup(x => x.DeleteFileAsync(Key))
                .ReturnsAsync(false);

            //Act
            var result = await _deleteFileCommandHandler.Handle(_command, default);

            //Assert
            result.Should().BeEquivalentTo(_expectedResponse);
        }
    }
}