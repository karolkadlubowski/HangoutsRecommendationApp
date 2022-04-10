using System.Threading.Tasks;
using AccountDefinition.API.Application.Features.AddAccountProvider;
using Library.Shared.Models.AccountDefinition.Dtos;

namespace AccountDefinition.API.Application.Abstractions
{
    public interface IAccountProviderService : IReadOnlyAccountProviderService
    {
        Task<AccountProviderDto> AddAccountProviderAsync(AddAccountProviderCommand command);
    }
}