using System.Threading.Tasks;
using AutoMapper;
using Category.API.Application.Abstractions;
using Category.API.Application.Features.AddCategory;
using FluentAssertions;
using Library.EventBus;
using Library.Shared.Events.Abstractions;
using Library.Shared.Models.Category.Dtos;
using Moq;
using NUnit.Framework;

namespace Category.API.Tests.Unit.Application.Features.AddCategory
{
    [TestFixture]
    public class AddCategoryCommandHandlerTests
    {
        private Mock<ICategoryService> _categoryService;
        private Mock<IEventSender> _eventSender;
        private Mock<IMapper> _mapper;

        private AddCategoryCommandHandler _addCategoryCommandHandler;

        [SetUp]
        public void SetUp()
        {
            _categoryService = new Mock<ICategoryService>();
            _eventSender = new Mock<IEventSender>();
            _mapper = new Mock<IMapper>();

            _addCategoryCommandHandler = new AddCategoryCommandHandler(_categoryService.Object,
                _eventSender.Object,
                _mapper.Object);
        }

        [Test]
        public async Task Handle_WhenCalled_ReturnAddCategoryResponse()
        {
            //Arrange
            const string ExpectedCategoryName = "NAME";

            var category = API.Domain.Entities.Category.Create(ExpectedCategoryName);
            var expectedCategory = new CategoryDto { Name = category.Name };

            var command = new AddCategoryCommand { Name = ExpectedCategoryName };

            _categoryService.Setup(x => x.AddCategoryAsync(command))
                .ReturnsAsync(category);
            _mapper.Setup(x => x.Map<CategoryDto>(category))
                .Returns(expectedCategory);

            var expectedResponse = new AddCategoryResponse { AddedCategory = expectedCategory };

            //Act
            var result = await _addCategoryCommandHandler.Handle(command, default);

            //Assert
            result.Should().BeEquivalentTo(expectedResponse);
        }

        [Test]
        public async Task Handle_WhenCalled_ShouldInvokeProperMethodsOnce()
        {
            //Arrange
            const string Name = nameof(Name);

            var category = API.Domain.Entities.Category.Create(Name);

            var command = new AddCategoryCommand { Name = Name };

            _categoryService.Setup(x => x.AddCategoryAsync(It.IsNotNull<AddCategoryCommand>()))
                .ReturnsAsync(category);

            //Act
            await _addCategoryCommandHandler.Handle(command, default);

            //Assert
            _categoryService.Verify(x => x.AddCategoryAsync(command), Times.Once);
            _eventSender.Verify(x => x.SendEventAsync(EventBusTopics.Category,
                category.FirstStoredEvent,
                default), Times.Once);
            _mapper.Verify(x => x.Map<CategoryDto>(category), Times.Once);
        }
    }
}