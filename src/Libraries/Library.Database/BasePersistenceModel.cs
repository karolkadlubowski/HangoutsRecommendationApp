using System;

namespace Library.Database
{
    public abstract record BasePersistenceModel
    {
        public DateTime CreatedOn { get; init; }
        public DateTime? ModifiedOn { get; init; }
    }
}