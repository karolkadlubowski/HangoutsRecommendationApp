using AutoMapper;
using FileStorage.API.Application.Database.Repositories;
using FileStorage.API.Application.Services;
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
    }
}