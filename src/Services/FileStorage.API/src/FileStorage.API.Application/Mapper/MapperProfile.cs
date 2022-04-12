﻿using AutoMapper;
using FileStorage.API.Application.Database.PersistenceModels;
using FileStorage.API.Domain.Entities;
using Library.Shared.Models.FileStorage.Dtos;

namespace FileStorage.API.Application.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Branch, BranchDto>();
            CreateMap<BranchPersistenceModel, Branch>();

            CreateMap<FileInformation, FileInformationDto>();
            CreateMap<FileInformationPersistenceModel, FileInformation>();
        }
    }
}