using AutoMapper;
using Library.Shared.Models.VenueReview.Dtos;
using VenueReview.API.Application.Database.PersistenceModels;

namespace VenueReview.API.Application.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Domain.Entities.VenueReview, VenueReviewDto>();
            CreateMap<VenueReviewPersistenceModel, Domain.Entities.VenueReview>();
        }
    }
}