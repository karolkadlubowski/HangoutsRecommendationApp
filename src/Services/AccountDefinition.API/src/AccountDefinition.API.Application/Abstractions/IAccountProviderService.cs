using System.Threading.Tasks;
using AccountDefinition.API.Application.Features.AddAccountProvider;
using AccountDefinition.API.Application.Features.DeleteAccountProviderById;
using Library.Shared.Models.AccountDefinition.Dtos;

namespace AccountDefinition.API.Application.Abstractions
{
    public interface IAccountProviderService : IReadOnlyAccountProviderService
    {
        Task<AccountProviderDto> AddAccountProviderAsync(AddAccountProviderCommand command);
        Task<long> DeleteAccountProviderByIdAsync(DeleteAccountProviderByIdCommand command);
    }
}