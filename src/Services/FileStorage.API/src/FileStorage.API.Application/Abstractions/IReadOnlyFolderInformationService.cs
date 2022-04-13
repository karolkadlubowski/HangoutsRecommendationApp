using System.Threading.Tasks;
using FileStorage.API.Application.Features.GetFolderByKey;
using Library.Shared.Models.FileStorage.Dtos;

namespace FileStorage.API.Application.Abstractions
{
    public interface IReadOnlyFolderInformationService
    {
        Task<FolderInformationDto> GetFolderByKeyAsync(GetFolderByKeyQuery query);
    }
}