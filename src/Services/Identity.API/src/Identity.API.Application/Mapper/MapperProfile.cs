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

            CreateMap<User, UserCreatedEventDataModel>()
                .ForMember(dest => dest.EmailAddress, opt => opt.MapFrom(x => x.Email));
            CreateMap<User, UserEmailChangedEventDataModel>()
                .ForMember(dest => dest.CurrentEmailAddress, opt => opt.MapFrom(x => x.Email));
                
        }
    }
}