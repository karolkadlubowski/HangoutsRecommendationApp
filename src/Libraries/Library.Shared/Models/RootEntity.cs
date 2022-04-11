using System;

namespace Library.Shared.Models
{
    public abstract class RootEntity
    {
        public DateTime CreatedOn { get; protected set; } = DateTime.UtcNow;
        public DateTime? ModifiedOn { get; protected set; }
    }
}