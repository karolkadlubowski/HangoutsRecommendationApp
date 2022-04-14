using System.Threading.Tasks;
using FileStorage.API.Application.Features.GetFolderByKey;
using FileStorage.API.Domain.Entities;

namespace FileStorage.API.Application.Abstractions
{
    public interface IReadOnlyFolderService
    {
        Task<Folder> GetFolderByKeyAsync(GetFolderByKeyQuery query);
    }
}