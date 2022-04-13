using AutoMapper;
using FileStorage.API.Application.Database.PersistenceModels;
using FileStorage.API.Domain.Entities;
using Library.Shared.Models.FileStorage.Dtos;

namespace FileStorage.API.Application.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Folder, FolderDto>();
            CreateMap<FolderPersistenceModel, Folder>()
                .ReverseMap();

            CreateMap<File, FileDto>();
            CreateMap<FilePersistenceModel, File>()
                .ReverseMap();
        }
    }
}