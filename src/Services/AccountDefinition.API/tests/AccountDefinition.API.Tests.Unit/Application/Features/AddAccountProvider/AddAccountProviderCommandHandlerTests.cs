using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using AccountDefinition.API.Application.Abstractions;
using AccountDefinition.API.Application.Features.AddAccountProvider;
using AccountDefinition.API.Domain.Entities;
using AutoMapper;
using FluentAssertions;
using Library.Database.Transaction;
using Library.Database.Transaction.Abstractions;
using Library.EventBus;
using Library.Shared.Events.Abstractions;
using Library.Shared.Logging;
using Moq;
using NUnit.Framework;

namespace AccountDefinition.API.Tests.Unit.Application.Features.AddAccountProvider
{
    [TestFixture]
    public class AddAccountProviderCommandHandlerTests
    {
        private Mock<IAccountProviderService> _accountProviderService;
        private Mock<IEventSender> _eventSender;
        private Mock<ITransactionManager> _transactionManager;
        private Mock<IMapper> _mapper;
        private Mock<ILogger> _logger;

        private AddAccountProviderCommandHandler _addAccountProviderCommandHandler;

        [SetUp]
        public void SetUp()
        {
            _accountProviderService = new Mock<IAccountProviderService>();
            _eventSender = new Mock<IEventSender>();
            _transactionManager = new Mock<ITransactionManager>();
            _mapper = new Mock<IMapper>();
            _logger = new Mock<ILogger>();

            _addAccountProviderCommandHandler = new AddAccountProviderCommandHandler(
                _accountProviderService.Object,
                _eventSender.Object,
                _transactionManager.Object,
                _mapper.Object,
                _logger.Object);
        }

        [Test]
        public async Task Handle_WhenCalled_ReturnAddAccountProviderResponse()
        {
            //Arrange
            _transactionManager.Setup(x => x.CreateScope(TransactionScopeOption.Required))
                .Returns(new DefaultTransactionScope(new TransactionScope()));
            _accountProviderService.Setup(x => x.AddAccountProviderAsync(
                    It.IsAny<AddAccountProviderCommand>()))
                .ReturnsAsync(new AccountProvider());

            //Act
            var response = await _addAccountProviderCommandHandler.Handle(new AddAccountProviderCommand(), default);

            //Assert
            response.Should().BeOfType<AddAccountProviderResponse>();
        }

        [Test]
        public async Task Handle_WhenCalled_ShouldInvokeAllRequiredMethods()
        {
            //Arrange
            _transactionManager.Setup(x => x.CreateScope(TransactionScopeOption.Required))
                .Returns(new DefaultTransactionScope(new TransactionScope()));
            _accountProviderService.Setup(x => x.AddAccountProviderAsync(
                    It.IsAny<AddAccountProviderCommand>()))
                .ReturnsAsync(new AccountProvider());

            //Act
            await _addAccountProviderCommandHandler.Handle(new AddAccountProviderCommand(), default);

            //Assert
            _transactionManager.Verify(x => x.CreateScope(TransactionScopeOption.Required), Times.Once);
            _accountProviderService.Verify(x => x.AddAccountProviderAsync(It.IsAny<AddAccountProviderCommand>()), Times.Once);
            _eventSender.Verify(x => x.SendEventAsync(EventBusTopics.AccountDefinition,
                It.IsAny<Event>(),
                It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}