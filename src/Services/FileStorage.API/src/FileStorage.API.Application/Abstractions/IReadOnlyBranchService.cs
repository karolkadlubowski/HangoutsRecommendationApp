using System.Threading.Tasks;
using FileStorage.API.Application.Features.GetBranchByName;
using Library.Shared.Models.FileStorage.Dtos;

namespace FileStorage.API.Application.Abstractions
{
    public interface IReadOnlyBranchService
    {
        Task<BranchDto> GetBranchByNameAsync(GetBranchByNameQuery query);
    }
}