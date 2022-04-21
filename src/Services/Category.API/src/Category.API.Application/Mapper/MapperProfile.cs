using AutoMapper;
using Category.API.Application.Database.PersistenceModels;
using Library.Shared.Models.Category.Dtos;

namespace Category.API.Application.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Domain.Entities.Category, CategoryDto>();
            CreateMap<CategoryPersistenceModel, Domain.Entities.Category>()
                .ReverseMap();
        }
    }
}