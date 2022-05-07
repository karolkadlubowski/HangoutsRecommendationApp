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

            CreateMap<LocationPersistenceModel, Location>()
                .ReverseMap();

            CreateMap<LocationCoordinatePersistenceModel, LocationCoordinate>()
                .ReverseMap();

            CreateMap<Domain.Entities.Venue, VenueDto>()
                .ReverseMap();

            CreateMap<Location, LocationDto>()
                .ReverseMap();

            CreateMap<LocationCoordinate, LocationCoordinateDto>()
                .ReverseMap();

            CreateMap<FileDto, Photo>()
                .ReverseMap();

            CreateMap<Domain.Entities.Venue, VenueCreatedEventDataModel>();
            CreateMap<Domain.Entities.Venue, VenueUpdatedEventDataModel>();
        }
    }
}