using AutoMapper;
using Identity.API.Application.Database.PersistenceModels;
using Identity.API.Domain.Entities;

namespace Identity.API.Application.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<UserPersistenceModel, User>()
                .ReverseMap();
        }
    }
}