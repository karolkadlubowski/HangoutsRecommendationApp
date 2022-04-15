using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using AccountDefinition.API.Application.Abstractions;
using AccountDefinition.API.Application.Features.DeleteAccountProviderById;
using FluentAssertions;
using FluentAssertions.Execution;
using Library.Database.Transaction;
using Library.Database.Transaction.Abstractions;
using Library.EventBus;
using Library.Shared.Events.Abstractions;
using Library.Shared.Logging;
using Moq;
using NUnit.Framework;

namespace AccountDefinition.API.Tests.Unit.Application.Features.DeleteAccountProviderById
{
    public class DeleteAccountProviderByIdCommandHandlerTests
    {
        private Mock<IAccountProviderService> _accountProviderService;
        private Mock<IEventSender> _eventSender;
        private Mock<ITransactionManager> _transactionManager;
        private Mock<ILogger> _logger;

        private DeleteAccountProviderByIdCommandHandler _deleteAccountProviderByIdCommandHandler;

        [SetUp]
        public void SetUp()
        {
            _accountProviderService = new Mock<IAccountProviderService>();
            _eventSender = new Mock<IEventSender>();
            _transactionManager = new Mock<ITransactionManager>();
            _logger = new Mock<ILogger>();

            _deleteAccountProviderByIdCommandHandler = new DeleteAccountProviderByIdCommandHandler(
                _accountProviderService.Object,
                _eventSender.Object,
                _transactionManager.Object,
                _logger.Object);
        }

        [Test]
        public async Task Handle_WhenCalled_ReturnDeleteAccountProviderByIdResponseWithDeletedAccountProviderId()
        {
            //Arrange
            const long DeletedAccountProviderId = 1;

            _transactionManager.Setup(x => x.CreateScope(TransactionScopeOption.Required))
                .Returns(new DefaultTransactionScope(new TransactionScope()));
            _accountProviderService.Setup(x => x.DeleteAccountProviderByIdAsync(
                    It.IsAny<DeleteAccountProviderByIdCommand>()))
                .ReturnsAsync(DeletedAccountProviderId);

            //Act
            var response = await _deleteAccountProviderByIdCommandHandler.Handle(new DeleteAccountProviderByIdCommand(), default);

            //Assert
            using (new AssertionScope())
            {
                response.Should().BeOfType<DeleteAccountProviderByIdResponse>();
                response.DeletedAccountProviderId.Should().Be(DeletedAccountProviderId);
            }
        }

        [Test]
        public async Task Handle_WhenCalled_ShouldInvokeAllRequiredMethods()
        {
            //Arrange
            _transactionManager.Setup(x => x.CreateScope(TransactionScopeOption.Required))
                .Returns(new DefaultTransactionScope(new TransactionScope()));
            _accountProviderService.Setup(x => x.DeleteAccountProviderByIdAsync(
                    It.IsAny<DeleteAccountProviderByIdCommand>()))
                .ReturnsAsync(default(long));

            //Act
            await _deleteAccountProviderByIdCommandHandler.Handle(new DeleteAccountProviderByIdCommand(), default);

            //Assert
            _transactionManager.Verify(x => x.CreateScope(TransactionScopeOption.Required), Times.Once);
            _accountProviderService.Verify(x => x.DeleteAccountProviderByIdAsync(It.IsAny<DeleteAccountProviderByIdCommand>()), Times.Once);
            _eventSender.Verify(x => x.SendEventAsync(EventBusTopics.AccountDefinition,
                It.IsAny<Event>(),
                It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}