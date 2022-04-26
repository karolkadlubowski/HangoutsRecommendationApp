using AutoMapper;
using Library.Shared.Models.UserProfile;
using UserProfile.API.Application.Database.PersistenceModels;
using UserProfile.API.Application.Handlers.UpdateEmailAddress;

namespace UserProfile.API.Application.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Domain.Entities.UserProfile, UserProfileDto>();
            CreateMap<UserProfilePersistenceModel, Domain.Entities.UserProfile>();
        }
    }
}