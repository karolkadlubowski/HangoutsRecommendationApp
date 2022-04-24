using AutoMapper;
using Library.Shared.Models.FileStorage.Dtos;
using Library.Shared.Models.Venue.Dtos;
using Library.Shared.Models.Venue.Events.DataModels;
using Venue.API.Application.Database.PersistenceModels;
using Venue.API.Domain.Entities;
using Venue.API.Domain.Entities.Models;

namespace Venue.API.Application.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<VenuePersistenceModel, Domain.Entities.Venue>()
                .ReverseMap();
            CreateMap<Domain.Entities.Venue, VenueCreatedWithoutLocationEventDataModel>();

            CreateMap<Domain.Entities.Venue, VenueDto>()
                .ReverseMap();

            CreateMap<FileDto, Photo>()
                .ReverseMap();
        }
    }
}