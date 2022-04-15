using System.Collections.Generic;
using System.Threading.Tasks;
using AccountDefinition.API.Domain.Entities;

namespace AccountDefinition.API.Application.Abstractions
{
    public interface IReadOnlyAccountTypeService
    {
        Task<IReadOnlyList<AccountType>> GetAccountTypesAsync();
    }
}