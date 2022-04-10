using Library.Shared.Models.AccountDefinition.Enums;

namespace Library.Shared.Models.AccountDefinition.Dtos
{
    public record AccountTypeDto
    {
        public long AccountTypeId { get; init; }
        public AccountTypeOption Type { get; init; }

        public string TypeName => Type.ToString().ToUpperInvariant();
    }
}