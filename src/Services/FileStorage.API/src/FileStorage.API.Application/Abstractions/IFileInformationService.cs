using System.Threading.Tasks;
using FileStorage.API.Application.Features.PutFile;
using Library.Shared.Models.FileStorage.Dtos;

namespace FileStorage.API.Application.Abstractions
{
    public interface IFileInformationService
    {
        Task<FileInformationDto> PutFileInformationAsync(PutFileCommand command);
    }
}