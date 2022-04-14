using System.Threading.Tasks;
using FileStorage.API.Application.Features.DeleteFolder;
using FileStorage.API.Domain.Entities;

namespace FileStorage.API.Application.Abstractions
{
    public interface IFolderService : IReadOnlyFolderService
    {
        Task<Folder> DeleteFolderAsync(DeleteFolderCommand command);
    }
}