using System;

namespace Template.API.Domain.Entities
{
    public abstract class RootEntity
    {
        public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
        public DateTime? ModifiedAt { get; init; }
    }
}