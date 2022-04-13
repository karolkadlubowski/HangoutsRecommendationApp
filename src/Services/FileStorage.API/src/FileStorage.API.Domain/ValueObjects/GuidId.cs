using System;
using Library.Shared.Models;

namespace FileStorage.API.Domain.ValueObjects
{
    public record GuidId : ValueObject<string>
    {
        public GuidId()
            => Value = Guid.NewGuid().ToString("N");
    }
}