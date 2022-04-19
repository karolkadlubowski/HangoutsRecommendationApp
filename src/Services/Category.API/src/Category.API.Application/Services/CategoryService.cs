using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Category.API.Application.Abstractions;
using Category.API.Application.Database.PersistenceModels;
using Category.API.Application.Database.Repositories;
using Category.API.Application.Features.AddCategory;
using Category.API.Application.Features.DeleteCategory;
using Library.EventBus;
using Library.Shared.Exceptions;
using Library.Shared.Logging;
using Library.Shared.Models.Category.Events;
using Library.Shared.Models.Category.Events.DataModels;

namespace Category.API.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public CategoryService(ICategoryRepository categoryRepository,
            IMapper mapper,
            ILogger logger)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IReadOnlyList<Domain.Entities.Category>> GetCategoriesAsync()
        {
            var categoriesPersistenceModels = await _categoryRepository.GetCategoriesOrderedByNameAsync();

            var categories = _mapper.Map<IReadOnlyList<Domain.Entities.Category>>(categoriesPersistenceModels);

            _logger.Info($"{categories.Count} categories read from the database");

            return categories;
        }

        public async Task<Domain.Entities.Category> AddCategoryAsync(AddCategoryCommand command)
        {
            var category = Domain.Entities.Category.Create(command.Name);

            if (await _categoryRepository.AnyCategoryExistAsync(category.Name))
                throw new DuplicateExistsException($"Category with name '{category.Name}' already exists in the database");

            var categoryPersistenceModel = await _categoryRepository.InsertCategoryAsync(category.Name)
                                           ?? throw new DatabaseOperationException($"Inserting category with name '{category.Name}' to the database failed");

            category = _mapper.Map<CategoryPersistenceModel, Domain.Entities.Category>(categoryPersistenceModel);

            _logger.Info($"Category #{category.CategoryId} with name '{category.Name}' added to the database successfully");

            category.AddDomainEvent(EventFactory<CategoryAddedEvent>.CreateEvent(category.CategoryId,
                new CategoryAddedEventDataModel { CategoryId = category.CategoryId, Name = category.Name }));

            return category;
        }

        public async Task<string> DeleteCategoryAsync(DeleteCategoryCommand command)
        {
            try
            {
                if (!await _categoryRepository.DeleteCategoryAsync(command.CategoryId))
                    throw new EntityNotFoundException($"Category #{command.CategoryId} was not deleted from the database because it was not found");

                _logger.Info($"Category #{command.CategoryId} deleted from the database successfully");

                return command.CategoryId;
            }
            catch (Exception e) when (e is not EntityNotFoundException)
            {
                throw new DatabaseOperationException($"Deleting category #{command.CategoryId} from the database failed. Exception: {e.Message}");
            }
        }
    }
}