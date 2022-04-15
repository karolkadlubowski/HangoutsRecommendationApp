using System.Threading.Tasks;
using AccountDefinition.API.Application.Features.AddAccountProvider;
using AccountDefinition.API.Application.Features.DeleteAccountProviderById;
using AccountDefinition.API.Domain.Entities;

namespace AccountDefinition.API.Application.Abstractions
{
    public interface IAccountProviderService : IReadOnlyAccountProviderService
    {
        Task<AccountProvider> AddAccountProviderAsync(AddAccountProviderCommand command);
        Task<long> DeleteAccountProviderByIdAsync(DeleteAccountProviderByIdCommand command);
    }
}