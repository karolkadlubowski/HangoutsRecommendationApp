using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Shared.Models.AccountDefinition.Dtos;

namespace AccountDefinition.API.Application.Abstractions
{
    public interface IReadOnlyAccountTypeService
    {
        Task<IReadOnlyList<AccountTypeDto>> GetAccountTypesAsync();
    }
}