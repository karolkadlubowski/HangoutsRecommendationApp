using System.Threading;
using System.Threading.Tasks;
using Category.API.Application.Abstractions;
using Library.EventBus;
using Library.Shared.Events.Abstractions;
using Library.Shared.Models.Category.Events;
using Library.Shared.Models.Category.Events.DataModels;
using MediatR;

namespace Category.API.Application.Features.DeleteCategory
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, DeleteCategoryResponse>
    {
        private readonly ICategoryService _categoryService;
        private readonly IEventSender _eventSender;

        public DeleteCategoryCommandHandler(ICategoryService categoryService,
            IEventSender eventSender)
        {
            _categoryService = categoryService;
            _eventSender = eventSender;
        }

        public async Task<DeleteCategoryResponse> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var deletedCategoryId = await _categoryService.DeleteCategoryAsync(request);

            await _eventSender.SendEventAsync(EventBusTopics.Category,
                EventFactory<CategoryDeletedEvent>.CreateEvent(deletedCategoryId,
                    new CategoryDeletedEventDataModel { CategoryId = deletedCategoryId }),
                cancellationToken);

            return new DeleteCategoryResponse { DeletedCategoryId = deletedCategoryId };
        }
    }
}