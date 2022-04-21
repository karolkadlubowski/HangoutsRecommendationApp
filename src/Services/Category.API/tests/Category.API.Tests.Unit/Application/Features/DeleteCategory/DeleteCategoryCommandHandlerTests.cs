using System;
using System.Threading.Tasks;
using Category.API.Application.Abstractions;
using Category.API.Application.Features.DeleteCategory;
using FluentAssertions;
using Library.EventBus;
using Library.Shared.Events.Abstractions;
using Library.Shared.Models.Category.Events;
using Moq;
using NUnit.Framework;

namespace Category.API.Tests.Unit.Application.Features.DeleteCategory
{
    [TestFixture]
    public class DeleteCategoryCommandHandlerTests
    {
        private Mock<ICategoryService> _categoryService;
        private Mock<IEventSender> _eventSender;

        private DeleteCategoryCommandHandler _deleteCategoryCommandHandler;

        [SetUp]
        public void SetUp()
        {
            _categoryService = new Mock<ICategoryService>();
            _eventSender = new Mock<IEventSender>();

            _deleteCategoryCommandHandler = new DeleteCategoryCommandHandler(_categoryService.Object,
                _eventSender.Object);
        }

        [Test]
        public async Task Handle_WhenCalled_ReturnDeleteCategoryResponse()
        {
            //Arrange
            var categoryId = Guid.NewGuid().ToString();

            var command = new DeleteCategoryCommand { CategoryId = categoryId };

            _categoryService.Setup(x => x.DeleteCategoryAsync(command))
                .ReturnsAsync(categoryId);

            var expectedResponse = new DeleteCategoryResponse { DeletedCategoryId = categoryId };

            //Act
            var result = await _deleteCategoryCommandHandler.Handle(command, default);

            //Assert
            result.Should().BeEquivalentTo(expectedResponse);
        }

        [Test]
        public async Task Handle_WhenCalled_ShouldInvokeProperMethodsOnce()
        {
            //Arrange
            var categoryId = Guid.NewGuid().ToString();

            var command = new DeleteCategoryCommand { CategoryId = categoryId };

            _categoryService.Setup(x => x.DeleteCategoryAsync(command))
                .ReturnsAsync(categoryId);

            //Act
            await _deleteCategoryCommandHandler.Handle(command, default);

            //Assert
            _categoryService.Verify(x => x.DeleteCategoryAsync(command), Times.Once);
            _eventSender.Verify(x => x.SendEventAsync(EventBusTopics.Category,
                It.IsNotNull<CategoryDeletedEvent>(),
                default), Times.Once);
        }
    }
}