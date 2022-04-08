using System;

namespace Library.Shared.Models
{
    public abstract class RootEntity
    {
        public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
        public DateTime? ModifiedAt { get; protected set; }
    }
}