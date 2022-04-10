using System;
using System.Threading.Tasks;
using AccountDefinition.API.Application.Database.Repositories;
using AccountDefinition.API.Application.Features.AddAccountProvider;
using AccountDefinition.API.Application.Services;
using AccountDefinition.API.Tests.Unit.Utilities.Models;
using AutoMapper;
using FluentAssertions;
using Library.Shared.Exceptions;
using Library.Shared.Logging;
using Library.Shared.Models.AccountDefinition.Dtos;
using Moq;
using NUnit.Framework;

namespace AccountDefinition.API.Tests.Unit.Application.Services
{
    [TestFixture]
    public class AccountProviderServiceTests
    {
        private const string Provider = " ProVider ";
        private const string ExpectedProvider = "PROVIDER";

        private readonly static DateTime _createdOn = DateTime.UtcNow;

        private Mock<IAccountProviderRepository> _accountProviderRepository;
        private Mock<IMapper> _mapper;
        private Mock<ILogger> _logger;

        private TestAccountProvider _accountProvider;

        private AddAccountProviderCommand _command;

        private AccountProviderService _accountProviderService;

        [SetUp]
        public void SetUp()
        {
            _accountProvider = new TestAccountProvider(ExpectedProvider, _createdOn);

            _accountProviderRepository = new Mock<IAccountProviderRepository>();
            _mapper = new Mock<IMapper>();
            _logger = new Mock<ILogger>();

            _command = new AddAccountProviderCommand { Provider = Provider };

            _accountProviderService = new AccountProviderService(_accountProviderRepository.Object,
                _mapper.Object,
                _logger.Object);
        }

        [Test]
        public async Task AddAccountProviderAsync_WhenAccountProviderInsertedToDatabase_ReturnAccountProviderDto()
        {
            //Arrange
            var expectedResult = new AccountProviderDto { Provider = ExpectedProvider, CreatedOn = _createdOn };

            _accountProviderRepository.Setup(x => x.InsertAccountProviderAsync(It.IsAny<string>()))
                .ReturnsAsync(_accountProvider);
            _mapper.Setup(x => x.Map<AccountProviderDto>(_accountProvider))
                .Returns(new AccountProviderDto { Provider = _accountProvider.Provider, CreatedOn = _accountProvider.CreatedOn });

            //Act
            var result = await _accountProviderService.AddAccountProviderAsync(_command);

            //Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public async Task AddAccountProviderAsync_WhenAccountProviderInsertingToDatabaseFailed_ThrowDatabaseOperationException()
        {
            //Arrange
            _accountProviderRepository.Setup(x => x.InsertAccountProviderAsync(It.IsAny<string>()))
                .ReturnsAsync(() => null);

            //Act
            Func<Task> act = () => _accountProviderService.AddAccountProviderAsync(_command);

            //Assert
            await act.Should().ThrowAsync<DatabaseOperationException>();
        }
    }
}