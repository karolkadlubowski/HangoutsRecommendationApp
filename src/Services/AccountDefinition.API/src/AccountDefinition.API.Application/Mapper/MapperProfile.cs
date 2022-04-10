using AccountDefinition.API.Domain.Entities;
using AutoMapper;
using Library.Shared.Models.AccountDefinition.Dtos;

namespace AccountDefinition.API.Application.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<AccountType, AccountTypeDto>();
            CreateMap<AccountProvider, AccountProviderDto>();
        }
    }
}