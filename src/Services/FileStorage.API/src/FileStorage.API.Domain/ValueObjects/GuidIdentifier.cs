using System;
using Library.Shared.Models;

namespace FileStorage.API.Domain.ValueObjects
{
    public record GuidIdentifier : ValueObject<string>
    {
        public GuidIdentifier()
            => Value = Guid.NewGuid().ToString("N");
    }
}