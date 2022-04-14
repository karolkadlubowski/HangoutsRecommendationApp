using System.Threading.Tasks;
using FileStorage.API.Application.Features.PutFile;
using FileStorage.API.Domain.Entities;

namespace FileStorage.API.Application.Abstractions
{
    public interface IFileService : IReadOnlyFileService
    {
        Task<File> PutFileAsync(PutFileCommand command);
    }
}