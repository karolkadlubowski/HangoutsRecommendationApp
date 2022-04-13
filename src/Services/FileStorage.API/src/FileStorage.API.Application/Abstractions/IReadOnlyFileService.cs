using System.Threading.Tasks;
using FileStorage.API.Application.Features.GetFileByName;
using Library.Shared.Models.FileStorage.Dtos;

namespace FileStorage.API.Application.Abstractions
{
    public interface IReadOnlyFileService
    {
        Task<FileDto> GetFileByNameAsync(GetFileByNameQuery query);
    }
}