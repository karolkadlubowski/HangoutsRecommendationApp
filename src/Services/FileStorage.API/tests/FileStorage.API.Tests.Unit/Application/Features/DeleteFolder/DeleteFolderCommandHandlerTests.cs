using System.Threading.Tasks;
using FileStorage.API.Application.Abstractions;
using FileStorage.API.Application.Features.DeleteFolder;
using FileStorage.API.Tests.Unit.Utilities.Models;
using FluentAssertions;
using Library.Shared.Logging;
using Moq;
using NUnit.Framework;

namespace FileStorage.API.Tests.Unit.Application.Features.DeleteFolder
{
    [TestFixture]
    public class DeleteFolderCommandHandlerTests
    {
        private Mock<IFolderService> _folderService;
        private Mock<IFileSystemAdapter> _fileSystemAdapter;
        private Mock<ILogger> _logger;

        private const string Key = nameof(Key);

        private StubFolder _deletedFolder;
        private DeleteFolderCommand _command;
        private DeleteFolderResponse _expectedResponse;

        private DeleteFolderCommandHandler _deleteFolderCommandHandler;

        [SetUp]
        public void SetUp()
        {
            _folderService = new Mock<IFolderService>();
            _fileSystemAdapter = new Mock<IFileSystemAdapter>();
            _logger = new Mock<ILogger>();

            _deletedFolder = new StubFolder(Key);
            _command = new DeleteFolderCommand { FolderKey = Key };
            _expectedResponse = new DeleteFolderResponse { DeletedFolderId = _deletedFolder.FolderId, DeletedFolderKey = Key };

            _folderService.Setup(x => x.DeleteFolderWithSubfoldersAsync(_command))
                .ReturnsAsync(_deletedFolder);

            _deleteFolderCommandHandler = new DeleteFolderCommandHandler(
                _folderService.Object,
                _fileSystemAdapter.Object,
                _logger.Object);
        }

        [Test]
        public async Task Handle_WhenFolderDeletedFromDatabaseAndFileStorage_ReturnDeleteFolderResponse()
        {
            //Arrange
            _fileSystemAdapter.Setup(x => x.DeleteFolderAsync(Key))
                .ReturnsAsync(true);

            //Act
            var result = await _deleteFolderCommandHandler.Handle(_command, default);

            //Assert
            result.Should().BeEquivalentTo(_expectedResponse);
        }

        [Test]
        public async Task Handle_WhenFolderDeletedFromDatabaseAndDeletingFolderFromFileStorageFailed_ReturnDeleteFolderResponse()
        {
            //Arrange
            _fileSystemAdapter.Setup(x => x.DeleteFolderAsync(Key))
                .ReturnsAsync(false);

            //Act
            var result = await _deleteFolderCommandHandler.Handle(_command, default);

            //Assert
            result.Should().BeEquivalentTo(_expectedResponse);
        }
    }
}