using AutoMapper;
using Identity.API.Application.Database.PersistenceModels;
using Identity.API.Domain.Entities;
using Library.Shared.Models.Identity.Events.DataModels;

namespace Identity.API.Application.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<UserPersistenceModel, User>()
                .ReverseMap();

            CreateMap<User, UserCreatedEventDataModel>();
            CreateMap<User, UserEmailChangedEventDataModel>();
        }
    }
}