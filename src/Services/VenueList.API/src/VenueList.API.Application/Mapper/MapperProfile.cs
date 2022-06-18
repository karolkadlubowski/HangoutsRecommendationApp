using AutoMapper;
using Library.Shared.Models.VenueList.Dtos;
using VenueList.API.Application.Database.PersistenceModels;

namespace VenueList.API.Application.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Domain.Entities.Favorite, FavoriteDto>();
            CreateMap<FavoritePersistenceModel, Domain.Entities.Favorite>();
        }
    }
}