using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Category.API.Application.Abstractions;
using Library.EventBus;
using Library.Shared.Events.Abstractions;
using Library.Shared.Logging;
using Library.Shared.Models.Category.Dtos;
using MediatR;

namespace Category.API.Application.Features.AddCategory
{
    public class AddCategoryCommandHandler : IRequestHandler<AddCategoryCommand, AddCategoryResponse>
    {
        private readonly ICategoryService _categoryService;
        private readonly IEventSender _eventSender;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public AddCategoryCommandHandler(ICategoryService categoryService,
            IEventSender eventSender,
            IMapper mapper,
            ILogger logger)
        {
            _categoryService = categoryService;
            _eventSender = eventSender;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<AddCategoryResponse> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
        {
            var addedCategory = await _categoryService.AddCategoryAsync(request);

            await _eventSender.SendEventAsync(EventBusTopics.Category,
                addedCategory.FirstStoredEvent,
                cancellationToken);

            return new AddCategoryResponse { AddedCategory = _mapper.Map<CategoryDto>(addedCategory) };
        }
    }
}