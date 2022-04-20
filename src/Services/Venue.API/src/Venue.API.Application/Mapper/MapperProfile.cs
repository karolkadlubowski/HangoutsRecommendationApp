using AutoMapper;
using Venue.API.Application.Database.PersistenceModels;

namespace Venue.API.Application.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<VenuePersistenceModel, Domain.Entities.Venue>();
        }
    }
}