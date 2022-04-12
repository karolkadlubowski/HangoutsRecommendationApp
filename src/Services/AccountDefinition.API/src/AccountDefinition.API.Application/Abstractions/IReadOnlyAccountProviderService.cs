using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Shared.Models.AccountDefinition.Dtos;

namespace AccountDefinition.API.Application.Abstractions
{
    public interface IReadOnlyAccountProviderService
    {
        Task<IReadOnlyList<AccountProviderDto>> GetAccountProvidersAsync();
    }
}