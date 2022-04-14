using System.Threading.Tasks;
using FileStorage.API.Application.Features.GetFileByName;
using FileStorage.API.Domain.Entities;

namespace FileStorage.API.Application.Abstractions
{
    public interface IReadOnlyFileService
    {
        Task<File> GetFileByNameAsync(GetFileByNameQuery query);
    }
}